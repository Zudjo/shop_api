using System.Text;
using Microsoft.AspNetCore.Mvc;
using shop_api.Data;
using shop_api.Models;

namespace shop_api.Controllers;

[ApiController]
public class ProductsController
{
    private DbTalker Db { get; set; }

    public ProductsController()
    {
        Db = new DbTalker();
    }
    
    // GET REQUESTS
    [HttpGet("products")]
    public JsonResult GetProducts()
    {
        try
        {
            return new JsonResult(Db.ExecuteReader("SELECT * FROM products"));

        } catch(MySqlConnector.MySqlException e)
        {
            return new JsonResult(e.Message);
        }
        
    }

    [HttpGet("products/{id}")]
    public JsonResult GetProductById(int id)
    {
        try
        {
            StringBuilder query = new StringBuilder();
            query.AppendFormat("SELECT * FROM products WHERE id = {0}", id);
            return new JsonResult(Db.ExecuteReader(query.ToString()));

        } catch(MySqlConnector.MySqlException e)
        {
            return new JsonResult(e.Message);
        }
        
    }
    
    
    // POST REQUESTS
    [HttpPost("products")]
    public JsonResult SetProducts(Product product)
    {
        StringBuilder query = new StringBuilder();
        query.AppendFormat("INSERT INTO products VALUES ({0}, '{1}', {2})", product.Id, product.Name, product.Price);
        try
        {
            return new JsonResult(Db.ExecuteReader(query.ToString()));
        } catch(MySqlConnector.MySqlException e)
        {
            return new JsonResult(e.Message);
        }
        
    }

}
