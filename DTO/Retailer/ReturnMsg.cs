namespace InventoryManagementSystem.DTO.User
{
    public class ReturnMsg
    {
        public bool Email { get; set; }
        public bool Password { get; set; }
        public int UserStatus { get; set; }
        public string? Role { get; set; }

    }
}
