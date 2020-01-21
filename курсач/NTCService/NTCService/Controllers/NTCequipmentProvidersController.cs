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
    //Клас NTCequipmentProvidersController - для роботи з постачальниками
    [Authorize(Roles = "Admin")]

    public class NTCequipmentProvidersController : Controller
    {
        private ModelDB db = new ModelDB();

        //Action для сторінки зі списком постачальників
        public ActionResult Index(string sortOrder, string SearchByName, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PhoneNumbSortParm = sortOrder == "phone_numb" ? "phone_numb_desc" : "phone_numb";
            ViewBag.WebsiteSortParm = sortOrder == "website" ? "website_desc" : "website";
            ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
            ViewBag.AddressSortParm = sortOrder == "address" ? "address_desc" : "address";

            if (SearchByName != null)
            {
                page = 1;
            }
            else
            {
                SearchByName = currentFilter;
            }

            ViewBag.CurrentFilter = SearchByName;

            var equipmentProviders = from s in db.NTCequipmentProviders select s;

            if (!String.IsNullOrEmpty(SearchByName))
            {
                equipmentProviders = equipmentProviders.Where(s => s.name_NTCprovider.Contains(SearchByName));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    equipmentProviders = equipmentProviders.OrderByDescending(s => s.name_NTCprovider);
                    break;
                case "phone_numb":
                    equipmentProviders = equipmentProviders.OrderByDescending(s => s.phone_number_NTCprovider);
                    break;
                case "phone_numb_desc":
                    equipmentProviders = equipmentProviders.OrderByDescending(s => s.phone_number_NTCprovider);
                    break;
                case "website":
                    equipmentProviders = equipmentProviders.OrderByDescending(s => s.website_NTCprovider);
                    break;
                case "website_desc":
                    equipmentProviders = equipmentProviders.OrderByDescending(s => s.website_NTCprovider);
                    break;
                case "email":
                    equipmentProviders = equipmentProviders.OrderByDescending(s => s.email_NTCprovider);
                    break;
                case "email_desc":
                    equipmentProviders = equipmentProviders.OrderByDescending(s => s.email_NTCprovider);
                    break;
                case "address":
                    equipmentProviders = equipmentProviders.OrderByDescending(s => s.address_NTCprovider);
                    break;
                case "address_desc":
                    equipmentProviders = equipmentProviders.OrderByDescending(s => s.address_NTCprovider);
                    break;
                default:
                    equipmentProviders = equipmentProviders.OrderBy(s => s.name_NTCprovider);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(equipmentProviders.ToPagedList(pageNumber, pageSize));
        }

        //Action для сторінки деталей
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCequipmentProviders nTCequipmentProviders = await db.NTCequipmentProviders.FindAsync(id);
            if (nTCequipmentProviders == null)
            {
                return HttpNotFound();
            }
            return View(nTCequipmentProviders);
        }

        //Action-и для сторінки зі створення
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_NTCprovider,name_NTCprovider,phone_number_NTCprovider," +
            "website_NTCprovider,email_NTCprovider,address_NTCprovider")] NTCequipmentProviders nTCequipmentProviders)
        {
            if (ModelState.IsValid)
            {
                db.NTCequipmentProviders.Add(nTCequipmentProviders);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nTCequipmentProviders);
        }

        //Action-и для редагування
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCequipmentProviders nTCequipmentProviders = await db.NTCequipmentProviders.FindAsync(id);
            if (nTCequipmentProviders == null)
            {
                return HttpNotFound();
            }
            return View(nTCequipmentProviders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_NTCprovider,name_NTCprovider,phone_number_NTCprovider," +
            "website_NTCprovider,email_NTCprovider,address_NTCprovider")] NTCequipmentProviders nTCequipmentProviders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nTCequipmentProviders).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nTCequipmentProviders);
        }

        //Action-и для видалення
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCequipmentProviders nTCequipmentProviders = await db.NTCequipmentProviders.FindAsync(id);
            if (nTCequipmentProviders == null)
            {
                return HttpNotFound();
            }
            return View(nTCequipmentProviders);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NTCequipmentProviders nTCequipmentProviders = await db.NTCequipmentProviders.FindAsync(id);
            db.NTCequipmentProviders.Remove(nTCequipmentProviders);
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
