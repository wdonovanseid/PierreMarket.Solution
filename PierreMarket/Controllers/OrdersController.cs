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
      .ThenInclude(x => x.Treat)
      .FirstOrDefault(x => x.OrderId == id);
      return View(model);
    }

    public ActionResult AddTreat(int id)
    {
      Order model = _db.Orders.FirstOrDefault(x => x.OrderId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "TreatName");
      return View(model);
    }

    [HttpPost]
    public ActionResult AddTreat(Order objectGettingAdd, int TreatId)
    {
      if (TreatId != 0)
      {
        _db.OrderTreat.Add(new OrderTreat() { TreatId = TreatId, OrderId = objectGettingAdd.OrderId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = objectGettingAdd.OrderId});
    }

    [HttpPost]
    public ActionResult DeleteTreat(int OrderTreatId)
    {
      OrderTreat joinEntry = _db.OrderTreat.FirstOrDefault(entry => entry.OrderTreatId == OrderTreatId);
      _db.OrderTreat.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

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