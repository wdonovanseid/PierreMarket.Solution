using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PierreMarket.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
      List<Treat> model = _db.Treats.OrderBy(x => x.TreatName).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Treat model = _db.Treats
      .Include(x => x.Flavors)
      .ThenInclude(join => join.Flavor)
      .FirstOrDefault(x => x.TreatId == id);
      return View(model);
    }

    public ActionResult AddFlavor(int id)
    {
      Treat model = _db.Treats.FirstOrDefault(x => x.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "FlavorName");
      return View(model);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      if (FlavorId != 0)
        {
          if(_db.FlavorTreat.Where(x => x.FlavorId == FlavorId && x.TreatId == treat.TreatId).ToHashSet().Count == 0)
          {
            _db.FlavorTreat.Add(new FlavorTreat() { TreatId = treat.TreatId, FlavorId = FlavorId });
          }
        }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = treat.TreatId});
    }

    [HttpPost]
    public ActionResult DeleteFlavor(int FlavorTreatId)
    {
      FlavorTreat joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == FlavorTreatId);
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Treat model = _db.Treats.FirstOrDefault(x => x.TreatId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Treat edittedObject)
    {
      _db.Entry(edittedObject).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      Treat model = _db.Treats.FirstOrDefault(x => x.TreatId == id);
      return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Treat model = _db.Treats.FirstOrDefault(x => x.TreatId == id);
      _db.Treats.Remove(model);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}