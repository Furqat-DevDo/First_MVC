using System.Diagnostics;
using BookShop.DATA;
using BookShop.Models;
using Microsoft.AspNetCore.Mvc;
namespace BookShop.Controllers;
public class CategoryController:Controller
{
    private readonly ILogger<CategoryController> _logger;
    private readonly CategoryDB_Context _dbctxt;

    public CategoryController(ILogger<CategoryController>logger, CategoryDB_Context dbcontext)
    {
       _logger=logger; 
       _dbctxt=dbcontext;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Category()
    {
        IEnumerable<Category>objects = _dbctxt.categories;
        return View(objects);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {   
        if(_dbctxt.categories.Any(t=>t.Name==obj.Name)){
            ModelState.AddModelError("Name","Category Name Allready Exsist!!"); 
        }
        if(ModelState.IsValid){
        _dbctxt.categories.Add(obj);
        _dbctxt.SaveChanges();
        TempData["success"]="Category Created Successfully!!";
        return RedirectToAction("Category");
        }
        return View(obj);
    }
    
    public IActionResult Edit(Guid id)
    {
        if(id==null || id==Guid.Empty)
        {
            return NotFound();
        }
        var cater = _dbctxt.categories.FirstOrDefault(a=>a.ID==id);
        //var cater= _dbctxt.categories.SingleOrDefault(u=>u.ID==id); Birdona bo'lsa qaytaradi yoki exception beradi.
        //var cater=_dbctxt.Find(id); o'sha aydidagi narsalarni qaytaradi.
        return View(cater);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {   
        if(ModelState.IsValid){
        _dbctxt.categories.Update(obj);
        _dbctxt.SaveChanges();
        TempData["success"]="Category Updated Successfully!!";
        return RedirectToAction("Category");
        }
        return View(obj);
    }
    public IActionResult Delete(Guid id)
    {
        if(id==null || id==Guid.Empty)
        {
            return NotFound();
        }
        var cater = _dbctxt.categories.FirstOrDefault(a=>a.ID==id);
        return View(cater);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Category obj)
    {   
       if(!_dbctxt.categories.Any(t=>t.ID==obj.ID)){
           return NotFound();
       }
        _dbctxt.categories.Remove(obj);
        _dbctxt.SaveChanges();
        TempData["success"]="Category Deleted Successfully!!";
        return RedirectToAction("Category");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}