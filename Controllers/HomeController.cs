using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BookApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookApp.Controllers;

public class HomeController : Controller
{
    public HomeController()
    {
        
    }

    public IActionResult Index(string searchString, string category)
    {
        var books = Repository.Products;
        if(!String.IsNullOrEmpty(searchString)){
            ViewBag.searchString = searchString;
            books = books.Where(b=>b.BookName!.ToLower().Contains(searchString)).ToList(); //girilen verileri küçük harfe çevirip aramak için "Tolower" kullanıldı.
        }
        if(!String.IsNullOrEmpty(category)){
            books = books.Where(b=>b.CategoryId == int.Parse(category)).ToList();
        }
        //ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");

        var model = new ProductViewModel
        {
            Products = books,
            Categories = Repository.Categories,
            SlectedCategory = category
        };
        return View(model);
    }
    [HttpGet]
    public IActionResult Admin(){
        return View(Repository.Products); 
    }
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View();
    }
    [HttpPost]
    public async Task <IActionResult> Create(ProductBook model, IFormFile imageFile)
    {
        var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
        var extensions = Path.GetExtension(imageFile.FileName);
        var RandomFileName = string.Format($"{Guid.NewGuid().ToString()}{extensions}");
        var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",RandomFileName);

        if(imageFile != null)
        {
            if(!allowedExtensions.Contains(extensions))
            {
                ModelState.AddModelError("","Please choose jpg, png or  jpeg file");
            }
        }
        if(ModelState.IsValid)
        {
            if(imageFile != null)
            {
            using(var stream = new FileStream(filePath,FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            }
        model.Image = RandomFileName;
        model.BookId = Repository.Products.Count +1;
        Repository.CreateBook(model);
        return RedirectToAction("Admin");
        }       
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(model);
    }
    public IActionResult Edit(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
      var entity = Repository.Products.FirstOrDefault(b=>b.BookId == id);
      if(entity == null)
      {
        return NotFound();
      }
      ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
      return View(entity);
    }
    [HttpPost]
    public async Task<IActionResult>Edit(int id,ProductBook model,IFormFile imageFile)
    {
        if(id != model.BookId)
        {
            return NotFound();
        }
        if(ModelState.IsValid)
        {
            if(imageFile != null)
            {
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            var extensions = Path.GetExtension(imageFile.FileName);
            var RandomFileName = string.Format($"{Guid.NewGuid().ToString()}{extensions}");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",RandomFileName);
               if(imageFile != null)
            {
            using(var stream = new FileStream(filePath,FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }
            model.Image = RandomFileName;
            }
            Repository.EditBook(model);
            return RedirectToAction("Admin");
            }
        }
        ViewBag.Categories = new SelectList(Repository.Categories,"CategoryId","Name");
        return View(model);
    }

        public IActionResult Details(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(b=>b.BookId == id);
        if(entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }
    [HttpPost]
    public IActionResult Delete(int id, int BookId)
    {
        if(id != BookId)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(b=>b.BookId == BookId);
        if(entity == null)
        {
            return NotFound();
        }
        Repository.DeleteBook(entity);
        return RedirectToAction("Admin");
    }
     [HttpGet]
    public IActionResult Delete(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(b=>b.BookId == id);
        if(entity == null)
        {
            return NotFound();
        }
        return View(entity);
    }
}
