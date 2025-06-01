using CarParts.Data;
using CarParts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CarParts.Controllers
{
    [Authorize(Roles = "Customer")]
    public class OrderController : Controller
    {
        private IRepositoryWrapper _repo;
        public OrderController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            var userOrder = _repo.Order.
                GetUserOrdersWithCategoryDetails(User.Identity.Name);
            return View(userOrder);
        }
        [HttpGet]
        public IActionResult OrderParts()
        {
            PopulatedCategoryDLL();
            return View();
        }
        [HttpPost]
        public IActionResult OrderParts(Order order)
        {
            order.CustomerName = User.Identity.Name;
            order.OrderDate = DateTime.Now;
            order.Status = "Pending";
            if(ModelState.IsValid)
            {
                try
                {
                    _repo.Order.Create(order);
                    TempData["Message"] = $"You have successfully placed an order.";
                    _repo.Save();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to place you order.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid order values");
            }

            PopulatedCategoryDLL();
            return View();
        }
        [HttpPost]
        public IActionResult CancelOrder(int id)
        {
            var order = _repo.Order.GetById(id);
            if(order != null)
            {
                try
                {
                    _repo.Order.Delete(order);
                    TempData["Message"] = $"Your order has been cancelled.";
                    _repo.Save();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to delete the order.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid order, the order does not exist.");
            }
            return View();

        }

        public void PopulatedCategoryDLL(object selectedCategory = null)
        {
            ViewBag.Names = new SelectList(_repo.Category.FindAll().
                OrderBy(c => c.CategoryName), "CategoryID", "CategoryName", selectedCategory);
        }
    }
}
