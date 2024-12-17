using AutoMapper;
using JournyTask.DTOs;
using JournyTask.IRepository;
using JournyTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JournyTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IGenericRepository<JournalHeader> _journalHeaderRepo;
        private readonly IGenericRepository<JournalDetail> _journalDetailRepo;
        private readonly IGenericRepository<AccountsChart> _accountsChartRepo;
        private readonly IMapper _mapper;

        public JournalController(IGenericRepository<JournalHeader> journalHeaderRepo, IGenericRepository<JournalDetail> journalDetailRepo, IMapper mapper, IGenericRepository<AccountsChart> accountsChartRepo)
        {
            _journalHeaderRepo = journalHeaderRepo;
            _journalDetailRepo = journalDetailRepo;
            _accountsChartRepo = accountsChartRepo;
            _mapper = mapper;
        }
        [HttpGet("GetAllJournals")]
        public IActionResult GetAllJournals()
        {
            var journals = _journalHeaderRepo.GetAll()
                .Select(journal => new JournalHeaderDTO
                {
                    Id = journal.Id,
                    EntryDate = journal.EntryDate,
                    Description = journal.Description,
                    JournalDetails = _journalDetailRepo.GetAll()
                        .Where(detail => detail.JournalHeaderId == journal.Id)
                        .Select(detail => new JournalDetailDTO
                        {
                            Id = detail.Id,
                            Debit = detail.Debit,
                            Credit = detail.Credit,
                            AccountId = detail.AccountId,
                            AccountName = _accountsChartRepo.getById(detail.AccountId).NameEn,
                            AccountNumber = _accountsChartRepo.getById(detail.AccountId).Number
                        }).ToList()
                }).ToList();

            return Ok(journals);
        }

        [HttpPost]
        public IActionResult CreateJournal([FromBody] JournalHeaderDTO dto)
        {
            if (dto == null || !dto.JournalDetails.Any())
                return BadRequest("Invalid data. Journal details are required.");

            foreach (var journalDetailDto in dto.JournalDetails)
            {
                var account = _accountsChartRepo.getById(journalDetailDto.AccountId);
                if (account == null)
                {
                    var newAccount = new AccountsChart
                    {
                        Id = journalDetailDto.AccountId == Guid.Empty ? Guid.NewGuid() : journalDetailDto.AccountId,
                        NameAr = journalDetailDto.AccountName ?? "Default Arabic Name",
                        NameEn = journalDetailDto.AccountName ?? "Default English Name",
                        Number = journalDetailDto.AccountNumber ?? "0000",
                        AllowEntry = true,
                        IsActive = true,
                        CreationDate = DateTime.UtcNow,
                        FkTransactionTypeId = 1,
                        UserId = 1,
                        BranchId = 1,
                        OrgId = 1
                    };

                    _accountsChartRepo.add(newAccount);
                    journalDetailDto.AccountId = newAccount.Id;
                }
                else
                {
                    account.NameAr = journalDetailDto.AccountName ?? account.NameAr;
                    account.NameEn = journalDetailDto.AccountName ?? account.NameEn;
                    account.Number = journalDetailDto.AccountNumber ?? account.Number;
                }
            }

            var journalHeader = new JournalHeader
            {
                Id = Guid.NewGuid(),
                EntryDate = dto.EntryDate,
                Description = dto.Description,
                JournalDetails = new List<JournalDetail>()
            };

            foreach (var jd in dto.JournalDetails)
            {
                var detail = new JournalDetail
                {
                    Id = Guid.NewGuid(),
                    Debit = jd.Debit,
                    Credit = jd.Credit,
                    AccountId = jd.AccountId,
                    JournalHeaderId = journalHeader.Id
                };
                journalHeader.JournalDetails.Add(detail);
            }

            _journalHeaderRepo.add(journalHeader);
            try
            {
                _journalHeaderRepo.save();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

            return Ok(new { message = "Journal created successfully", journalId = journalHeader.Id });
        }

        [HttpGet("GetJournalById/{id}")]
        public IActionResult GetJournalById(Guid id)
        {
            var journal = _journalHeaderRepo.getById(id);
            if (journal == null)
            {
                return NotFound($"Journal with ID {id} not found.");
            }

            journal.JournalDetails = _journalDetailRepo.GetAll()
                .Where(detail => detail.JournalHeaderId == journal.Id)
                .ToList();

            var journalDTO = _mapper.Map<JournalHeaderDTO>(journal);

            // Populate account name and number for each journal detail
            foreach (var detail in journalDTO.JournalDetails)
            {
                var account = _accountsChartRepo.getById(detail.AccountId);
                if (account != null)
                {
                    detail.AccountName = account.NameEn; // or account.NameAr based on your preference
                    detail.AccountNumber = account.Number;
                }
            }

            return Ok(journalDTO);
        }
    }
}
