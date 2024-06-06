namespace _3.Data.Models;

public class Personalization : BaseModel
{
    public string Description { get; set; }
    public float AdditionalCost { get; set; }
    public bool State {  get; set; }
}