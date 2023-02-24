using System.Text;
using Microsoft.AspNetCore.Mvc;
using shop_api.Data;
using shop_api.Models;

namespace shop_api.Controllers;

[ApiController]
public class InvoicesController
{
    private DbTalker Db { get; set; }

    public InvoicesController()
    {
        Db = new DbTalker();
    }
    
    // GET REQUESTS
    [HttpGet("invoices")]
    public JsonResult getInvoices()
    {
        try
        {
            return new JsonResult(Db.ExecuteReader("SELECT * FROM invoices"));

        } catch(MySqlConnector.MySqlException e)
        {
            return new JsonResult(e.Message);
        }
        
    }

    // POST REQUESTS
    [HttpPost("invoices")]
    public JsonResult setInvoices(Invoice invoice)
    {
        StringBuilder query = new StringBuilder();
        query.AppendFormat("INSERT INTO invoices VALUES ({0})", invoice.Id);
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

    // DELETE REQUEST
    [HttpDelete("invoices/{Id}")]
    public JsonResult deleteInvoices(int Id)
    {
        StringBuilder query = new StringBuilder();
        query.AppendFormat("DELETE FROM invoices WHERE Id = {0}",
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
