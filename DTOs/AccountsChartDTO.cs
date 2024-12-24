namespace JournyTask.DTOs
{
    public class AccountsChartDTO
    {
        public Guid? AccountId { get; set; }

        public string ArabicName { get; set; } = null!;

        public string EnglishName { get; set; } = null!;

        public string AccountNumber { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime? CreationDate { get; set; }

        public bool AllowEntry { get; set; }
    }
}
