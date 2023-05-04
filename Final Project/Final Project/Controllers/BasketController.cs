using Final_Project.Data;
using Final_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Final_Project.Controllers
{
    //when the basket is empty tthereis an error
    public class BasketController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext _context;
        UserManager<IdentityUser> _userManager;
        public BasketController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        
        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                ViewData["NoUser"] = "there is no user logged in";
                return  View();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            Basket basket = _context.Baskets.Include(b => b.BasketItems).ThenInclude(bi => bi.Item).Where(b => b.UserId == userId).FirstOrDefault();
            if (basket == null)
            {
                Basket b = new Basket();
                b.UserId = userId;
                basket = b;
                _context.Baskets.Add(b);
                await _context.SaveChangesAsync();
                
            }
            ViewData["basketItems"] = basket.BasketItems.ToList() ?? new List<BasketItems>();
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> createAsync(int ItemId, int Quntity)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);
            Basket basket =  _context.Baskets.Where(b => b.UserId == userId).FirstOrDefault();
            Item item = _context.Items.Where(i => i.Id == ItemId).FirstOrDefault();
            BasketItems ifbi = _context.BasketItems.Where(bi => bi.BasketId==basket.Id && bi.ItemId == ItemId).FirstOrDefault();
            if (ifbi == null)
            {
                BasketItems bi = new BasketItems();
                bi.ItemId = ItemId;
                bi.Quantity = Quntity;
                bi.Item = item;
                bi.Basket = basket;
                bi.BasketId = basket.Id;
                _context.BasketItems.Add(bi);
                await _context.SaveChangesAsync();
            }
            else
            {
                ifbi.Quantity = ifbi.Quantity + Quntity;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> DeleteItemAsync(int ItemId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);
            Basket basket = _context.Baskets.Where(b => b.UserId == userId).FirstOrDefault();
            Item item = _context.Items.Where(i => i.Id == ItemId).FirstOrDefault();
            BasketItems ifbi = _context.BasketItems.Where(bi => bi.BasketId == basket.Id && bi.ItemId == ItemId).FirstOrDefault();
            if (ifbi != null)
            {
                _context.BasketItems.Remove(ifbi);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> EditItemAsync(int ItemId, int Quntity)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = await _userManager.GetUserIdAsync(user);
            Basket basket = _context.Baskets.Where(b => b.UserId == userId).FirstOrDefault();
            Item item = _context.Items.Where(i => i.Id == ItemId).FirstOrDefault();
            BasketItems ifbi = _context.BasketItems.Where(bi => bi.BasketId == basket.Id && bi.ItemId == ItemId).FirstOrDefault();
            if (ifbi != null)
            {
                ifbi.Quantity = Quntity;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}
