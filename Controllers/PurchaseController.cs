using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTO.Dealer;
using InventoryManagementSystem.DTO.Order;
using InventoryManagementSystem.Methods;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly Appdbcontext _dbcontext;
        public PurchaseController(Appdbcontext dbContext)
        {
            _dbcontext = dbContext;
        }

        [HttpPost]
        public ActionResult<Models.Purchase> Purchaseorder([FromBody] PurchaseDTO PurchaseDTO)
        {
            Retailer retailer = _dbcontext.Retailer.Find(PurchaseDTO.Retailerid);

            //update stock 
            var superproducts = _dbcontext.SuperProduct.Find(PurchaseDTO.SuperProductId);
            superproducts!.Stock = superproducts.Stock - PurchaseDTO.Count;
            _dbcontext.SuperProduct.Update(superproducts);

            Purchase purchase = new Purchase()
            {
                PurchaseId=0,
                PurchaseDate=DateTime.Now,
                Retailer= retailer,
                SuperProduct= superproducts,
                Count= PurchaseDTO.Count,
                BillId = "TYC" + Randompassword.Randompasswordgenerator(),
                ProductAmount = superproducts.SellingPrice * PurchaseDTO.Count,
             
            };
            _dbcontext.Purchase.Add(purchase);
            _dbcontext.SaveChanges();
            return Ok("ok");
        }


        [HttpGet]
        public ActionResult<IEnumerable<Purchase>> GetAllPurchase()
        {
      
            var Purchasedetails = _dbcontext.Purchase.Include(x => x.Retailer).Include(x => x.SuperProduct).ToList();
     
            if (Purchasedetails == null)
            {
                return NoContent();
            }
            return Ok(Purchasedetails);
        }

   

        // Cancel order

        [HttpDelete("{Id:int}")]
        public ActionResult Cancelorder(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();

            }
            var Purchase = _dbcontext.Purchase.Find(Id);
            if (Purchase == null)
            {
                return NotFound();
            }

            _dbcontext.Purchase.Remove(Purchase);
            _dbcontext.SaveChanges();
            return Ok();
        }



    }


}

