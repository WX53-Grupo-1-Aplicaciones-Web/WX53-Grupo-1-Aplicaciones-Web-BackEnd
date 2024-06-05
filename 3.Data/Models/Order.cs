namespace _3.Data.Models;

public class Order: BaseModel
{
    public DateTime request_date { get; set; }
    public DateTime shipping_date { get; set; }
    public string status { get; set; }
    public string delivery_address { get; set; }
}