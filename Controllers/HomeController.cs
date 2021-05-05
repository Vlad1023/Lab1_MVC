using System;
using Dapper;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.SqlServer.Design;
using Lab1_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Lab1_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        string connectionString;
        private readonly AddResponcesDbContext dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, AddResponcesDbContext dbContext, IConfiguration configuration)
        {
            _logger = logger;
            this.dbContext = dbContext;
            this._configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewData["AllWorkers"] = dbContext.Workers.FromSqlRaw("select * from Workers").ToList();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("filterOption")]
        public IActionResult FilterTable(string filterOption)
        {
            switch (filterOption)
            {
                case "CountOfWorkersByEachWorkType":
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        var result = db.Query<dynamic>(
                            @"SELECT wt.Description, wt.PaymentPerDay, COUNT(wks.Payment) as CountOfWorkers FROM Works ws
                            INNER Join Workers wks ON wks.WorkersId = ws.WorkersId
                            INNER Join WorkTypes wt ON wt.WorkTypesId = ws.WorkTypesId
                            GROUP BY wt.Description, wt.PaymentPerDay").ToList();
                        ViewData["FilteredData"] = result;
                        ViewData["FilteredKeys"] =  new string[] { "Description", "PaymentPerDay", "CountOfWorkers" };
                    }
                    break;
                case "CountOfWorksByEachWorker":
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        ViewData["FilteredData"] = db.Query<dynamic>(
                            @"SELECT wks.Name, wks.Surname, wks.Payment, COUNT(wt.PaymentPerDay) AS CountOfWorks FROM Works ws
                            INNER Join Workers wks ON wks.WorkersId = ws.WorkersId
                            INNER Join WorkTypes wt ON wt.WorkTypesId = ws.WorkTypesId
                            GROUP BY wks.Name, wks.Surname, wks.Payment").ToList();
                        ViewData["FilteredKeys"] = new string[] { "Name", "Surname", "Payment", "CountOfWorks" };
                    }
                    break;
                case "AllWorkers":
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        ViewData["FilteredData"] = db.Query<dynamic>(
                            @"SELECT wks.Name, wks.Surname, wks.Payment, wt.Description, wt.PaymentPerDay FROM Works ws
                             INNER Join WorkTypes wt ON wt.WorkTypesId = ws.WorkTypesId
							 RIGHT Join Workers wks ON wks.WorkersId = ws.WorkersId").ToList();
                        ViewData["FilteredKeys"] = new string[] { "Name", "Surname", "Payment", "Description", "PaymentPerDay" };
                    }
                    break;
                default:
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        ViewData["FilteredData"] = db.Query<dynamic>(
                            @"SELECT wks.Name, wks.Surname, wks.Payment, wt.Description, wt.PaymentPerDay FROM Works ws
                            INNER Join Workers wks ON wks.WorkersId = ws.WorkersId
                            INNER Join WorkTypes wt ON wt.WorkTypesId = ws.WorkTypesId
                            ORDER BY wks.Payment
                            ").ToList();
                        ViewData["FilteredKeys"] = new string[] { "Name", "Surname", "Payment", "Description", "PaymentPerDay" };
                    }

                    break;
            }
            //ViewData["AllWorks"] = dbContext.Works.FromSqlRaw("select * from Works Order BY StartDate ASC");
            return View();
        }

        [HttpGet]
        public IActionResult FilterTableByDate()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
