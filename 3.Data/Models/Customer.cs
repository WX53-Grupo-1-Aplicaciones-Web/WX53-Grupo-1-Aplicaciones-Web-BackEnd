namespace _3.Data.Models;

public class Customer: BaseModel
{
    public string Name  { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public long Phone { get; set; }
}