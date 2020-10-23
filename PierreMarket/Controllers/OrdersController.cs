using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PierreMarket.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierreMarket.Controllers
{
  [Authorize]
  public class OrdersController : Controller
  {
    private readonly PierreMarketContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public OrdersController(UserManager<ApplicationUser> userManager, PierreMarketContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userOrders = _db.Orders.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userOrders);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Order newObject)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      newObject.User = currentUser;
      _db.Orders.Add(newObject);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Order model = _db.Orders
      .Include(x => x.Treats)
      .FirstOrDefault(x => x.OrderId == id);
      return View(model);
    }

    // public ActionResult AddTreat(int id)
    // {
    //   Flavor model = _db.Flavors.FirstOrDefault(x => x.FlavorId == id);
    //   ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
    //   return View(model);
    // }

    // [HttpPost]
    // public ActionResult AddTreat(Flavor objectGettingAdd, int TreatId)
    // {
    //   if (TreatId != 0)
    //     {
    //       if(_db.FlavorTreat.Where(x => x.FlavorId == objectGettingAdd.FlavorId && x.TreatId == TreatId).ToHashSet().Count == 0)
    //       {
    //         _db.FlavorTreat.Add(new FlavorTreat() { TreatId = TreatId, FlavorId = objectGettingAdd.FlavorId });
    //       }
    //     }
    //   _db.SaveChanges();
    //   return RedirectToAction("Details", new { id = objectGettingAdd.FlavorId});
    // }

    // [HttpPost]
    // public ActionResult DeleteTreat(int FlavorTreatId)
    // {
    //   FlavorTreat joinEntry = _db.FlavorTreat.FirstOrDefault(entry => entry.FlavorTreatId == FlavorTreatId);
    //   _db.FlavorTreat.Remove(joinEntry);
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }

    public ActionResult Edit(int id)
    {
      Order model = _db.Orders.FirstOrDefault(x => x.OrderId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Order edittedObject)
    {
      _db.Entry(edittedObject).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Order model = _db.Orders.FirstOrDefault(x => x.OrderId == id);
      return View(model);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Order model = _db.Orders.FirstOrDefault(x => x.OrderId == id);
      _db.Orders.Remove(model);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}