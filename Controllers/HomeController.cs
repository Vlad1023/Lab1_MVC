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
using DinkToPdf.Contracts;
using DinkToPdf;
using System.IO;

namespace Lab1_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        string connectionString;
        private readonly AddResponcesDbContext dbContext;
        private readonly ILogger<HomeController> _logger;

        private IConverter _converter;

        public HomeController(ILogger<HomeController> logger, AddResponcesDbContext dbContext, IConfiguration configuration, IConverter converter)
        {
            _logger = logger;
            _converter = converter;
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
                case "WorkersThatDoNothing":
                    using (IDbConnection db = new SqlConnection(connectionString))
                    {
                        db.Open();
                        ViewData["FilteredData"] = db.Query<dynamic>(
                            @"SELECT wks.Name, wks.Surname, wks.Patronymic, wks.Payment FROM Workers wks
                            Where NOT Exists (Select * from Works ws where wks.WorkersId = ws.WorkersId)").ToList();
                        ViewData["FilteredKeys"] = new string[] { "Name", "Surname", "Patronymic", "Payment" };
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
            return View();
        }

        [HttpGet]
        public IActionResult FilterTableByDate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePDF(string htmlString)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 20 },
                DocumentTitle = "PDF Report about Works in out company",
                Out = Directory.GetCurrentDirectory() + "/Report.pdf"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlString,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/css", "bootstrap.min.css") },
                HeaderSettings = { Spacing = 10, FontName = "Arial", FontSize = 20, Right = "Information about company", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 25, Line = true, Center = "Information about company" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            _converter.Convert(pdf);
            return RedirectToAction("FilterTable");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
