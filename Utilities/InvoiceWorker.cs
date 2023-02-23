using shop_api.Models;

namespace shop_api.Utilities;

public class InvoiceWorker : Invoice
{
    public List<ProductInInvoice> ProductsInInvoice { get; set; }
    public int Total { get; set; }
    
    public InvoiceWorker(int idInvoice)
    {
        Id = idInvoice;
        ProductsInInvoice = new List<ProductInInvoice>();
        Total = 0;
    }

    public void AddProductInInvoice(ProductInInvoice productInInvoice)
    {
        ProductsInInvoice.Add(productInInvoice);
        Total += productInInvoice.GetSubTotal();
    }

    public void AddProductInInvoice(IDictionary<string, object> record)
    {
        ProductInInvoice newProductInInvoice = new ProductInInvoice(
            (int)record["IdProduct"],
            (string)record["Name"],
            (int)record["Price"],
            (int)record["Quantity"]
        );

        ProductsInInvoice.Add(newProductInInvoice);
        Total += newProductInInvoice.GetSubTotal();
    }

}