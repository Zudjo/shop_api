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
            if (Db.ExecuteNonQuery(query.ToString()) == 1)
            {
                return new JsonResult("Record added");
            }
            return new JsonResult("Make sure this id doesn't exist already");
        } catch(MySqlConnector.MySqlException e)
        {
            return new JsonResult(e.Message);
        }
        
    }

    [HttpPost("products/{Id}/{Price}/{Name}")]
    public JsonResult UpdateProducts(int Id, int Price, string Name)
    {
        StringBuilder query = new StringBuilder();
        query.AppendFormat("UPDATE products SET Price = {0}, Name = '{1}' WHERE Id = {2})", Name, Price, Id);
        try
        {
            if (Db.ExecuteNonQuery(query.ToString()) == 1)
            {
                return new JsonResult("Record updated");
            }
            return new JsonResult("This id doesn't exist");
        } catch(MySqlConnector.MySqlException e)
        {
            return new JsonResult(e.Message);
        }
        
    }

    // DELETE REQUEST
    [HttpDelete("products/{Id}")]
    public JsonResult deleteProducts(int Id)
    {
        StringBuilder query = new StringBuilder();
        query.AppendFormat("DELETE FROM products WHERE Id = {0}",
            Id);
        try
        {
            if (Db.ExecuteNonQuery(query.ToString()) == 1)
            {
                return new JsonResult("Record deleted");
            }
            return new JsonResult("This id doesn't exist.");
        } catch(MySqlConnector.MySqlException e)
        {
            return new JsonResult(e.Message);
        }
    }



}
