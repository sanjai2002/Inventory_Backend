using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class SuperProduct
    {
        [Key]
        public int SuperProductId { get; set; }
        public int Productcode { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public byte[] ProductImage { get; set; }
        public int BuyingPrice { get; set; }
        public int SellingPrice { get; set; }
        public string? ExpiryDate { get; set; }
        public int Stock { get; set; }

        //public ICollection<Purchase> Purchase { get; set;}

    }
}
