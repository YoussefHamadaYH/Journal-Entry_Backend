using JournyTask.Models;

namespace JournyTask.DTOs
{
    public class JournalHeaderDTO
    {
        public Guid Id { get; set; }
        public  DateTimeOffset EntryDate  { get; set; }
        public string Description { get; set; }
        public List<JournalDetailDTO> JournalDetails { get; set; } = new List<JournalDetailDTO>();

    }
}
