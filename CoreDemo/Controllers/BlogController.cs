using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {

        BlogManager bm = new BlogManager(new EfBlogRepository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());

        public IActionResult Index()
        {

            var values = bm.GetBlogListWithCategory();
            return View(values);
        }

        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = bm.GetBlogsByID(id);
            return View(values);
        }

        public IActionResult BlogListByWriter()
        {
            var values = bm.GetListWithCategoryByWriterBlogManager(1);
            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
           
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cm = categoryValues;
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog p)
        {

            BlogValidator bv = new BlogValidator();
            ValidationResult results = bv.Validate(p);
            //WriterValidator wv = new WriterValidator();

            if (results.IsValid)
            {
                p.BlogStatus = true;
                p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                p.WriterID = 1;
                bm.TAdd(p);

                return RedirectToAction("BlogListByWriter", "Blog");
            }
            else{ 
                
                foreach (var rule in results.Errors)
                {
                    ModelState.AddModelError(rule.PropertyName, rule.ErrorMessage);
                }           
            } 
            return View();
        }

        public IActionResult DeleteBlog(int id)
        {
            var blogvalue = bm.TGetByID(id);
            bm.TDelete(blogvalue);
            return RedirectToAction("BlogListByWriter");
        }
        
        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            List<SelectListItem> categoryValues = (from x in cm.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cm = categoryValues;
            var blogvalues = bm.TGetByID(id);
            
            return View(blogvalues);
        }
        [HttpPost]

        public IActionResult EditBlog(Blog p)
        {
            p.WriterID = 1;
            p.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.BlogStatus = true;
            bm.TUpdate(p);
            return RedirectToAction("BlogListByWriter");
        }

    }
}
