namespace InventoryManagementSystem.DTO.Order
{
    public class OrderDTO
    {
    
        public int ProductsId { get; set; }

        public int Count {  get; set; }

        public long MobileNumber { get; set; }

        public string? CustomerName { get; set; }

        public int Retailerid { get; set; }

        public string? BillId { get; set; }



    }
}
