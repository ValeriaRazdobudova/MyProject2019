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

    //Клас TypesNTCequipmentController - для роботи з типами обладнання
    public class TypesNTCequipmentController : Controller
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

            var typesEquipment = from s in db.TypesNTCequipment select s;

            if (!String.IsNullOrEmpty(SearchByName))
            {
                typesEquipment = typesEquipment.Where(s => s.name_type_NTCequipment.Contains(SearchByName));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    typesEquipment = typesEquipment.OrderByDescending(s => s.name_type_NTCequipment);
                    break;
                default:
                    typesEquipment = typesEquipment.OrderBy(s => s.name_type_NTCequipment);
                    break;
            }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(typesEquipment.ToPagedList(pageNumber, pageSize));
        }

        //Action для сторінки деталей
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypesNTCequipment typesNTCequipment = await db.TypesNTCequipment.FindAsync(id);
            if (typesNTCequipment == null)
            {
                return HttpNotFound();
            }
            return View(typesNTCequipment);
        }

        //Action-и для сторінки зі створення
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_type_NTCequipment,name_type_NTCequipment")]
        TypesNTCequipment typesNTCequipment)
        {
            if (ModelState.IsValid)
            {
                db.TypesNTCequipment.Add(typesNTCequipment);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(typesNTCequipment);
        }

        //Action-и для редагування
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypesNTCequipment typesNTCequipment = await db.TypesNTCequipment.FindAsync(id);
            if (typesNTCequipment == null)
            {
                return HttpNotFound();
            }
            return View(typesNTCequipment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_type_NTCequipment,name_type_NTCequipment")]
        TypesNTCequipment typesNTCequipment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typesNTCequipment).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(typesNTCequipment);
        }

        //Action-и для видалення
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TypesNTCequipment typesNTCequipment = await db.TypesNTCequipment.FindAsync(id);
            if (typesNTCequipment == null)
            {
                return HttpNotFound();
            }
            return View(typesNTCequipment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TypesNTCequipment typesNTCequipment = await db.TypesNTCequipment.FindAsync(id);
            db.TypesNTCequipment.Remove(typesNTCequipment);
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
