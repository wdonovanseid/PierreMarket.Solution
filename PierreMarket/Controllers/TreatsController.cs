using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PierreMarket.Models;
using System.Linq;

namespace PierreMarket.Controllers
{
  public class TreatsController : Controller
  {
    private readonly PierreMarketContext _db;

    public TreatsController(PierreMarketContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }
  }
}