namespace InventoryManagementSystem.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Retailer? Retailer { get; set; }
        public int Retailerid { get; set; }
        public SuperProduct? SuperProduct { get; set; }
        public int SuperProductId { get; set; }
        public int Count { get; set; }
        public string? BillId { get; set; }
        public int ProductAmount { get; set; }
      

    }
}
