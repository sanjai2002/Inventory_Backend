using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class Products
    {

        [Key]
      
         public int ProductsId { get; set; }
        public int Productcode {  get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public byte[] ProductImage { get; set; }
        public int BuyingPrice {  get; set; }
        public int SellingPrice { get; set; }
        public string? ExpiryDate { get; set; }
        public int Stock { get; set; }

        public int Retailerid { get; set; }

        [ForeignKey("Retailerid")]
        public Retailer? Retailer { get; set; }
        
        //public ICollection<Order> Order { get; set; }
        
    }
}