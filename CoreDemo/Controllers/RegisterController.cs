using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class RegisterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterRepository());
        WriterValidator wv = new WriterValidator();

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.getCity = GetCityList();
            return View();
        }

        [HttpPost]
        public IActionResult Index(Writer p, string city, string againPassword)
        {
            //WriterValidator wv = new WriterValidator();
            ValidationResult results = wv.Validate(p);
            if(results.IsValid && p.WriterPassword == againPassword)
            {
                p.WriterStatus = true;
                p.WriterAbout = "Deneme";
                wm.TAdd(p);
                return RedirectToAction("Index", "Blog");
            }
            else if (!results.IsValid)
            {
                foreach (var rule in results.Errors)
                {
                    ModelState.AddModelError(rule.PropertyName, rule.ErrorMessage);
                }


            }
            else
            {
                ModelState.AddModelError("WriterPassword", "Hatalı giriş. Girilen şifreler eşleşmedi tekrar deneyiniz.");
            }
            return View();


        }
        public List<string> GetCity()
        {
            String[] CityList = new String[] { "Adana", "Adıyaman", "Afyon", "Ağrı", "Aksaray", "Amasya", "Ankara", "Antalya", "Ardahan", "Artvin", "Aydın", "Bartın", "Batman", "Balıkesir", "Bayburt", "Bilecik", "Bingöl", "Bitlis", "Bolu", "Burdur", "Bursa", "Çanakkale", "Çankırı", "Çorum", "Denizli", "Diyarbakır", "Düzce", "Edirne", "Elazığ", "Erzincan", "Erzurum", "Eskişehir", "Gaziantep", "Giresun", "Gümüşhane", "Hakkari", "Hatay", "Iğdır", "Isparta", "İçel", "İstanbul", "İzmir", "Karabük", "Karaman", "Kars", "Kastamonu", "Kayseri", "Kırıkkale", "Kırklareli", "Kırşehir", "Kilis", "Kocaeli", "Konya", "Kütahya", "Malatya", "Manisa", "Kahramanmaraş", "Mardin", "Muğla", "Muş", "Nevşehir", "Niğde", "Ordu", "Osmaniye", "Rize", "Sakarya", "Samsun", "Siirt", "Sinop", "Sivas", "Tekirdağ", "Tokat", "Trabzon", "Tunceli", "Şanlıurfa", "Şırnak", "Uşak", "Van", "Yalova", "Yozgat", "Zonguldak" };
            return new List<string>(CityList);
        }

        public List<SelectListItem> GetCityList()
        {
            List<SelectListItem> getCity = (from n in GetCity()
                                            select new SelectListItem
                                            {
                                                Text = n,
                                                Value = n
                                            }).ToList();
            return getCity;
        }
        }
}
