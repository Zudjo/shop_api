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

    [HttpGet("invoices-products/{invoice-id}")]
    public JsonResult getInvoicesProductsById(int id)
    {
        if (!ThereIsId(id))
        {
            return new JsonResult("This invoice id doesn't exists.");
        }
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT * FROM invoices_products WHERE id_invoice = {0}", id);
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
        query.AppendFormat("INSERT INTO invoices_products VALUES ({0}, '{1}', {2})",
            invoices_products.IdInvoice, invoices_products.IdProduct, invoices_products.Quantity);
        try
        {
            return new JsonResult(Db.ExecuteNonQuery(query.ToString()));
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

