using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1_MVC.Controllers
{
    public class WorkersController : Controller
    {
        private readonly AddResponcesDbContext dbContext;
        public WorkersController (AddResponcesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewData["AllWorkers"] = dbContext.Workers.FromSqlRaw("select * from Workers").ToList();
            ViewData["AllWorkTypes"] = dbContext.WorkTypes.FromSqlRaw("select * from WorkTypes").ToList();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddWorker()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWorker(string Name, string Patronymic, int Payment, string Surname)
        {
            dbContext.Database.ExecuteSqlRaw("Insert into Workers Values({0},{1},{2},{3})", Name, Patronymic, Payment, Surname);
            return RedirectToAction("Index", "Home");
        }
    }
}
