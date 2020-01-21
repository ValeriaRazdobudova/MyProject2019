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

    //Клас TypesNTCServicesController - для роботи з типами обладнання
    public class TypesNTCServicesController : Controller
    {
        private ModelDB db = new ModelDB();

        //Action для сторінки зі списком типів обладнання
        public ActionResult Index(string sortOrder, string SearchByName, string currentFilter, int? page)
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
            return View( typesServices.ToPagedList(pageNumber, pageSize));

        }

        //Action для сторінки деталей
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypesNTCServices typesNTCServices = await db.TypesNTCServices.FindAsync(id);
            if (typesNTCServices == null)
            {
                return HttpNotFound();
            }
            return View(typesNTCServices);
        }

        //Action-и для сторінки зі створення
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_type_NTCservice,name_type_NTCservice")]
        TypesNTCServices typesNTCServices)
        {
            if (ModelState.IsValid)
            {
                db.TypesNTCServices.Add(typesNTCServices);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typesNTCServices);
        }

        //Action-и для редагування
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypesNTCServices typesNTCServices = await db.TypesNTCServices.FindAsync(id);
            if (typesNTCServices == null)
            {
                return HttpNotFound();
            }
            return View(typesNTCServices);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_type_NTCservice,name_type_NTCservice")]
        TypesNTCServices typesNTCServices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typesNTCServices).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(typesNTCServices);
        }

        //Action-и для видалення
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypesNTCServices typesNTCServices = await db.TypesNTCServices.FindAsync(id);
            if (typesNTCServices == null)
            {
                return HttpNotFound();
            }
            return View(typesNTCServices);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TypesNTCServices typesNTCServices = await db.TypesNTCServices.FindAsync(id);
            db.TypesNTCServices.Remove(typesNTCServices);
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
