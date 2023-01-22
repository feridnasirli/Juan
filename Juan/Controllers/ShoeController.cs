using Juan.Models;
using Juan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Juan.Controllers
{
	public class ShoeController : Controller
	{
		private readonly JuanContext _juanContext;

		public ShoeController(JuanContext juanContext)
		{
			_juanContext = juanContext;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddCart(int shoeId)
		{
			if (!_juanContext.Shoes.Any(x => x.Id == shoeId)) return NotFound();
			List<CartItemsViewModel> items = new List<CartItemsViewModel>();
			CartItemsViewModel item = new CartItemsViewModel();
			string shoeSTR = HttpContext.Request.Cookies["Basket"];
			if(shoeSTR != null)
			{
				items=JsonConvert.DeserializeObject<List<CartItemsViewModel>>(shoeSTR);
				item=items.FirstOrDefault(x=>x.ShoeId== shoeId);
				if(item!=null)
				{
					item=new CartItemsViewModel
					{
						ShoeId= shoeId,
						Count= 1,
					};
					items.Add(item);
				}
			}
		   shoeSTR=JsonConvert.SerializeObject(items);
		   HttpContext.Response.Cookies.Append("Basket", shoeSTR);
		   return RedirectToAction("Index","home");
		}

		public IActionResult GetCart()
		{
			List<CartItemsViewModel> items = new List<CartItemsViewModel>();
			string shoeSTR = HttpContext.Request.Cookies["Basket"];
			if(shoeSTR != null)
			{
				items=JsonConvert.DeserializeObject<List<CartItemsViewModel>>(shoeSTR);
			}
			return Json(items);
		}
	}
}
