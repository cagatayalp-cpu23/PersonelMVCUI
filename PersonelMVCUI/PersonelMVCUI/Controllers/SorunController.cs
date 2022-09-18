using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonelMVCUI.Models.EntityFramework;
using System.Data.Entity;
using PersonelMVCUI.ViewModels;

namespace PersonelMVCUI.Controllers
{
    public class SorunController : Controller
    {
        private PersoneldbEntities6 db = new PersoneldbEntities6();
        // GET: Sorun
        public ActionResult Index()
        {
            var model = db.Sorun.Include(x => x.Customers).Include(x => x.Status).ToList();
           

            return View(model);
        }

        public ActionResult Ekle()
        {
            var model = new SorunViewModel()
            {
                Customers = db.Customers.ToList(),
                Status = db.Status.ToList(),
                Sorun = new Sorun()
            };
            return View("SorunForm",model);
        }
        public ActionResult Kaydet(Sorun sorun)
        {
            MesajViewModel mesaj = new MesajViewModel();
            if (!ModelState.IsValid)
            {
                var model = new SorunViewModel()
                {
                    Customers = db.Customers.ToList(),
                    Status = db.Status.ToList(),
                    Sorun = sorun
                };
                return View("SorunForm",model);
            }

            if (sorun.Id == 0)
            {
                db.Sorun.Add(sorun);
                mesaj.Mesaj = sorun.TITLE + " " + "başarıyla eklendi";
            }
            db.SaveChanges();
            mesaj.Status = true;
            mesaj.LinkText = "Sorun Listesi";
            mesaj.Url = "/Sorun/";
            return View("_Mesaj",mesaj);
        }
    }
}