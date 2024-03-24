using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTO.Customer;
using InventoryManagementSystem.DTO.Order;
using InventoryManagementSystem.DTO.Product;
using InventoryManagementSystem.DTO.Retailer;
using InventoryManagementSystem.DTO.User;
using InventoryManagementSystem.Methods;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using Mysqlx.Notice;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly Appdbcontext _dbcontext;
        public OrderController(Appdbcontext dbContext)
        {
            _dbcontext = dbContext;
       }

        [HttpPost]
        public ActionResult<Models.Order> Orderproduct([FromBody] OrderDTO OrderDTO){

            var Existingcustomer = _dbcontext.Customer.Any(s => s.MobileNumber == OrderDTO.MobileNumber);
            if (Existingcustomer == false)
            {
                Customer customer = new Customer()
                {
                    CustomerID = 0,
                    CustomerName = OrderDTO.CustomerName,
                    MobileNumber = OrderDTO.MobileNumber,
                    Creditpoints = 0
                };
                _dbcontext.Customer.Add(customer);
                _dbcontext.SaveChanges();
            }

            //update stock 
            var Products = _dbcontext.Products.Find(OrderDTO.ProductsId);
            Products!.Stock = Products.Stock - OrderDTO.Count;
            _dbcontext.Products.Update(Products);

            Retailer retailer = _dbcontext.Retailer.Find(OrderDTO.Retailerid);
            Customer Customers = _dbcontext.Customer.Where(s=>s.MobileNumber==OrderDTO.MobileNumber).FirstOrDefault();

            Models.Order order = new Models.Order()
            {
                OrderId = 0,
                OrderDate = DateTime.Now,
                Customer = Customers,
                ProductsId = OrderDTO.ProductsId,
                Count = OrderDTO.Count,
                ProductAmount = Products.SellingPrice * OrderDTO.Count,
                Retailer = retailer,
                BillId = OrderDTO.BillId
            };

            Customers!.Creditpoints = Customers.Creditpoints + (order.ProductAmount) / 500;
            _dbcontext.Customer.Update(Customers);
            _dbcontext.Order.Add(order);
            _dbcontext.SaveChanges();
            return Ok("ok");
        }
        //Order history
        [HttpPost]
        public ActionResult Findorderhistory([FromBody] GetRetailerid retailer)
        {
            var Retailer = _dbcontext.Order.Include(x=>x.Retailer).Include(x=>x.Customer).Where(s => s.RetailerID == retailer.Retailerid);
      
            //Retailer retailer = _dbcontext.Retailer.Find(ProductDTO!.Retailerid!);
            if (Retailer == null)
            {
                return Ok("Not the data");
            }

            else
            {
                return Ok(Retailer);
            }
        }

//customer 

        [HttpPost]
        public ActionResult FindCustomer([FromBody] GetRetailerid retailer)
        {
            var Retailer = _dbcontext.Order.Where(s => s.CustomerID == retailer.Retailerid);

            //Retailer retailer = _dbcontext.Retailer.Find(ProductDTO!.Retailerid!);
            if (Retailer == null)
            {
                return Ok("Not the data");
            }

            else
            {
                return Ok(Retailer);
            }
        }





        //[HttpGet]
        //public ActionResult<IEnumerable<Models.Order>> GetAll()
        //{
        //    //return _context.Appointment.Include(a => a.user).Include(x => x.mobile).Include(s => s.repairShop).ToList();
        //    var Orderslist = _dbcontext.Order.ToList();
        //    if (Orderslist == null)
        //    {
        //        return NoContent();
        //    }
        //    return Ok(Orderslist);

        //}


        //[HttpGet("{Id:int}")]
        //public ActionResult<Models.Order> Getyorder(int Id)
        //{
        //    var OrderBill = _dbcontext.Order.Find(Id);

        //    if (OrderBill == null)
        //    {
        //        return NoContent();
        //    }
        //    return Ok(OrderBill);
        //}


    }
}