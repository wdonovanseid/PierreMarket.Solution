using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PierreMarket.Models;
using System.Linq;

namespace PierreMarket.Controllers
{
  public class FlavorsController : Controller
  {
    private readonly PierreMarketContext _db;

    public FlavorsController(PierreMarketContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }
  }
}