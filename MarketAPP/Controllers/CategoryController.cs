using MarketAPP.Business;
using MarketAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketAPP.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            CategoryBS bs = new CategoryBS();
            List<CategoryViewModel> list = bs.Index();
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            CategoryBS bs = new CategoryBS();
            List<CategoryViewModel> model = bs.Index();
            CategoryViewModel vmodel = new CategoryViewModel();
            vmodel.CategoryList = model;
            return View(vmodel);
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModel request)
        {
            CategoryBS bs = new CategoryBS();
            bool result = bs.Create(request);
            if (result == true)
            {
                TempData["Mesaj"] = "Başarılı İşlem";
            }
            else
            {
                TempData["Mesaj"] = "Bir hata oluştu.";
            }
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CategoryBS bs = new CategoryBS();
            CategoryViewModel vmodel = bs.LoadEdit(id);
            return View(vmodel);
        }
        [HttpPost]
        public ActionResult Edit(CategoryViewModel request)
        {
            CategoryBS bs = new CategoryBS();
            bool result = bs.Edit(request);
            if (result == true)
            {
                TempData["Mesaj"] = "Başarılı işlem";
            }
            else
            {
                TempData["Mesaj"] = "Bir hata oluştu.";
            }
            return RedirectToAction("Edit", new { request.Id });
        }

        public ActionResult Delete(int id)
        {
            CategoryBS bs = new CategoryBS();
            bool result = bs.Delete(id); ;
            return RedirectToAction("Index");

        }
    }
}