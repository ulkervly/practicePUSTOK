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
        public IActionResult Create(Slider slide)
        {
            string fileName = slide.Image.FileName;

            if (slide.Image.ContentType != "image/png" && slide.Image.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("Image", "please select correct file type");
            }

            if (slide.Image.Length > 1048576)
            {
                ModelState.AddModelError("Image", "file size should be more lower than 1mb ");
            }

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }

            fileName = Guid.NewGuid().ToString() + fileName;

            string path = $"C:\\Users\\elvin\\OneDrive\\Documents\\Sənədlər\\ForFuture-tasks\\MVC-PustokAdmin\\MVC.Practice\\MVC.PracticeTask-1\\wwwroot\\uploads\\bg-slide\\{fileName}";

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                slide.Image.CopyTo(fileStream);
            }

            slide.ImageUrL= fileName;

         


            _context.Sliders.Add(slide);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Slider slide = _context.Sliders.FirstOrDefault(x => x.id == id);
            return View(slide);
        }

        [HttpPost]
        public IActionResult Update(Slider slide)
        {

            Slider existSlide = _context.Sliders.FirstOrDefault(x => x.Id == slide.Id);



            existSlide.Title = slide.Title;
            existSlide.Description = slide.Description;
            existSlide.ImageUrL = slide.Image.FileName;
            existSlide.RedirectUrl = slide.RedirectUrl;
            existSlide.Image = slide.Image;


            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Slider slide = _context.Sliders.FirstOrDefault(x => x.id == id);
            return View(slide);
        }

        [HttpPost]
        public IActionResult Delete(Slider slide)
        {

            Slider existSlide = _context.Sliders.FirstOrDefault(x => x.id == slide.id);

            string fileName = existSlide.ImageUrL;
            string path = $"C:\\Users\\\\{fileName}";

            if (existSlide.ImageUrL != null)
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            _context.Sliders.Remove(existSlide);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
