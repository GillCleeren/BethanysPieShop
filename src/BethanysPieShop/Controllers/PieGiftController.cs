using System.Linq;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class PieGiftController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly IOrderRepository _orderRepository;

        public PieGiftController(IPieRepository pieRepository, IOrderRepository orderRepository)
        {
            _pieRepository = pieRepository;
            _orderRepository = orderRepository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[IgnoreAntiforgeryToken]
        public IActionResult Index(PieGiftOrder pieGiftOrder)
        {
            var pieOfTheMonth = _pieRepository.PiesOfTheWeek.FirstOrDefault();

            if (pieOfTheMonth != null)
            {
                pieGiftOrder.Pie = pieOfTheMonth;
                _orderRepository.CreatePieGiftOrder(pieGiftOrder);
                return RedirectToAction("PieGiftOrderComplete");
            }

            return View();
        }

        public IActionResult PieGiftOrderComplete()
        {
            ViewBag.PieGiftOrderCompleteMessage = HttpContext.User.Identity.Name +
                                                  ", thanks for the order. Your friend will soon receive the pie!";
            return View();
        }
    }
}
