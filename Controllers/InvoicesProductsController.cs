using System.Text;
using Microsoft.AspNetCore.Mvc;
using shop_api.Data;
using shop_api.Models;
using shop_api.Utilities;

namespace shop_api.Controllers;

[ApiController]
public class InvoicesProductsController
{
    private DbTalker Db { get; set; }

    public InvoicesProductsController()
    {
        Db = new DbTalker();
    }
    
    // GET REQUESTS
    [HttpGet("invoices-products")]
    public JsonResult getInvoicesProducts()
    {
        try
        {
            string query = @"
                SELECT invoices_products.IdInvoice, invoices_products.IdProduct, invoices_products.Quantity, products.Name, products.Price
                FROM invoices_products
                JOIN products ON invoices_products.IdProduct = products.Id";
            List<object> invoicesProducts = Db.ExecuteReader(query);
            return GroupProductsUnderInvoices(invoicesProducts);

        } catch(MySqlConnector.MySqlException)
        {
            throw;
        }
        
    }

    [HttpGet("invoices-products/{invoiceId}")]
    public JsonResult getInvoicesProductsById(int invoiceId)
    {
        if (!ThereIsId(invoiceId))
        {
            return new JsonResult("This invoice id doesn't exists.");
        }
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM invoices_products WHERE id_invoice = {0}", invoiceId);
            return new JsonResult(Db.ExecuteReader(sb.ToString()));

        } catch(MySqlConnector.MySqlException e)
        {
            return new JsonResult(e.Message);
        }
        
    }

    // POST REQUESTS
    [HttpPost("invoices-products")]
    public JsonResult setInvoicesProducts(InvoicesProducts invoices_products)
    {
        StringBuilder query = new StringBuilder();
        query.AppendFormat("INSERT INTO invoices_products VALUES ({0}, {1}, {2})",
            invoices_products.IdInvoice, invoices_products.IdProduct, invoices_products.Quantity);
        try
        {
            if (Db.ExecuteNonQuery(query.ToString()) == 1)
            {
                return new JsonResult("Record added");
            }
            return new JsonResult("Make sure this id couple doesn't exist already");
        } catch(MySqlConnector.MySqlException e)
        {
            return new JsonResult(e.Message);
        }
    }

    [HttpPost("invoices-products/{invoiceId}/{productId}/{quantity}")] // update quantity of a invoices-products record
    public JsonResult updateInvoicesProducts(int invoiceId, int productId, int quantity)
    {
        StringBuilder query = new StringBuilder();
        query.AppendFormat("UPDATE invoices_products SET Quantity = {0} WHERE IdInvoice = {1} AND IdProduct = {2}",
            quantity, invoiceId, productId);
        try
        {
            if (Db.ExecuteNonQuery(query.ToString()) == 1)
            {
                return new JsonResult("Record updated");
            }
            return new JsonResult("This id couple doesn't exist.");
        } catch(MySqlConnector.MySqlException e)
        {
            return new JsonResult(e.Message);
        }
    }

    // UTILITIES
    private bool ThereIsId(int id)
    {
        StringBuilder query = new StringBuilder();
        query.AppendFormat("SELECT * FROM invoices WHERE id = {0}", id);
        try
        {
            JsonResult result = new JsonResult(Db.ExecuteReader(query.ToString()));
            Console.WriteLine(result.ToString());
            if (result.ToString() == "")
            {
                return true;
            }
            return false;
            
        } catch(MySqlConnector.MySqlException)
        {
            return false;
        }
    }

    private JsonResult GroupProductsUnderInvoices(List<object> invoicesProducts)
    {
        // Create a list of InvoiceWorker to get all data from invoicesProducts
        List<InvoiceWorker> invoiceWorkers = new List<InvoiceWorker>();

        List<int> groupedIdInvoices = new List<int>(); // Registers invoices ids already checked
        int idInvoice;

        foreach (IDictionary<string, object> record in invoicesProducts)
        {
            idInvoice = (int)record["IdInvoice"];

            // if the invoiceId isnt present, create a new InvoiceWorker with that id
            // and add data of the InvoicesProducts
            if (!groupedIdInvoices.Contains(idInvoice))
            {
                InvoiceWorker newInvoiceWorker = new InvoiceWorker(idInvoice);
                groupedIdInvoices.Add(idInvoice);                

                newInvoiceWorker.AddProductInInvoice(record);
                invoiceWorkers.Add(newInvoiceWorker);

                continue;
            }

            // if it is present, find the invoiceWorker with that id
            // and add data of the InvoicesProducts
            foreach (InvoiceWorker invoiceWorker in invoiceWorkers)
            {
                if (invoiceWorker.Id == idInvoice)
                {
                    invoiceWorker.AddProductInInvoice(record);
                }
            }
        }
        return new JsonResult(invoiceWorkers);
    }

}

