using IMS.Data;
using IMS.Models;
using IMS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace IMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMSDbContext imsDbContext;

        public HomeController(ILogger<HomeController> logger, IMSDbContext imsDbContext)
        {
            _logger = logger;
            this.imsDbContext = imsDbContext;
        }

        public IActionResult Index()
        {
            int insuranceCount = imsDbContext.Insurances.Count();
            int clientCount = imsDbContext.Clients.Count();

            var viewModel = new HomeViewModel
            {
                InsuranceCount = insuranceCount,
                ClientCount = clientCount
            };

            return View(viewModel);
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
    }
}
