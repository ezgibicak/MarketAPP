using MarketAPP.Data;
using MarketAPP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAPP.Business
{
    public class CategoryBS
    {
        private readonly MarketAPPEntities ctx;
        public CategoryBS()
        {
            if (ctx == null)
            {
                ctx = new MarketAPPEntities();
            }
        }

        public List<CategoryViewModel> Index()
        {
            List<CategoryViewModel> model = ctx.tblCategory.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => new CategoryViewModel
            {
                Id = x.Id,
                TopCategoryId = x.TopCategoryId,
                IsActive = x.IsActive,
                CategoryName = x.CategoryName,
                IsDeleted = x.IsDeleted


            }).ToList();
            return model;
        }

        public bool Create(CategoryViewModel request)
        {
            bool value = false;
            tblCategory model = new tblCategory();

            model.IsDeleted = false;
            model.IsActive = true;
            model.CategoryName = request.CategoryName;
            model.TopCategoryId = request.TopCategoryId;

            ctx.tblCategory.Add(model);
            int result = ctx.SaveChanges();

            if (result > 0)
            {
                value = true;
            }
            else
            {
                value = false;
            }

            return value;

        }

        public CategoryViewModel LoadEdit(int id)
        {

            tblCategory model = ctx.tblCategory.Find(id);
            CategoryViewModel vmodel = new CategoryViewModel();
            vmodel.CategoryName = model.CategoryName;
            vmodel.TopCategoryId = model.TopCategoryId;

            List<CategoryViewModel> cgmodel = ctx.tblCategory.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => new CategoryViewModel
            {
                Id = x.Id,
                TopCategoryId = x.TopCategoryId,
                IsActive = x.IsActive,
                CategoryName = x.CategoryName,
                IsDeleted = x.IsDeleted


            }).ToList();
            vmodel.CategoryList = cgmodel;
            return vmodel;
        }
        public bool Edit(CategoryViewModel request)
        {
            bool value = false;
            tblCategory model = ctx.tblCategory.Where(x => x.Id == request.Id).FirstOrDefault();
            model.CategoryName = request.CategoryName;
            model.TopCategoryId = request.TopCategoryId;
            model.IsDeleted = false;
            model.IsActive = true;

            int result = ctx.SaveChanges();

            if (result > 0)
            {
                value = true;
            }
            else
            {
                value = false;
            }

            return value;

        }

        public bool Delete(int id)
        {
            bool value = false;
            var model = ctx.tblCategory.Find(id);
            model.IsActive = false;
            model.IsDeleted = true;
            List<tblProduct> pmodel = ctx.tblProduct.Where(x => x.CategoryId == id).ToList();
            foreach (var item in pmodel)
            {
                item.IsActive = false;
                item.IsDeleted = true;
                ctx.SaveChanges();
            }
            int result = ctx.SaveChanges();

            if (result > 0)
            {
                value = true;
            }
            else
            {
                value = false;
            }

            return value;

        }
    }
}
