using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Lab1_MVC.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lab1_MVC.Controllers
{
    public class WorksController : Controller
    {
        private readonly AddResponcesDbContext dbContext;
        public WorksController(AddResponcesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewData["AllWorkers"] = dbContext.Workers.FromSqlRaw("select * from Workers").ToList();
            ViewData["AllWorkTypes"] = dbContext.WorkTypes.FromSqlRaw("select * from WorkTypes").ToList();
        }

        [HttpGet("{id}")]
        public IActionResult WorksOfWorker(int id)
        {
            var works = dbContext.Works.FromSqlRaw("select * from Works where WorkersId = {0}", id).ToList();
            var worker = dbContext.Workers.FromSqlRaw("select * from Workers where WorkersId = {0}", id).First();
            ViewBag.WorkerId = id;
            ViewBag.Worker = worker;
            foreach (var item in works)
            {
                item.WorkTypes = dbContext.WorkTypes.FromSqlRaw("select * from WorkTypes where WorkTypesId = {0}", item.WorkTypesId).First();
            }
            return View(works);
        }

        [HttpGet("{WorkTypeId}/{WorkerId}")]
        public IActionResult AddWorkToWorker(int WorkTypeId, int WorkerId)
        {
            var createdModel = dbContext.Database.ExecuteSqlRaw("Insert into Works Values({0},{1},{2},{3})", WorkerId, WorkTypeId, DateTime.Now, DateTime.Now);
            return RedirectToAction("WorksOfWorker", "Works", new { id = WorkerId });
        }

        [HttpGet("{WorkId}/{WorkerId}/{delete}")]
        public IActionResult DeleteWorkFromWorker(int WorkId, int WorkerId)
        {
            var createdModel = dbContext.Database.ExecuteSqlRaw("Delete from Works WHERE WorksId = {0}", WorkId);
            return RedirectToAction("WorksOfWorker", "Works", new { id = WorkerId });
        }

        [HttpGet]
        public IActionResult AddWorkType()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddWorkType(string description, int payment)
        {
            dbContext.Database.ExecuteSqlRaw("Insert into WorkTypes Values({0},{1})", description, payment);
            return RedirectToAction("Index", "Home");
        }

        // GET: WorksController
        public ActionResult Index()
        {
            return View();
        }

        // GET: WorksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
