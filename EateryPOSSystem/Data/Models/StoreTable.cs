namespace EateryPOSSystem.Data.Models
{
    public class StoreTable
    {
        public int Id { get; set; }

        public int TableNumber { get; set; }

        public int StoreId { get; set; }

        public string StoreName { get; set; }

        public int BillId { get; set; }

        public int UserId { get; set; }
    }
}
