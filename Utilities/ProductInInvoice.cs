using shop_api.Models;

namespace shop_api.Utilities;

public class ProductInInvoice : Product
{
    public int Quantity { get; set; }
    
    public ProductInInvoice(int id, string name, int price, int quantity)
    {
        Id = id;
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public int GetSubTotal()
    {
        return Price * Quantity;
    }
}