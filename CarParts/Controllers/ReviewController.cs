using CarParts.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarParts.Controllers
{
    [Authorize(Roles = "Staff")]
    public class ReviewController : Controller
    {
        private IRepositoryWrapper _repo;
        public ReviewController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public IActionResult Index()
        {
            return View(_repo.Order.FindAll().OrderByDescending(o=>o.OrderDate));
        }
        [HttpPost]
        public IActionResult ReviewOrder(int id, string status)
        {
            var order = _repo.Order.GetById(id);
            if (order != null)
            {
                try
                {
                    order.Status = status;
                    _repo.Order.Update(order);
                    TempData["Message"] = $"The car part is {status}";
                    _repo.Save();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to review an oder.");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
