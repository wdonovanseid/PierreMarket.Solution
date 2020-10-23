using System.Collections.Generic;

namespace PierreMarket.Models
{
  public class Treat
  {

    public Treat()
    {
      this.Flavors = new HashSet<FlavorTreat>();
      this.Orders = new HashSet<OrderTreat>();
    }

    public int TreatId { get; set; }
    public string TreatName { get; set; }
    public string TreatDetails { get; set; }
    public double TreatCost { get; set; }
    public virtual ICollection<FlavorTreat> Flavors { get; set; }
    public virtual ICollection<OrderTreat> Orders { get; set;}

  }
}