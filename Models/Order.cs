namespace InventoryManagementSystem.Models
{
    public class Order
    {
        public int OrderId {  get; set; }
        public DateTime OrderDate { get; set; }
        public Customer? Customer { get; set; }
        public int CustomerID {  get; set; }
        public Products? Products { get; set; }
        public int ProductsId {  get; set; }
        public Retailer? Retailer { get; set; }
        public  int RetailerID { get; set; }
        public int Count { get; set; }
        public string? BillId { get; set; }
        public int ProductAmount {  get; set; }


      
    }
}
