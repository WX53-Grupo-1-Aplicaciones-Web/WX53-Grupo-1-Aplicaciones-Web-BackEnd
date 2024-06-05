namespace _3.Data.Models;

public class Artisan : BaseModel
{
    public string Name  { get; set; }
    public string LastName { get; set; }
    public long Phone { get; set; }
    public string Email { get; set; }
}