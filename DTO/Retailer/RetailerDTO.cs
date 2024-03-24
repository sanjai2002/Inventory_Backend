using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.DTO.User
{
    public class RetailerDTO
    {
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public long Mobilenumber { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? ShopName { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        public string? GstNumber { get; set; }
    }
}
