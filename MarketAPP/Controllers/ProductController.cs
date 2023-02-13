using MarketAPP.Business;
using MarketAPP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketAPP.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int id)
        {
            ProductBS bs = new ProductBS();
            List<ProductViewModel> model = bs.Index(id);
            return View(model);
        }

        public ActionResult List()
        {
            ProductBS bs = new ProductBS();
            List<ProductViewModel> model = bs.GetAll();
            return View(model);

        }
        [HttpGet]
        public ActionResult Create()
        {
            CategoryBS bs = new CategoryBS();
            ProductViewModel vmodel = new ProductViewModel();
            List<CategoryViewModel> model = bs.Index();
            vmodel.CategoryList = model;
            return View(vmodel);
        }
        [HttpPost]
        public ActionResult Create(ProductViewModel request, IEnumerable<HttpPostedFileBase> pics)
        {
            List<string> LocationPicture = new List<string>();
            IEnumerable<HttpPostedFileBase> filePic = pics;
            foreach (var file in filePic)
            {
                if (file != null)
                {
                    string filePath = Path.GetFileName(file.FileName);
                    string filePathLocation = "/files/" + filePath;
                    string uploadLocation = Path.Combine(Server.MapPath("~/files/"), filePath);
                    file.SaveAs(uploadLocation);
                    LocationPicture.Add(filePathLocation);
                }
            }
            request.FileLocationPic = LocationPicture;

            ProductBS bs = new ProductBS();
            bool result = bs.Create(request, pics);
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
            ProductBS bs = new ProductBS();
            ProductViewModel vmodel = bs.LoadEdit(id); ;
            return View(vmodel);
        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel request, IEnumerable<HttpPostedFileBase> pics)
        {
            List<string> LocationPicture = new List<string>();
            IEnumerable<HttpPostedFileBase> filePic = pics;
            foreach (var file in filePic)
            {
                if (file != null)
                {
                    string filePath = Path.GetFileName(file.FileName);
                    string filePathLocation = "/files/" + filePath;
                    string uploadLocation = Path.Combine(Server.MapPath("~/files/"), filePath);
                    file.SaveAs(uploadLocation);
                    LocationPicture.Add(filePathLocation);
                }
            }
            request.FileLocationPic = LocationPicture;
            ProductBS bs = new ProductBS();
            bool result = bs.Edit(request, pics);
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
            ProductBS bs = new ProductBS();
            bool result = bs.Delete(id); ;
            return RedirectToAction("List");

        }


        public JsonResult ProductById(int id)
        {
            ProductBS bs = new ProductBS();
            ProductViewModel model = bs.Detail(id);

            return Json(new { Result = true, Product = model });



        }
    }
}