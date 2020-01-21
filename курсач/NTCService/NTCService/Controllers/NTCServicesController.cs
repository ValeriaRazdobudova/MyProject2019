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
    [Authorize(Roles = "Admin")]

    //Клас NTCServicesController - для роботи з типами послуг
    public class NTCServicesController : Controller
    {
        private ModelDB db = new ModelDB();

        //Action для сторінки зі списком послуг
        public ActionResult Index(string sortOrder, string SearchByName, string SearchByTypeNTCService, string currentFilter, int? page)
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

        //Action для сторінки деталей
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCServices nTCServices = await db.NTCServices.FindAsync(id);
            if (nTCServices == null)
            {
                return HttpNotFound();
            }
            return View(nTCServices);
        }

        //Action-и для сторінки зі створення
        public ActionResult Create()
        {
            ViewBag.id_NTCequipment = new SelectList(db.NTCequipmentInStock, "id_NTCequipment", "name_NTCequipment");
            ViewBag.id_type_NTCservice = new SelectList(db.TypesNTCServices, "id_type_NTCservice", "name_type_NTCservice");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_NTCservice,name_NTCservice,price_NTCservice,id_NTCequipment," +
            "id_type_NTCservice")] NTCServices nTCServices)
        {
            if (ModelState.IsValid)
            {
                db.NTCServices.Add(nTCServices);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_NTCequipment = new SelectList(db.NTCequipmentInStock, "id_NTCequipment", "name_NTCequipment", 
                nTCServices.id_NTCequipment);
            ViewBag.id_type_NTCservice = new SelectList(db.TypesNTCServices, "id_type_NTCservice", "name_type_NTCservice",
                nTCServices.id_type_NTCservice);
            return View(nTCServices);
        }

        //Action-и для редагування
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCServices nTCServices = await db.NTCServices.FindAsync(id);
            if (nTCServices == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_NTCequipment = new SelectList(db.NTCequipmentInStock, "id_NTCequipment", "name_NTCequipment", 
                nTCServices.id_NTCequipment);
            ViewBag.id_type_NTCservice = new SelectList(db.TypesNTCServices, "id_type_NTCservice", "name_type_NTCservice",
                nTCServices.id_type_NTCservice);
            return View(nTCServices);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_NTCservice,name_NTCservice,price_NTCservice,id_NTCequipment," +
            "id_type_NTCservice")] NTCServices nTCServices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nTCServices).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_NTCequipment = new SelectList(db.NTCequipmentInStock, "id_NTCequipment", "name_NTCequipment", 
                nTCServices.id_NTCequipment);
            ViewBag.id_type_NTCservice = new SelectList(db.TypesNTCServices, "id_type_NTCservice", "name_type_NTCservice", 
                nTCServices.id_type_NTCservice);
            return View(nTCServices);
        }

        //Action-и для видалення
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCServices nTCServices = await db.NTCServices.FindAsync(id);
            if (nTCServices == null)
            {
                return HttpNotFound();
            }
            return View(nTCServices);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NTCServices nTCServices = await db.NTCServices.FindAsync(id);
            db.NTCServices.Remove(nTCServices);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //звільнення некерованих ресурсів
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
