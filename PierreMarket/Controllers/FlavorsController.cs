using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PierreMarket.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

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
      List<Flavor> model = _db.Flavors.OrderBy(x => x.FlavorName).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor newObject)
    {
      _db.Flavors.Add(newObject);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Flavor model = _db.Flavors
      .Include(x => x.Treats)
      .ThenInclude(join => join.Treat)
      .FirstOrDefault(x => x.FlavorId == id);
      return View(model);
    }

    public ActionResult AddTreat(int id)
    {
      Flavor model = _db.Flavors.FirstOrDefault(x => x.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
      return View(model);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor objectGettingAdd, int TreatId)
    {
      if (TreatId != 0)
        {
          if(_db.FlavorTreat.Where(x => x.FlavorId == objectGettingAdd.FlavorId && x.TreatId == TreatId).ToHashSet().Count == 0)
          {
            _db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = objectGettingAdd.FlavorId });
          }
        }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = objectGettingAdd.FlavorId});
    }

    [HttpPost]
    public ActionResult DeleteTreat(int FlavorTreatId)
    {
      FlavorTreat joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == FlavorTreatId);
      _db.FlavorTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      Flavor model = _db.Flavors.FirstOrDefault(x => x.FlavorId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Flavor edittedObject)
    {
      _db.Entry(edittedObject).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      Flavor model = _db.Flavors.FirstOrDefault(x => x.FlavorId == id);
      return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Flavor model = _db.Flavors.FirstOrDefault(x => x.FlavorId == id);
      _db.Flavors.Remove(model);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}