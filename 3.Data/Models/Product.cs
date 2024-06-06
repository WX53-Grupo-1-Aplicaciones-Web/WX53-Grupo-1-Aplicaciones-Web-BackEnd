namespace _3.Data.Models;

public class Product : BaseModel
{
    public string Name { get; set; }
    public double Unit_Price { get; set; } 
    public double Stock { get; set; } 
    public string Description { get; set; }//
    public string Category { get; set; }//
    public string Image { get; set; }//
}