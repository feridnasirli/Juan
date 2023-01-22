using Juan.Helpers;
using Juan.Models;
using Microsoft.AspNetCore.Mvc;

namespace Juan.Areas.manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly JuanContext _juanContext;
        private readonly IWebHostEnvironment _web;

        public SliderController(JuanContext juanContext,IWebHostEnvironment web)
        {
            _juanContext = juanContext;
            this._web = web;
        }
        public IActionResult Index()
        {
            List<Slider> slider = _juanContext.Sliders.ToList();
            return View(slider);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Encag png ve jpeg olar!");
                return View();
            }
            slider.Image = FileManager.SaveFile(_web.WebRootPath, "uploads/sliders", slider.ImageFile);
            _juanContext.Sliders.Add(slider);
            _juanContext.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult Update(int id) 
        {
           Slider slider= _juanContext.Sliders.FirstOrDefault(x => x.Id == id);
           if (slider == null) return View("Error");

            return View(slider);
        }
        [HttpPost]
        public IActionResult Update(Slider slider)
        {
           Slider exslider= _juanContext.Sliders.FirstOrDefault(x=>x.Id==slider.Id);
            if (exslider == null) return View("Error");
            if (exslider.Image != null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Encag png ve jpeg olar!");
                    return View();
                }
                string name = FileManager.SaveFile(_web.WebRootPath, "uploads/sliders", slider.ImageFile);
                FileManager.DeleteFile(_web.WebRootPath, "uploads/sliders", exslider.Image);
                exslider.Image = name;
            }
            exslider.Title1 = slider.Title1;
            exslider.Title2 = slider.Title2;
            exslider.RedirectUrl= slider.RedirectUrl;
            exslider.RedirectUrlText= slider.RedirectUrlText;
            _juanContext.SaveChanges();
            return RedirectToAction("Index");
        }

          public IActionResult Delete(int id)
        {
            Slider slider =_juanContext.Sliders.FirstOrDefault(x=>x.Id== id);
            if (slider == null) return NotFound();
            if (slider.ImageFile != null)
            {
                FileManager.DeleteFile(_web.WebRootPath, "uploads/sliders", slider.Image);
            }
            _juanContext.Sliders.Remove(slider);
            _juanContext.SaveChanges();
            return Ok();
        }
    }
}
