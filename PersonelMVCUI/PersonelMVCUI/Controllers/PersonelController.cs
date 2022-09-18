
using PersonelMVCUI.Models.EntityFramework;
using PersonelMVCUI.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PersonelMVCUI.Controllers
{
    
    public class PersonelController : Controller
    {
        PersoneldbEntities6 db = new PersoneldbEntities6();
        [Authorize(Roles = "Employee,Manager,Customer")]
        public ActionResult Index(string searchBy,string search,string orderBy)
        {
            var personel = db.Personel.AsQueryable();
            if(searchBy=="Ad")
            {
               personel= personel.Where((x => x.Ad.StartsWith(search)));
            }
            if(searchBy=="Departman")
            {
                personel = personel.Where(x => x.Departman.Ad.StartsWith(search));
            }
            ViewBag.orderBy = orderBy == "Pers" ? "Pers desc" : "Pers";
            switch(orderBy)
            {
                case "Pers":
                   personel=personel.OrderBy(x => x.Ad);
                    break;
                case "Pers desc":
                   personel=personel.OrderByDescending(x => x.Ad);
                    break;
            }
            return View(personel.ToList());

           // var model = db.Personel.Include(x => x.Departman).ToList();
           // return View(model);
        }
        //  [Authorize(Roles = "A")]
        [Authorize(Roles = "Manager")]
        public ActionResult Yeni()
        {
            var model = new PersonelFormViewModel()
            {
                Departmanlar = db.Departman.ToList(),
                Personel=new Personel()
            };
            return View("PersonelForm", model);
        }
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public ActionResult Kaydet(Personel personel)
        {
            if(!ModelState.IsValid)
            {
                var model = new PersonelFormViewModel()
                {
                    Departmanlar = db.Departman.ToList(),
                    Personel = personel
                };

                return View("PersonelForm",model);
            }
            if (personel.Id == 0)
            {
               
                db.Personel.Add(personel);
                
                var cale = new Select
                {
                    PersonelId = personel.Id,
                    StatusId=16
                };
                db.Select.Add(cale);
            }
            else
            {
                db.Entry(personel).State = System.Data.Entity.EntityState.Modified;
            }
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Manager")]
        public ActionResult Guncelle(int id)
        {
            var model = new PersonelFormViewModel() {Departmanlar=db.Departman.ToList(),Personel=db.Personel.Find(id) };
            return View("PersonelForm",model);
        }
        [Authorize(Roles = "Manager")]
        public ActionResult Sil(int id)
        {
            var silinecekPersonel = db.Personel.Find(id);
            if (silinecekPersonel==null)
            {
                return HttpNotFound();
            }
            db.Personel.Remove(silinecekPersonel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Manager")]
        public int? ToplamMaas()
        {
           return db.Personel.Sum(x=>x.Maas);
        }
        public  ActionResult PersonelleriListele(int id)
        {
            var model = db.Personel.Where(x => x.DepartmanId == id).ToList();
            return PartialView(model);
        }

        public ActionResult PersonelGorev(string id)
        {
            var model = db.Tasks.Where(x => x.Personel.Ad==id).ToList();
            return View(model);
        }

    }
}