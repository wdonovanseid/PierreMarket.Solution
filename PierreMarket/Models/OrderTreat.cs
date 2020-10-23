namespace PierreMarket.Models
{
  public class OrderTreat
  {
    public int OrderTreatId { get; set; }
    public int OrderId { get; set; }
    public int TreatId { get; set; }
    public Order Order { get; set; }
    public Treat Treat { get; set; }
  }
}