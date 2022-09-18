using PersonelMVCUI.Models.EntityFramework;
using PersonelMVCUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonelMVCUI.Controllers
{
    [Authorize(Roles = "Manager,Employee")]
    public class StatusController : Controller
    {
        PersoneldbEntities6 db = new PersoneldbEntities6();
        // GET: Status
        public ActionResult Index(string sortBy,string searchBy,string search)
        {
            ViewBag.SortStatusParameter = sortBy=="Status" ? "Status desc" : "Status";
            var status = db.Status.AsQueryable();
         //   var model = db.Status.ToList();
            if (searchBy=="Statu")
            {
                status = status.Where(x => x.Status1.StartsWith(search));
            }
            switch(sortBy)
            {
                case "Status":
                    status = status.OrderBy(x=>x.Status1);
                    break;
                case "Status desc":
                    status = status.OrderByDescending(x => x.Status1);
                    break;
                    

            }
            return View(status.ToList());
        }
        public ActionResult Yeni()
        {
            var model = new Status();
            return View("StatusForm",model);
        }

        public ActionResult Guncelle(int id)
        {
            var model = db.Status.Find(id);
            return View("StatusForm",model);
        }

        public ActionResult Kaydet(Status status)
        {
            MesajViewModel model = new MesajViewModel();

            if(!ModelState.IsValid)
            {

                return View("StatusForm");
            }
            if(status.Id==0)
            {
                
                db.Status.Add(status);
                model.Mesaj = status.Status1 + " " + "eklendi";


                var cale = new Select
                {
                    StatusId = status.Id,
                    PersonelId = 2
                };
                db.Select.Add(cale);
            }
            else
            {
                
                db.Entry(status).State = System.Data.Entity.EntityState.Modified;
                model.Mesaj = status.Status1 + " " + "güncellendi";
            }
            db.SaveChanges();
            model.LinkText = "Status";
            model.Status = true;
            model.Url = "/Status";
            return View("_Mesaj", model);

        }
        public ActionResult Sil(int id)
        {
            var model = db.Status.Find(id);
            db.Status.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}