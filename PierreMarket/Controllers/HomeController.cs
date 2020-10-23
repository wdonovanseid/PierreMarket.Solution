using Microsoft.AspNetCore.Mvc;
using PierreMarket.Models;
using System.Collections.Generic;
using System.Linq;

namespace PierreMarket.Controllers
{
  public class HomeController : Controller
  {

    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    private readonly PierreMarketContext _db;
    public HomeController(PierreMarketContext db)
    {
      _db = db;
    }

    public ActionResult AllFlavAndTreat()
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Flavor> allFlav = _db.Flavors.OrderBy(x => x.FlavorName).ToList();
      List<Treat> allTreat = _db.Treats.OrderBy(x => x.TreatName).ToList();
      model.Add("flavors", allFlav);
      model.Add("treats", allTreat);
      return View(model);
    }

  }
}