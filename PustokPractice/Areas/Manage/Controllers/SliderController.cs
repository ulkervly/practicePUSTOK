using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokPractice.DAL;
using PustokPractice.Models;

namespace PustokPractice.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            List<Slider> Sliders = _context.Sliders.ToList();
            return View(Sliders);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {

            if (!ModelState.IsValid) return View();

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.id == id);
            if (slider == null) return NotFound();

            return View(slider);
        }

        [HttpPost]
        public IActionResult Delete(Slider slider)
        {
            Slider existService1 = _context.Sliders.FirstOrDefault(x => x.id == slider.id);
            if (existService1 == null)
            {
                return NotFound();
            }
            _context.Sliders.Remove(existService1);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.id == id);
            return View(slider);
        }

        [HttpPost]
        public IActionResult Update(Slider slider)
        {

            Service existService1 = _context.Services.FirstOrDefault(x => x.id == slider.id);
            if (existService1 == null)
            {
                return NotFound();
            }
            existService1.Title = slider.Title;
            existService1.Description = slider.Description;
            

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
