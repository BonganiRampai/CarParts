using CarParts.Data;
using CarParts.Models;
using CarParts.Data.DataAccess;
using CarParts.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CarParts.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IRepositoryWrapper _repo;
        public HomeController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public int iPageSize=3;
        [AllowAnonymous]
        public IActionResult List(int carPartPage = 1)
        {
            IEnumerable<CarPart> carparts;

            int iTotalCarParts = _repo.CarPart.FindAll().Count();
            carparts = _repo.CarPart.GetCarPartsWithOptions(new QueryOptions<CarPart>
                {
                    PageNumber = carPartPage,
                    PageSize = iPageSize
                });
            
            var model = new CarPartListViewModel
            {
                CarParts = carparts,
                PagingInfo = new PagingInfoViewModel
                {
                    CurrentPage = carPartPage,
                    ItemsPerPage = iPageSize,
                    TotalItems = iTotalCarParts
                }
            };

            // bind ViewModel to view
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            CarPart carpart = _repo.CarPart.GetById(id);
            if (carpart != null)
            {
                // use ViewBag to pass data to view
                ViewBag.ImageFilename = carpart.Name + ".jpg";

                // bind Carpart to view
                return View(carpart);
            }
            else
                return RedirectToAction("List");
        }
        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

    }
}
