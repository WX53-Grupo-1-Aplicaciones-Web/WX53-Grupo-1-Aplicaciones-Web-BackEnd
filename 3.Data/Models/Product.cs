namespace _3.Data.Models;

public class Product : BaseModel
{
    public string Name { get; set; }
    public double Unit_Price { get; set; } 
    public double Stock { get; set; } 
}