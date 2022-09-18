using PersonelMVCUI.Models.EntityFramework;
using PersonelMVCUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PersonelMVCUI.Controllers
{
    
    public class DepartmanController : Controller
    {
        // GET: Departman
        PersoneldbEntities6 db = new PersoneldbEntities6();

        [Authorize(Roles = "Customer,Employee,Manager")]
        public ActionResult Index(string error,string orderby,string searchBy,string search)
        {
            var departman = db.Departman.AsQueryable();
           ViewBag.orderby=  orderby == "depart desc" ? "depart" : "depart desc";
            if(orderby=="depart")
            {
                departman = departman.OrderBy(x => x.Ad);
            }
            else
            {
                departman = departman.OrderByDescending(x=>x.Ad);
            }
            if(searchBy=="Ad")
            {
                departman = departman.Where(x => x.Ad.StartsWith(search));
            }

            
            //var model = db.Departman.ToList();
            ViewBag.Error = error;
            return View(departman.ToList());
        }
        [Authorize(Roles = "Employee,Manager")]
        [HttpGet]
        public ActionResult Kaydet(/*string DepartmanAdi*/)// yeni departman kaydetme için ekran
        {
            /* var departman = new Departman();
             departman.Ad = DepartmanAdi;
             db.Departman.Add(departman);
             db.SaveChanges();*/
            return View("DepartmanForm", new Departman());
        }
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee,Manager")]
        public ActionResult Kaydet(Departman departman)//kaydet tuşuna basıp kaydetme
        {
            // Random random = new Random();
            if (!ModelState.IsValid)
            {
                return View("DepartmanForm");
            }
            MesajViewModel model = new MesajViewModel();
            if (departman.Id == 0)
            {
                db.Departman.Add(departman);
                model.Mesaj = departman.Ad + " " + "başarıyla eklendi";
            }
            else
            {
                var guncellenecekDepartman = db.Departman.Find(departman.Id);
                if (guncellenecekDepartman == null)
                {
                    return HttpNotFound();
                }


                guncellenecekDepartman.Ad = departman.Ad;
                model.Mesaj = departman.Ad + "başarıyla guncellendi ";

            }

            // db.Departman.Add(departman);

            db.SaveChanges();
            model.Status = true;
            model.LinkText = "Departman Listesi";
            model.Url = "/Departman";
            return View("_Mesaj", model);
        }
        [Authorize(Roles = "Employee,Manager")]
        public ActionResult Guncelle(int id)
        {
            var model = db.Departman.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View("DepartmanForm", model);
        }
        [Authorize(Roles = "Employee,Manager")]
        public ActionResult Sil(int id)
        {
            var silinecekDepartman = db.Departman.Find(id);
            if (silinecekDepartman == null)
            {
                return HttpNotFound();
            }
            if (!db.Personel.Any(x=>x.DepartmanId==silinecekDepartman.Id))
            {

                db.Departman.Remove(silinecekDepartman);
                db.SaveChanges();
            }
            else
            {
             return RedirectToAction("Index",new { error= "Departmanda personel mevcut!" });
            }
            return RedirectToAction("Index");
        }
    }
}