namespace JournyTask.DTOs
{
    public class JournalDetailDTO
    {
        public Guid? Id { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
    }
}
