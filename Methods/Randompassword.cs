using System.Text;

namespace InventoryManagementSystem.Methods
{
    public class Randompassword
    {
        public static string Randompasswordgenerator()
        {
            Random random = new Random();
            int passwordlenth = random.Next(6, 10);
            const string Validcharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder password = new StringBuilder();
            for (int i = 0; i < passwordlenth; i++)
            {
                int randomcode = random.Next(0, Validcharacters.Length - 1);
                password.Append(Validcharacters[randomcode]);
            }
            return password.ToString();
        }
    }
}
