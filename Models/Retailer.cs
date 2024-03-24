using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Retailer
    {
        [Key]
        public int Retailerid {  get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public long Mobilenumber { get; set; }
        [Required]
        public string? Password {  get; set; }
        [Required]
        public string? ShopName { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        public string? GstNumber { get; set; }
        [Required]
        public string? Role { get; set; }
        [Required]
        public int UserStatus { get; set;}
        //public ICollection<Products> Products { get;  }
        //public ICollection<Order> Orders { get; }

        //public ICollection<Purchase> Purchase { get; }


    }
}
