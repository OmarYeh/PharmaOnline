using Final_Project.Data;
using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Xml.Linq;

namespace Final_Project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;
        ApplicationDbContext _context;
        public ProductsController(ILogger<ProductsController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            /* ProductCategory PVD = new ProductCategory
             {
                 Categories = _context.Categories.ToList(),
                 Items = _context.Items.ToList(),
             };*/
            ViewData["Categories"] = _context.Categories.ToList();
            ViewData["Items"] = _context.Items.ToList();
            return View();
        }

        public IActionResult SingularProduct(int Id)
        {
            Item ItemS = _context.Items.Include(i => i.Category).Where(I => I.Id == Id).FirstOrDefault();
            return View(ItemS);
        }

        public IActionResult SearchProduct(String SearchContent,String Name)
        {
           
            if (!string.IsNullOrEmpty(Name))
            {
                Category C = _context.Categories.Where(c => c.Name == Name).FirstOrDefault();
                ViewData["Categories"] = _context.Categories.ToList();
                ViewData["Items"] = _context.Items.Where(i => i.Name.Contains(SearchContent) && i.CategoryId == C.Id).ToList();
                ViewData["SearchValue"] = SearchContent;
                ViewData["Name"] = Name;
            }
            else
            {

                ViewData["Categories"] = _context.Categories.ToList();
                ViewData["Items"] = _context.Items.Where(i => i.Name.Contains(SearchContent)).ToList();
                ViewData["SearchValue"] = SearchContent;
                
            }
            return View();
        }
        public IActionResult FilterProduct(String Name)
        {
            Category C = _context.Categories.Include(c=>c.Items).Where(c => c.Name == Name).FirstOrDefault();
            ViewData["Categories"] = _context.Categories.ToList();
            // ViewData["Items"] = _context.Items.Where(i => i.CategoryId == C.Id).ToList();
            ViewData["Items"] = C.Items.ToList();
            ViewData["Name"] = Name;
            return View("index");
        }
        public IActionResult GetItemsByPriceRange(int priceRange,string Name)
        {
           
            if (priceRange == 5)
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    Category C = _context.Categories.Include(c => c.Items).Where(c => c.Name == Name).FirstOrDefault();
                    ViewData["Categories"] = _context.Categories.ToList();
                    ViewData["Items"] = C.Items.ToList();
                }
                else
                {
                    ViewData["Categories"] = _context.Categories.ToList();
                    ViewData["Items"] = _context.Items.ToList();

                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    Category C = _context.Categories.Include(c=>c.Items).Where(c=>c.Name == Name).FirstOrDefault();
                    ViewData["Categories"] = _context.Categories.ToList();
                    ViewData["Items"] = C.Items.Where(i => i.Price <= priceRange).ToList();
                }
                else
                {
                    ViewData["Categories"] = _context.Categories.ToList();
                    ViewData["Items"] = _context.Items.Where(i => i.Price <= priceRange).ToList();
                }

                
            }
            return PartialView("Index");
        }

        public IActionResult GetItemsByPriceRangeSearch(int priceRange, String SearchContent, String Name)
        {

            if (priceRange == 5)
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    Category C = _context.Categories.Where(c => c.Name == Name).FirstOrDefault();
                    ViewData["Categories"] = _context.Categories.ToList();
                    ViewData["Items"] = _context.Items.Where(i => i.Name.Contains(SearchContent) && i.CategoryId == C.Id).ToList();
                }
                else
                {
                    ViewData["Categories"] = _context.Categories.ToList();
                    ViewData["Items"] = _context.Items.Where(i => i.Name.Contains(SearchContent)).ToList();
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(Name))
                {
                    Category C = _context.Categories.Where(c => c.Name == Name).FirstOrDefault();
                    ViewData["Categories"] = _context.Categories.ToList();
                    ViewData["Items"] = _context.Items.Where(i => i.Name.Contains(SearchContent) && i.CategoryId == C.Id && i.Price <= priceRange).ToList();
                    ViewData["SearchValue"] = SearchContent;
                }
                else
                {

                    ViewData["Categories"] = _context.Categories.ToList();
                    ViewData["Items"] = _context.Items.Where(i => i.Name.Contains(SearchContent) && i.Price <= priceRange).ToList();
                    ViewData["SearchValue"] = SearchContent;
                }
                


            }
            return PartialView("SearchProduct");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}