using MarketAPP.Business;
using MarketAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketAPP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ProductBS bs = new ProductBS();
            List<ProductViewModel> model = bs.GetAll();
            return View(model);

        }

        public PartialViewResult GetCategory()
        {
            CategoryBS bs = new CategoryBS();
            List<CategoryViewModel> list = bs.Index();
            return PartialView("_Category", list);
        }
        public PartialViewResult GetCategoryLeft()
        {
            CategoryBS bs = new CategoryBS();
            List<CategoryViewModel> list = bs.Index();
            return PartialView("_CategoryLeftSideBar", list);
        }

    }
}