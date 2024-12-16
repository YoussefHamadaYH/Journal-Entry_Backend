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
            // Get all journal headers and include related JournalDetails for each journal header
            var journals = _journalHeaderRepo.GetAll()
                             .Select(journal =>
                             {
                                 journal.JournalDetails = _journalDetailRepo.GetAll()
                                     .Where(d => d.JournalHeaderId == journal.Id)
                                     .Select(d => new JournalDetail
                                     {
                                         Id = d.Id,
                                         Debit = d.Debit,
                                         Credit = d.Credit,
                                         AccountId = d.AccountId,
                                         JournalHeaderId = d.JournalHeaderId,
                                         Account = _accountsChartRepo.getById(d.AccountId)
                                     }).ToList();
                                 return journal;
                             }).ToList();
            var journalDTOs = _mapper.Map<IEnumerable<JournalHeaderDTO>>(journals);

            return Ok(journalDTOs);
        }


        [HttpGet("GetJournalById/{id}")]
        public IActionResult GetJournalById(Guid id)
        {
            var journal = _journalHeaderRepo.getById(id);
            if (journal == null)
            {
                return NotFound();
            }

            journal.JournalDetails = _journalDetailRepo.GetAll()
                                .Where(d => d.JournalHeaderId == journal.Id)
                                .Select(d => new JournalDetail
                                {
                                    Id = d.Id,
                                    Debit = d.Debit,
                                    Credit = d.Credit,
                                    AccountId = d.AccountId,
                                    JournalHeaderId = d.JournalHeaderId,
                                    Account = _accountsChartRepo.getById(d.AccountId)
                                }).ToList();

            var journalDTO = _mapper.Map<JournalHeaderDTO>(journal);
            return Ok(journalDTO);
        }

        // POST: api/journal
        [HttpPost]
        public IActionResult CreateJournal([FromBody] JournalHeaderDTO journalDTO)
        {
            if (journalDTO == null)
            {
                return BadRequest();
            }

           

            // Map the JournalHeaderDTO to JournalHeader
            var journal = _mapper.Map<JournalHeader>(journalDTO);

            // Add the journal header
            _journalHeaderRepo.add(journal);

            // Save the journal header first to ensure the Id is generated
            _journalHeaderRepo.save();

            // Map and add each JournalDetailDTO to JournalDetail
            foreach (var detailDTO in journalDTO.JournalDetails)
            {
                var detail = _mapper.Map<JournalDetail>(detailDTO);

                // Ensure unique ID for JournalDetail
                detail.Id = Guid.NewGuid();
                detail.JournalHeaderId = journal.Id; // Set foreign key

                _journalDetailRepo.add(detail);
            }

            // Save changes to the database
            _journalDetailRepo.save(); // Ensure the JournalDetails are saved in a separate context operation

            return CreatedAtAction(nameof(GetJournalById), new { id = journal.Id }, journalDTO);
        }



        // PUT: api/journal/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateJournal(Guid id, [FromBody] JournalHeaderDTO journalDTO)
        {
            if (id != journalDTO.Id)
            {
                return BadRequest();
            }
            var journal = _journalHeaderRepo.getById(id);
            if (journal == null)
            {
                return NotFound();
            }
            _mapper.Map(journalDTO, journal);
            _journalHeaderRepo.update(journal);
            _journalHeaderRepo.save();
            return NoContent();
        }

        // DELETE: api/journal/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteJournal(Guid id)
        {
            var journal = _journalHeaderRepo.getById(id);
            if (journal == null)
            {
                return NotFound();
            }
            _journalHeaderRepo.delete(journal);
            _journalHeaderRepo.save();
            return NoContent();
        }

    }
}
