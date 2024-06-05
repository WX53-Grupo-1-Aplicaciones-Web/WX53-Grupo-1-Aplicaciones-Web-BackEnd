namespace _1.API.Response;

public class OrderResponse
{
    public int Id { set; get; }
    public DateTime RequestDate { get; set; }
    public DateTime ShippingDate { get; set; }
    public string Status { get; set; }
    public string ShippingAddress { get; set; }
}