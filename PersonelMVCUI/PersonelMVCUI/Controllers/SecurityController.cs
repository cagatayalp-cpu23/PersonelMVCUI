using PersonelMVCUI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonelMVCUI.Controllers
{ 
     [AllowAnonymous]
    public class SecurityController : Controller
    {
        PersoneldbEntities6 db = new PersoneldbEntities6();
        // GET: Security
       
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDb = db.Kullanici.FirstOrDefault(x=>x.Ad==kullanici.Ad&&x.Sifre==kullanici.Sifre);
            if(kullaniciInDb!=null)
            {
                FormsAuthentication.SetAuthCookie(kullaniciInDb.Ad, false);
                return RedirectToAction("Index","Customer");

            }
            else
            {
                ViewBag.Mesaj = "Geçersiz kullanıcı adı veya şifre ";
                  return View();
            }
           
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}