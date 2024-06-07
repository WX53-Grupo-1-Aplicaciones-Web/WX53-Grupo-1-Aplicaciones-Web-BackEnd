namespace _3.Data.Models;

public class Order: BaseModel
{
    public DateTime RequestDate { get; set; }
    public DateTime ShippingDate { get; set; }
    public string Status { get; set; }
    public string DeliveryAddress { get; set; }
}