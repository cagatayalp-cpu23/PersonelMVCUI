using PersonelMVCUI.Models.EntityFramework;
using PersonelMVCUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;

namespace PersonelMVCUI.Controllers
{
    
    public class CustomerController : Controller
    {
        PersoneldbEntities6 db = new PersoneldbEntities6();
        // GET: Customer
        [Authorize(Roles = "Manager,Employee,Customer")]
        public ActionResult Index()
        {
            var model = db.Customers.Include(x=>x.Departman).ToList();
            return View(model);
        }
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult Yeni()
        {
            var model = new CustomerFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Customers = new Customers()
            };
            // return View("CustomerForm",model);
            return View("CustomerForm",model);
        }
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult Guncelle(int id)
        {
            var model = new CustomerFormViewModel() { Departmanlar = db.Departman.ToList(), Customers = db.Customers.Find(id) };
            return View("CustomerForm",model);
        }
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult Kaydet(Customers customers)
        {
            if(!ModelState.IsValid)
            {
                return View("CustomerForm", new Customers());
            }
            MesajViewModel model = new MesajViewModel();
            if(customers.Id==0)
            {
                db.Customers.Add(customers);
                model.Mesaj = customers.Ad + "Eklendi";
            }
            else
            {

                db.Entry(customers).State =System.Data.Entity.EntityState.Modified;
                model.Mesaj = customers.Ad + "Güncellendi";
            }
            db.SaveChanges();
            model.Status = true;
            model.LinkText = "Customers";
            model.Url = "/Customer";
            return PartialView("_Mesaj",model);
        }
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult Sil(int id)
        {
            var silinecek = db.Customers.Find(id);
            if(silinecek==null)
            {
                return HttpNotFound();
            }
            db.Customers.Remove(silinecek);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}