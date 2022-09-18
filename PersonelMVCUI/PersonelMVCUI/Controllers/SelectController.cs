using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonelMVCUI.Models.EntityFramework;
using System.Data.Entity;

namespace PersonelMVCUI.Controllers
{
    public class SelectController : Controller
    {
        // GET: Select
        private PersoneldbEntities6 db = new PersoneldbEntities6();
        public ActionResult Index()
        {
            var model = db.Select.Include(x => x.Status).Include(x=>x.Personel).ToList();
            return View(model);
        }
    }
}