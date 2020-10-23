using System.Collections.Generic;

namespace PierreMarket.Models
{
  public class Order
  {
    public Order()
    {
      this.Treats = new HashSet<Treat>();
    }

    public int OrderId { get; set; }
    public string OrderNotes { get; set; }
    public virtual ApplicationUser User { get; set; }

    public ICollection<Treat> Treats { get;}
  }
}