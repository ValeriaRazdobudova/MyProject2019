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

    //Клас EquipmentTypesController - для роботи з працівниками
    public class NTCWorkersController : Controller
    {
        private ModelDB db = new ModelDB();

        //Action для сторінки зі списком працівників
        public ActionResult Index(string sortOrder, string SearchByName, string SearchByNumb, string SearchByMail, string SearchBySkype,
                    string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PhoneNumbSortParm = sortOrder == "phone_numb" ? "phone_numb_desc" : "phone_numb";
            ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
            ViewBag.SkypeSortParm = sortOrder == "skype" ? "skype_desc" : "skype";
            ViewBag.ExperienceSortParm = sortOrder == "experience" ? "experience_desc" : "experience";
            ViewBag.SalarySortParm = sortOrder == "salary" ? "salary_desc" : "salary";

            if (SearchByName != null)
            {
                page = 1;
            }
            else
            {
                SearchByName = currentFilter;
            }

            if (SearchByNumb != null)
            {
                page = 1;
            }
            else
            {
                SearchByNumb = currentFilter;
            }

            if (SearchByMail != null)
            {
                page = 1;
            }
            else
            {
                SearchByMail = currentFilter;
            }

            if (SearchBySkype != null)
            {
                page = 1;
            }
            else
            {
                SearchBySkype = currentFilter;
            }

            ViewBag.CurrentFilter = SearchByName;
            ViewBag.CurrentFilter = SearchByNumb;
            ViewBag.CurrentFilter = SearchByMail;
            ViewBag.CurrentFilter = SearchBySkype;

            var workers = from s in db.NTCWorkers select s;

            if (!String.IsNullOrEmpty(SearchByName))
            {
                workers = workers.Where(s => s.name_NTCworker.Contains(SearchByName));
            }

            if (!String.IsNullOrEmpty(SearchByNumb))
            {
                workers = workers.Where(s => s.phone_number_NTCworker.Contains(SearchByNumb));
            }

            if (!String.IsNullOrEmpty(SearchByMail))
            {
                workers = workers.Where(s => s.email_NTCworker.Contains(SearchByMail));
            }

            if (!String.IsNullOrEmpty(SearchBySkype))
            {
                workers = workers.Where(s => s.skype_NTCworker.Contains(SearchBySkype));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    workers = workers.OrderByDescending(s => s.name_NTCworker);
                    break;
                case "phone_numb":
                    workers = workers.OrderBy(s => s.phone_number_NTCworker);
                    break;
                case "phone_numb_desc":
                    workers = workers.OrderByDescending(s => s.phone_number_NTCworker);
                    break;
                case "email":
                    workers = workers.OrderBy(s => s.email_NTCworker);
                    break;
                case "email_desc":
                    workers = workers.OrderByDescending(s => s.email_NTCworker);
                    break;
                case "skype":
                    workers = workers.OrderBy(s => s.skype_NTCworker);
                    break;
                case "skype_desc":
                    workers = workers.OrderByDescending(s => s.skype_NTCworker);
                    break;
                case "experience":
                    workers = workers.OrderBy(s => s.experience);
                    break;
                case "experience_desc":
                    workers = workers.OrderByDescending(s => s.experience);
                    break;
                case "salary":
                    workers = workers.OrderBy(s => s.salary);
                    break;
                case "salary_desc":
                    workers = workers.OrderByDescending(s => s.salary);
                    break;
                default:
                    workers = workers.OrderBy(s => s.name_NTCworker);
                    break;
            }
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(workers.ToPagedList(pageNumber, pageSize));
        }

        //Action для сторінки деталей
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCWorkers nTCWorkers = await db.NTCWorkers.FindAsync(id);
            if (nTCWorkers == null)
            {
                return HttpNotFound();
            }
            return View(nTCWorkers);
        }

        //Action-и для сторінки зі створення
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_NTCworker,name_NTCworker,phone_number_NTCworker," +
            "email_NTCworker,skype_NTCworker,experience,salary")] NTCWorkers nTCWorkers)
        {
            if (ModelState.IsValid)
            {
                db.NTCWorkers.Add(nTCWorkers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(nTCWorkers);
        }

        //Action-и для редагування
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCWorkers nTCWorkers = await db.NTCWorkers.FindAsync(id);
            if (nTCWorkers == null)
            {
                return HttpNotFound();
            }
            return View(nTCWorkers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_NTCworker,name_NTCworker,phone_number_NTCworker," +
            "email_NTCworker,skype_NTCworker,experience,salary")] NTCWorkers nTCWorkers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nTCWorkers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(nTCWorkers);
        }

        //Action-и для видалення
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NTCWorkers nTCWorkers = await db.NTCWorkers.FindAsync(id);
            if (nTCWorkers == null)
            {
                return HttpNotFound();
            }
            return View(nTCWorkers);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            NTCWorkers nTCWorkers = await db.NTCWorkers.FindAsync(id);
            db.NTCWorkers.Remove(nTCWorkers);
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
