using Juan.Helpers;
using Juan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Juan.Areas.manage.Controllers
{
    [Area("manage")]
    public class ShoeController : Controller
    {
        private readonly JuanContext _juanContext;
        private readonly IWebHostEnvironment _web;

        public ShoeController(JuanContext juanContext,IWebHostEnvironment web)
        {
            _juanContext = juanContext;
            _web = web;
        }
        public IActionResult Index()
        {
            List<Shoe> shoes=_juanContext.Shoes.ToList();
            return View(shoes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Shoe shoe)
        {
            if (shoe.ImageFile.ContentType != "image/png" && shoe.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Encag png ve jpeg olar!");
                return View();
            }
            shoe.Image=FileManager.SaveFile(_web.WebRootPath,"uploads/shoes",shoe.ImageFile);
            _juanContext.Shoes.Add(shoe);
            _juanContext.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Update(int id) 
        {
            Shoe shoe =_juanContext.Shoes.FirstOrDefault(x=>x.Id== id);
            if (shoe == null) return View("Error");

            return View(shoe);
        }
        [HttpPost]
        public IActionResult Update(Shoe shoe)
        {
            Shoe exshoe= _juanContext.Shoes.FirstOrDefault(x=>x.Id== shoe.Id);
            if (exshoe == null) return NotFound();
            if (exshoe.Image != null)
            {
                if (shoe.ImageFile.ContentType != "image/png" && shoe.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Encag png ve jpeg olar!");
                    return View();
                }
                string name = FileManager.SaveFile(_web.WebRootPath, "uploads/shoes", shoe.ImageFile);
                FileManager.DeleteFile(_web.WebRootPath, "uploads/shoes", exshoe.Image);
                exshoe.Image = name;
            }
            exshoe.SalePrice = shoe.SalePrice;
            exshoe.Name = shoe.Name;
            exshoe.DisCountPrice = shoe.DisCountPrice;
            _juanContext.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            Shoe shoe = _juanContext.Shoes.FirstOrDefault(x=>x.Id== id);
            if (shoe == null) return NotFound();
            if (shoe.Image != null)
            {
                FileManager.DeleteFile(_web.WebRootPath,"uploads/shoes",shoe.Image);
            }
            _juanContext.Shoes.Remove(shoe);
            _juanContext.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
