using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTO.Retailer;
using InventoryManagementSystem.DTO.User;
using InventoryManagementSystem.Methods;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class RetailerController : ControllerBase
    {
        private readonly Appdbcontext _dbcontext;
        private readonly IConfiguration _configuration;

        public RetailerController(Appdbcontext dbContext, IConfiguration configuration)
        {
            _dbcontext = dbContext;
            _configuration = configuration;
        }
        //Retailer Register
        [HttpPost]
        public ActionResult<Retailer> Regsiter([FromBody] RetailerDTO RetailerDTO)
        {
            var Existinguser = _dbcontext.Retailer.Any(s => s.Email == RetailerDTO.Email);
            if (Existinguser == false)
            {
                Retailer Registerusers = new Retailer()
                {
                    Name = RetailerDTO.Name,
                    Email = RetailerDTO.Email,
                    Mobilenumber = RetailerDTO.Mobilenumber,
                    Password = SHA256Encrypt.ComputePasswordToSha256Hash(RetailerDTO!.Password!),
                    ShopName = RetailerDTO.ShopName,
                    Location = RetailerDTO.Location,
                    GstNumber = RetailerDTO.GstNumber,
                    UserStatus = 0,
                    Role = "Retailer",
                };
                _dbcontext.Retailer.Add(Registerusers);
                _dbcontext.SaveChanges();
                return Ok("Register Successfully");
            }
            else
            {
                return Ok("Email is already exit");
            }

        }

        //login page

        [HttpPost]
        public ActionResult UserLogin([FromBody] Login Login)
        {
            ReturnMsg authmessage;
            var loginuser = _dbcontext.Retailer.Any(s => s.Email == Login.Email);
            ReturnMsg ob = new ReturnMsg();
            if (loginuser == true)
            {
                var Existinguser = _dbcontext.Retailer.Where(s => s.Email == Login.Email).FirstOrDefault();

                if (Existinguser!.Password == SHA256Encrypt.ComputePasswordToSha256Hash(Login!.Password!))
                {

                    authmessage = new ReturnMsg
                    {
                        Email = true,
                        Password = true,
                        UserStatus = Existinguser.UserStatus,
                        Role = Existinguser.Role
                    };
                    return Ok(authmessage);
                }
                authmessage = new ReturnMsg
                {
                    Email = true,
                    Password = false,

                };
                return Ok(authmessage);

            }
            authmessage = new ReturnMsg
            {
                Email = false,
                Password = false,
            };
            return Ok(authmessage);
        }

        //forget password-Send Email
        [HttpPost]
        public ActionResult<Retailer> Forget([FromBody] Emaildetails email)
        {
            var Email = _dbcontext.Retailer.Where(e => e.Email == email.Email).FirstOrDefault();
            if (Email == null)
            {
                return Ok("Enter correct Email");
            }
            string password = Randompassword.Randompasswordgenerator();
            Email.Password = SHA256Encrypt.ComputePasswordToSha256Hash(password);

            _dbcontext.Retailer.Update(Email);
            _dbcontext.SaveChanges();
            SendEmailPassword.Sendpassword(password, Email!.Email!);
            return Ok("Send password your Email");
        }


        [HttpPut]
        public ActionResult<Retailer> Updatepassword([FromBody] ForgetPassword forgetpassword)
        {
            var Existinguser = _dbcontext.Retailer.Where(e => e.Email == forgetpassword.Email).FirstOrDefault();

            if (Existinguser!.Password == SHA256Encrypt.ComputePasswordToSha256Hash(forgetpassword!.ReceivePassword!))
            {
                Existinguser!.Password = SHA256Encrypt.ComputePasswordToSha256Hash(forgetpassword!.Newpassword!);
                _dbcontext.Retailer.Update(Existinguser);
                _dbcontext.SaveChanges();
                return Ok("password updated");
            }
            return Ok("Enter correct password");
        }


        //find details using email
        [HttpPost]
        public ActionResult FindEmail([FromBody] Emaildetails email)
        {
            var Email = _dbcontext.Retailer.Where(s => s.Email == email.Email).FirstOrDefault();
            if (Email == null)
            {
                return Ok("Not the data");
            }
            else
            {
          
                return Ok(Email);
            }
        }



    }
}
