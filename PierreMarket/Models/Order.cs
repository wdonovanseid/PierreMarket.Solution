using System.Collections.Generic;

namespace PierreMarket.Models
{
  public class Order
  {
    public Order()
    {
      this.Treats = new HashSet<OrderTreat>();
    }

    public int OrderId { get; set; }
    public string OrderNotes { get; set; }
    public virtual ApplicationUser User { get; set; }

    public ICollection<OrderTreat> Treats { get;}
  }
}