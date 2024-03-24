using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTO.Product;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Collections;
using Mysqlx.Notice;
using System.Collections.Generic;
using InventoryManagementSystem.DTO.Order;
namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly Appdbcontext _dbcontext;
        public ProductController(Appdbcontext dbContext)
        {
            _dbcontext = dbContext;
        }

        //Add product
        [HttpPost]
        public ActionResult<Products> Addproducts([FromBody] ProductDTO ProductDTO)
        {
            byte[] byteArray = Encoding.ASCII.GetBytes(ProductDTO!.ProductImage!);
            Retailer retailer = _dbcontext.Retailer.Find(ProductDTO!.Retailerid!);
            Products products = new Products()
            {
                ProductsId= ProductDTO.ProductsId,
                Productcode = ProductDTO.Productcode,
                ProductCategory = ProductDTO.ProductCategory,
                ProductName = ProductDTO.ProductName,
                Description = ProductDTO.Description,
                ProductImage = byteArray,
                BuyingPrice = ProductDTO.BuyingPrice,
                SellingPrice = ProductDTO.SellingPrice,
                ExpiryDate = ProductDTO.ExpiryDate,
                Stock = ProductDTO.Stock,
                Retailer=retailer,
            };
            _dbcontext.Products.Add(products);
            _dbcontext.SaveChanges();
            return Ok("Products Addded");
        }


        //View all product 
        [HttpGet]
        public ActionResult<IEnumerable<ProductDTO>> GetAllProducts()
        {

            //Retailer retailer = _dbcontext.Retailer.Find(ProductDTO!.Retailerid!);
            var Productslist = _dbcontext.Products.Select(i => new ProductDTO
            {
               ProductsId = i.ProductsId,
                Productcode = i.Productcode,
                ProductCategory = i.ProductCategory,
                ProductName = i.ProductName,
                Description = i.Description,
                ProductImage = Encoding.ASCII.GetString(i.ProductImage),
                BuyingPrice = i.BuyingPrice,
                SellingPrice = i.SellingPrice,
                ExpiryDate = i.ExpiryDate,
                Stock = i.Stock,
                Retailerid=i.Retailerid,
            }).ToList();
            return Ok(Productslist);
        }



        [HttpPost]
        public ActionResult FindRetailer([FromBody] GetRetailerid retailer)
        {
            var Retailer = _dbcontext.Products.Where(s => s.Retailerid == retailer.Retailerid);
            if (Retailer == null)
            {
                return Ok("Not the data");
            }
            else
            {
                return Ok(Retailer);
            }
        }


        [HttpGet("{Id:int}")]
 
        public ActionResult<Products> GetByproduct(int Id)
        {
            var Products = _dbcontext.Products.Select(i => new ProductDTO
            {
                ProductsId = i.ProductsId,
                Productcode = i.Productcode,
                ProductCategory = i.ProductCategory,
                ProductName = i.ProductName,
                Description = i.Description,
                ProductImage = Encoding.ASCII.GetString(i.ProductImage),
                BuyingPrice = i.BuyingPrice,
                SellingPrice = i.SellingPrice,
                ExpiryDate = i.ExpiryDate,
                Stock = i.Stock,
                Retailerid = i.Retailerid,
            }).FirstOrDefault(x=>x.ProductsId==Id);
            if (Products == null)
            {
                return NoContent();
            }
            return Ok(Products);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Products> UpdateProducts(int id, [FromBody] ProductDTO ProductDTO)
        {
            
            if (ProductDTO == null || id != ProductDTO.ProductsId)
            {
                return BadRequest();
            }
            var Products = _dbcontext.Products.Find(id);
            if (Products == null)
            {
                return NotFound();
            }
            Retailer retailer = _dbcontext.Retailer.Find(ProductDTO!.Retailerid!);
            byte[] byteArray = Encoding.ASCII.GetBytes(ProductDTO!.ProductImage!);
            Products.Productcode = ProductDTO.Productcode;
            Products.ProductCategory = ProductDTO.ProductCategory;
            Products.ProductName = ProductDTO.ProductName;
            Products.Description = ProductDTO.Description;
            Products.ProductImage = byteArray;
            Products.BuyingPrice = ProductDTO.BuyingPrice;
            Products.SellingPrice = ProductDTO.SellingPrice;
            Products.ExpiryDate = ProductDTO.ExpiryDate;
            Products.Stock = ProductDTO.Stock;
            Products.Retailer = retailer;
            
            _dbcontext.Products.Update(Products);
            _dbcontext.SaveChanges();
            return NoContent();
        }
        //Delete Product
        [HttpDelete("{Id:int}")]
        public ActionResult RemoveProduce(int Id)
        {
            if (Id == 0)
            {
                return BadRequest();

            }
            var Product = _dbcontext.Products.Find(Id);
            if (Product == null)
            {
                return NotFound();
            }

            _dbcontext.Products.Remove(Product);
            _dbcontext.SaveChanges();
            return Ok();
        }

    }

}
