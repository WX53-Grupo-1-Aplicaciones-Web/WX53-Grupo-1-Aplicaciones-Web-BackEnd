using System.Runtime.InteropServices.JavaScript;

namespace _1.API.Request;

public class OrderRequest
{
    public DateTime RequestDate { get; set; }
    public DateTime ShippingDate { get; set; }
    public string Status { get; set; }
    public string ShippingAddress { get; set; }
}