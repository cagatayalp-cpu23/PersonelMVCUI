using PersonelMVCUI.Models.EntityFramework;
using PersonelMVCUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace PersonelMVCUI.Controllers
{
    public class TaskController : Controller
    {
        PersoneldbEntities6 db = new PersoneldbEntities6();

        // GET: Task
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult Index(string searchBy, string search, string sortBy, Tasks task, Kullanici kullanici, int? id)
        {

            var userName = System.Web.HttpContext.Current.User.Identity.Name;



            Personel personel = new Personel()
            {
                Ad = userName
            };
            


            ViewBag.SortTITLEParameter = string.IsNullOrEmpty(sortBy) ? "TITLE desc" : "";
            ViewBag.SortStatusParameter = sortBy == "Status" ? "Status desc" : "Status";
            var tasks = db.Tasks.AsQueryable();
            if (searchBy == "TITL")
            {
                tasks = tasks.Where(x => x.TITLE.StartsWith(search));
                //  return View(tasks.Include(x => x.Customers).Include(x => x.Status).Include(x => x.Personel));

            }
            else if (searchBy == "Statu")
            {
                tasks = tasks.Where(x => x.Status.Status1.StartsWith(search));
                //     return View(tasks.Include(x => x.Customers).Include(x => x.Status));

            }

            switch (sortBy)
            {
                case "TITLE desc":
                    tasks = tasks.OrderByDescending(x => x.TITLE);
                    break;
                case "Status desc":
                    tasks = tasks.OrderByDescending(x => x.Status.Status1);
                    break;
                case "Status":
                    tasks = tasks.OrderBy(x => x.Status.Status1);
                    break;
                default:
                    tasks = tasks.OrderBy(x => x.TITLE);
                    break;
            }

            // var modelFind = db.Kullanici.Find(id);
            return View(tasks.Where(x => x.Personel.Ad == userName).Include(x => x.Status).Include(x => x.Customers).Include(x => x.Personel).ToList());


        }
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult SorunEkle(int id)
        {
            var userName = System.Web.HttpContext.Current.User.Identity.Name;



            Personel personel = new Personel()
            {
                Ad = userName
            };

            var asd = db.Sorun.Find(id);
            Tasks task1 = new Tasks()
            {
                Id = asd.Id,
                TITLE = asd.TITLE,
                Customers = asd.Customers,
                Personel = personel,
                Status = asd.Status,
                DEADLINE = asd.DEADLINE,
                CREATED_AT = (DateTime)asd.CREATED_AT,

            };
            db.Tasks.Add(task1);
            db.SaveChanges();
            return View("Index",task1);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Yeni()
        {
            var model = new TaskFormViewModel()
            {
                Personeller = db.Personel.ToList(),
                Status = db.Status.ToList(),
                Customers = db.Customers.ToList(),
                Tasks = new Tasks()
            };
            return View("TaskForm", model);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Guncelle(int id)
        {
            var model = new TaskFormViewModel()
            {
                Personeller = db.Personel.ToList(),
                Status = db.Status.ToList(),
                Customers = db.Customers.ToList(),
                Tasks = db.Tasks.Find(id)
            };
            return View("TaskForm", model);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Kaydet(Tasks tasks)
        {
            MesajViewModel mesaj = new MesajViewModel();
            if (!ModelState.IsValid)
            {
                var model = new TaskFormViewModel()
                {
                    Personeller = db.Personel.ToList(),
                    Customers = db.Customers.ToList(),
                    Status = db.Status.ToList(),
                    Tasks = tasks
                };
                return View("TaskForm", model);
            }

            if (tasks.Id == 0)
            {
                db.Tasks.Add(tasks);
                mesaj.Mesaj = tasks.TITLE + " " + "başarıyla eklendi";
            }
            else
            {
                db.Entry(tasks).State = EntityState.Modified;
                mesaj.Mesaj = tasks.TITLE + " " + "başarıyla guncellendi";

            }

            db.SaveChanges();
            mesaj.Status = true;
            mesaj.LinkText = "Task Listesi";
            mesaj.Url = "/Task/";
            return View("_Mesaj", mesaj);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Sil(int id)
        {
            var silinecek = db.Tasks.Find(id);
            db.Tasks.Remove(silinecek);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PerTask(string id)
        {
            var model = db.Tasks.Where(x => x.Status.Status1 == id).ToList();
            return View(model);
        }







    }
}