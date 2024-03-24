using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTO.Dealer;
using InventoryManagementSystem.DTO.Product;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SuperproductController : ControllerBase
    {
        private readonly Appdbcontext _dbcontext;
        public SuperproductController(Appdbcontext dbContext)
        {
            _dbcontext = dbContext;
        }


        //Add product
        [HttpPost]
        public ActionResult<SuperProduct> AddSuperproduct([FromBody] SuperproductDTO Superproduct)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(Superproduct!.ProductImage!);

            SuperProduct SuperProduct = new SuperProduct()
            {

                SuperProductId= Superproduct.SuperProductId,
                Productcode = Superproduct.Productcode,
                ProductCategory = Superproduct.ProductCategory,
                ProductName = Superproduct.ProductName,
                Description = Superproduct.Description,
                ProductImage = byteArray,
                BuyingPrice = Superproduct.BuyingPrice,
                SellingPrice = Superproduct.SellingPrice,
                ExpiryDate = Superproduct.ExpiryDate,   
                Stock = Superproduct.Stock,
           
            };
            _dbcontext.SuperProduct.Add(SuperProduct);
            _dbcontext.SaveChanges();
            return Ok("super Products Addded");
        }

        //View all product
        [HttpGet]
        public ActionResult<IEnumerable<SuperproductDTO>> GetAllProducts()
        {
            var Productslist = _dbcontext.SuperProduct.Select(i => new SuperproductDTO
            {
                SuperProductId = i.SuperProductId,
                Productcode = i.Productcode,
                ProductCategory = i.ProductCategory,
                ProductName = i.ProductName,
                Description = i.Description,
                ProductImage = Encoding.ASCII.GetString(i.ProductImage),
                BuyingPrice = i.BuyingPrice,
                SellingPrice = i.SellingPrice,
                ExpiryDate = i.ExpiryDate,
                Stock = i.Stock,
            }).ToList();
            return Ok(Productslist);
        }

        [HttpGet("{Id}")]
        public ActionResult<SuperProduct> GetByproductid(int Id)
        {
            //var Orderdetails = _dbcontext.SuperProduct.Find(Id);

            var Productslist = _dbcontext.SuperProduct.Select(i => new SuperproductDTO
            {
                SuperProductId = i.SuperProductId,
                Productcode = i.Productcode,
                ProductCategory = i.ProductCategory,
                ProductName = i.ProductName,
                Description = i.Description,
                ProductImage = Encoding.ASCII.GetString(i.ProductImage),
                BuyingPrice = i.BuyingPrice,
                SellingPrice = i.SellingPrice,
                ExpiryDate = i.ExpiryDate,
                Stock = i.Stock,
            }).FirstOrDefault(x => x.SuperProductId == Id); ;
            if (Productslist == null)
            {
                return NoContent();
            }
            return Ok(Productslist);
        }


        [HttpPut("{id:int}")]
        public ActionResult<SuperProduct> UpdatesuperProducts(int id, [FromBody] SuperproductDTO SuperproductDTO)
        {

            
            var SuperProducts = _dbcontext.SuperProduct.Find(id);
            if (SuperProducts == null)
            {
                return NotFound();
            }
            
            byte[] byteArray = Encoding.ASCII.GetBytes(SuperproductDTO!.ProductImage!);
            SuperProducts.Productcode = SuperproductDTO.Productcode;
            SuperProducts.ProductCategory = SuperproductDTO.ProductCategory;
            SuperProducts.ProductName = SuperproductDTO.ProductName;
            SuperProducts.Description = SuperproductDTO.Description;
            SuperProducts.ProductImage = byteArray;
            SuperProducts.BuyingPrice = SuperproductDTO.BuyingPrice;
            SuperProducts.SellingPrice = SuperproductDTO.SellingPrice;
            SuperProducts.ExpiryDate = SuperproductDTO.ExpiryDate;
            SuperProducts.Stock = SuperproductDTO.Stock;
           

            _dbcontext.Products.Update(SuperProducts);
            _dbcontext.SaveChanges();
            return NoContent();
        }
        //Delete Product
        //[HttpDelete("{Id:int}")]
        //public ActionResult RemoveProduce(int Id)
        //{
        //    if (Id == 0)
        //    {
        //        return BadRequest();

        //    }
        //    var Product = _dbcontext.Products.Find(Id);
        //    if (Product == null)
        //    {
        //        return NotFound();
        //    }

        //    _dbcontext.Products.Remove(Product);
        //    _dbcontext.SaveChanges();
        //    return Ok();
        //}





    }
}
