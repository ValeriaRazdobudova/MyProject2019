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

    //Клас EquipmentInStockController - для роботи з типами техніки та ПЗ
    public class NTCequipmentInStockController : Controller
    {
        private ModelDB db = new ModelDB();

        //Action для сторінки зі списком техніки та ПЗ
        public ActionResult Index(string sortOrder, string SearchByName, string SearchByTypeNTCService, 
            string SearchByNTCequipmentProviders, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TypeServiceSortParm = sortOrder == "name_type" ? "name_type_desc" : "name_type";
            ViewBag.EquipmentProviderSortParm = sortOrder == "eq_provider" ? "eq_provider_desc" : "eq_provider";
            ViewBag.AmountSortParm = sortOrder == "amount" ? "amount_desc" : "amount";

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

            if (SearchByNTCequipmentProviders != null)
            {
                page = 1;
            }
            else
            {
                SearchByNTCequipmentProviders = currentFilter;
            }

            ViewBag.CurrentFilter = SearchByName;
            ViewBag.CurrentFilter = SearchByTypeNTCService;
            ViewBag.CurrentFilter = SearchByNTCequipmentProviders;
            
            var equipmentInStock = db.NTCequipmentInStock.Include(n => n.NTCequipmentProviders).Include(n => n.TypesNTCequipment);

            if (!String.IsNullOrEmpty(SearchByName))
            {
                equipmentInStock = equipmentInStock.Where(s => s.name_NTCequipment.Contains(SearchByName));
            }
            if (!String.IsNullOrEmpty(SearchByTypeNTCService))
            {
                equipmentInStock = equipmentInStock.Where(s => s.TypesNTCequipment.name_type_NTCequipment.
                Contains(SearchByTypeNTCService));
            }
            if (!String.IsNullOrEmpty(SearchByNTCequipmentProviders))
            {
                equipmentInStock = equipmentInStock.Where(s => s.NTCequipmentProviders.name_NTCprovider.
                Contains(SearchByNTCequipmentProviders));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    equipmentInStock = equipmentInStock.OrderByDescending(s => s.name_NTCequipment);
                    break;
                case "name_type":
                    equipmentInStock = equipmentInStock.OrderBy(s => s.TypesNTCequipment.name_type_NTCequipment);
                    break;
                case "name_type_desc":
                    equipmentInStock = equipmentInStock.OrderByDescending(s => s.TypesNTCequipment.name_type_NTCequipment);
                    break;
                case "eq_provider":
                    equipmentInStock = equipmentInStock.OrderBy(s => s.NTCequipmentProviders.name_NTCprovider);
                    break;
                case "eq_provider_desc":
                    equipmentInStock = equipmentInStock.OrderByDescending(s => s.NTCequipmentProviders.name_NTCprovider);
                    break;
                case "amount":
                    equipmentInStock = equipmentInStock.OrderBy(s => s.amount_NTCequipment);
                    break;
                case "amount_desc":
                    equipmentInStock = equipmentInStock.OrderByDescending(s => s.amount_NTCequipment);
                    break;
                default:
                    equipmentInStock = equipmentInStock.OrderBy(s => s.name_NTCequipment);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(equipmentInStock.ToPagedList(pageNumber, pageSize));
        }

        //Action для сторінки деталей
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCequipmentInStock nTCequipmentInStock = await db.NTCequipmentInStock.FindAsync(id);
            if (nTCequipmentInStock == null)
            {
                return HttpNotFound();
            }
            return View(nTCequipmentInStock);
        }

        //Action-и для сторінки зі створення
        public ActionResult Create()
        {
            ViewBag.id_NTCprovider = new SelectList(db.NTCequipmentProviders, "id_NTCprovider", "name_NTCprovider");
            ViewBag.id_type_NTCequipment = new SelectList(db.TypesNTCequipment, "id_type_NTCequipment", "name_type_NTCequipment");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_NTCequipment,name_NTCequipment,id_type_NTCequipment," +
            "id_NTCprovider,amount_NTCequipment")] NTCequipmentInStock nTCequipmentInStock)
        {
            if (ModelState.IsValid)
            {
                db.NTCequipmentInStock.Add(nTCequipmentInStock);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_NTCprovider = new SelectList(db.NTCequipmentProviders, "id_NTCprovider", "name_NTCprovider", 
                nTCequipmentInStock.id_NTCprovider);
            ViewBag.id_type_NTCequipment = new SelectList(db.TypesNTCequipment, "id_type_NTCequipment", "name_type_NTCequipment", 
                nTCequipmentInStock.id_type_NTCequipment);
            return View(nTCequipmentInStock);
        }

        //Action-и для редагування
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCequipmentInStock nTCequipmentInStock = await db.NTCequipmentInStock.FindAsync(id);
            if (nTCequipmentInStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_NTCprovider = new SelectList(db.NTCequipmentProviders, "id_NTCprovider", "name_NTCprovider", 
                nTCequipmentInStock.id_NTCprovider);
            ViewBag.id_type_NTCequipment = new SelectList(db.TypesNTCequipment, "id_type_NTCequipment", "name_type_NTCequipment", 
                nTCequipmentInStock.id_type_NTCequipment);
            return View(nTCequipmentInStock);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_NTCequipment,name_NTCequipment,id_type_NTCequipment," +
            "id_NTCprovider,amount_NTCequipment")] NTCequipmentInStock nTCequipmentInStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nTCequipmentInStock).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_NTCprovider = new SelectList(db.NTCequipmentProviders, "id_NTCprovider", "name_NTCprovider", 
                nTCequipmentInStock.id_NTCprovider);
            ViewBag.id_type_NTCequipment = new SelectList(db.TypesNTCequipment, "id_type_NTCequipment", "name_type_NTCequipment", 
                nTCequipmentInStock.id_type_NTCequipment);
            return View(nTCequipmentInStock);
        }

        //Action-и для видалення
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCequipmentInStock nTCequipmentInStock = await db.NTCequipmentInStock.FindAsync(id);
            if (nTCequipmentInStock == null)
            {
                return HttpNotFound();
            }
            return View(nTCequipmentInStock);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NTCequipmentInStock nTCequipmentInStock = await db.NTCequipmentInStock.FindAsync(id);
            db.NTCequipmentInStock.Remove(nTCequipmentInStock);
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
