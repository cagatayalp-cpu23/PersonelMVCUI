using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonelMVCUI.Models.EntityFramework;
using System.Data.Entity;

namespace PersonelMVCUI.Controllers
{
    public class SorunlarController : Controller
    {
        // GET: Sorunlar
        private PersoneldbEntities6 db = new PersoneldbEntities6();
        public ActionResult Index()
        {
            var model = db.Sorunlar.Include(x => x.Tasks).ToList();
            return View(model);
        }
        public ActionResult Sorun(int id)
        {
            var model = db.Sorunlar.Where(x => x.TaskId == id).ToList();
            return View(model);
        }
        
        public ActionResult Degistir(int id,Sorunlar sorun)
       {
           int k = 0;
           var sorussn = db.Sorunlar.Find(id);
         //  db.Sorunlar.Where(x => x.IsSelected == true);
           if (sorussn.IsSelected == true)
           {
               sorussn.IsSelected = false;
               k++;
           }
           if (sorussn.IsSelected == false && k==0)
           {
               sorussn.IsSelected = true;
           }
           db.SaveChanges();
           return RedirectToAction("Sorun", sorussn);

       }
    }
}