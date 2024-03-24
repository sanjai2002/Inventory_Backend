using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.DTO.Dealer
{
    public class PurchaseDTO
    {

        public int PurchaseId {  get; set; }
        public int SuperProductId { get; set; }

        public int Retailerid { get; set; }
        public int Count { get; set; }




    }
}
