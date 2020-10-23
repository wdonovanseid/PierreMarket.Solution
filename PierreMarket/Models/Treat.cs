using System.Collections.Generic;

namespace PierreMarket.Models
{
  public class Treat
  {

    public Treat()
    {
      this.Flavors = new HashSet<FlavorTreat>();
    }

    public int TreatId { get; set; }
    public string TreatName { get; set; }
    public string TreatDetails { get; set; }
    public virtual ICollection<FlavorTreat> Flavors { get; set; }

  }
}