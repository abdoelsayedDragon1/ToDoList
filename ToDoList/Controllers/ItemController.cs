using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ItemController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public IActionResult Index(ToDoItem toDoItem)
        {
           var toDoitem = dbContext.ToDoItems.ToList();
            return View(model: toDoitem);
        }
        [HttpGet]
    public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1) 
                };

                if (Request.Cookies.ContainsKey("username"))
                {
                    Response.Cookies.Delete("username");
                }

                Response.Cookies.Append("username", name, cookieOptions);
                return RedirectToAction("Index");
            }

        return View();
        }


        [HttpGet]
        public IActionResult Save()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save(ToDoItem model)
        {
            if (ModelState.IsValid)
            {
                dbContext.ToDoItems.Add(model);  
                dbContext.SaveChanges();      

                TempData["SuccessMessage"] = "Item created successfully!";
                return RedirectToAction(nameof(Index));  
            }

            return View(model);  
        }

        public IActionResult Edit(int ToDoItemId)
        {
            var ToDoItem = dbContext.ToDoItems.Find(ToDoItemId);
            if (ToDoItem != null)
                return View(model: ToDoItem);
            return RedirectToAction("NotFound", "Home");

        }
        [HttpPost]
        public IActionResult Edit(ToDoItem toDoItem)
        {
            
            dbContext.ToDoItems.Update(toDoItem);
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int ToDoItemId)
        {
            dbContext.ToDoItems.Remove(new() { Id = ToDoItemId });
            dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
