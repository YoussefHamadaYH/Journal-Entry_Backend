using JournyTask.Models;
using System.ComponentModel.DataAnnotations;

namespace JournyTask.DTOs
{
    public class JournalHeaderDTO
    {
        public Guid Id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public  DateTime EntryDate  { get; set; }
        [Required]
        public string Description { get; set; }
        public List<JournalDetailDTO> JournalDetails { get; set; } = new List<JournalDetailDTO>();

    }
}
