using Juan.Models;
using Juan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Juan.Controllers
{
	public class HomeController : Controller
	{
        private readonly JuanContext _juanContext;

        public HomeController(JuanContext juanContext)
		{
            _juanContext = juanContext;
        }
		
		public IActionResult Index()
		{
			HomeViewModel homeVM = new HomeViewModel
			{
				sliders = _juanContext.Sliders.ToList(),
				shoes=_juanContext.Shoes.ToList(),
			};
			
			return View(homeVM);
		}

	}
}