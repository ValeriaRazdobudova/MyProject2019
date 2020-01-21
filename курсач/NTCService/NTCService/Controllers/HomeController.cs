using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NTCService;
using PagedList;

namespace NTCService.Controllers
{
    public class HomeController : Controller
    {
        private ModelDB db = new ModelDB();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TypesOfServices(string sortOrder, string SearchByName, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";


            if (SearchByName != null)
            {
                page = 1;
            }
            else
            {
                SearchByName = currentFilter;
            }

            ViewBag.CurrentFilter = SearchByName;

            var typesServices = from s in db.TypesNTCServices select s;

            if (!String.IsNullOrEmpty(SearchByName))
            {
                typesServices = typesServices.Where(s => s.name_type_NTCservice.Contains(SearchByName));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    typesServices = typesServices.OrderByDescending(s => s.name_type_NTCservice);
                    break;
                default:
                    typesServices = typesServices.OrderBy(s => s.name_type_NTCservice);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(typesServices.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Services(string sortOrder, string SearchByName, string SearchByTypeNTCService, string currentFilter,
            int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.TypeServiceSortParm = sortOrder == "name_type" ? "name_type_desc" : "name_type";
            ViewBag.EquipmentInStockrSortParm = sortOrder == "eq_in_stock" ? "eq_in_stock_desc" : "eq_in_stock";

            if (SearchByName != null)
            {
                page = 1;
            }
            else
            {
                SearchByName = currentFilter;
            }

            if (SearchByTypeNTCService != null)
            {
                page = 1;
            }
            else
            {
                SearchByTypeNTCService = currentFilter;
            }

            ViewBag.CurrentFilter = SearchByName;
            ViewBag.CurrentFilter = SearchByTypeNTCService;

            var services = db.NTCServices.Include(n => n.NTCequipmentInStock).Include(n => n.TypesNTCServices);

            if (!String.IsNullOrEmpty(SearchByName))
            {
                services = services.Where(s => s.name_NTCservice.Contains(SearchByName));
            }
            if (!String.IsNullOrEmpty(SearchByTypeNTCService))
            {
                services = services.Where(s => s.TypesNTCServices.name_type_NTCservice.Contains(SearchByTypeNTCService));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    services = services.OrderByDescending(s => s.name_NTCservice);
                    break;
                case "price":
                    services = services.OrderBy(s => s.price_NTCservice);
                    break;
                case "price_desc":
                    services = services.OrderByDescending(s => s.price_NTCservice);
                    break;
                case "name_type":
                    services = services.OrderBy(s => s.TypesNTCServices.name_type_NTCservice);
                    break;
                case "name_type_desc":
                    services = services.OrderByDescending(s => s.TypesNTCServices.name_type_NTCservice);
                    break;
                case "eq_in_stock":
                    services = services.OrderBy(s => s.NTCequipmentInStock.name_NTCequipment);
                    break;
                case "eq_in_stock_desc":
                    services = services.OrderByDescending(s => s.NTCequipmentInStock.name_NTCequipment);
                    break;
                default:
                    services = services.OrderBy(s => s.name_NTCservice);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(services.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult Application()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Application([Bind(Include = "id_application,phone_numb,mail")] Application application)
        {

            if (ModelState.IsValid)
            {
                db.Application.Add(application);
                db.SaveChanges();
                return RedirectToAction("Sent", "Home");
            }

            return View(application);
        }

        public ActionResult Sent()
        {
            return View();
        }
    }
}