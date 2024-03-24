using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly Appdbcontext _dbcontext;
        public CustomerController(Appdbcontext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpGet]
       
        public ActionResult<IEnumerable<Customer>> GetCustomerdetails()
        {
            var Customer = _dbcontext.Customer.ToList();
            if (Customer == null)
            {
                return NoContent();
            }
            return Ok(Customer);
        }

    }
}
