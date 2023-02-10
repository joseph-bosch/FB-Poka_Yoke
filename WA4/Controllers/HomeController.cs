using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WA4.Data;
using WA4.Controllers;
using WA4.Models;


namespace WA4.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //CategoryController cc = new CategoryController(_db);
            
            IEnumerable<Category> objCategoryList = _db.Categories1;
            return View(objCategoryList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchResults(String SearchPhrase)
        {
            if (SearchPhrase == null)
            {
                return NotFound();
            }
            return View("ShowSearch", _db.Categories1.Where(j => j.MaterialNumber.Contains(SearchPhrase)));


            //return View();

        }

        public IActionResult UpdateDb()
        {

            TempData["success"] = "Database Updated Successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectedPN(String nameobj)
        {
            var categoryFromDb = _db.Categories1.Where(j => j.MaterialNumber == nameobj);

            return View("SelectedPN", categoryFromDb);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult checker(SetVal data1)
        {

            return null;

        }
    }
}