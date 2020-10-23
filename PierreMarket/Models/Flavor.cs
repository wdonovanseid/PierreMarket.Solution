using System.Collections.Generic;

namespace PierreMarket.Models
{
  public class Flavor
  {

    public Flavor()
    {
      this.Treats = new HashSet<FlavorTreat>();
    }

    public int FlavorId { get; set; }
    public string FlavorName { get; set; }
    public string FlavorDetails { get; set; }
    public virtual ICollection<FlavorTreat> Treats { get; set; }

  }
}