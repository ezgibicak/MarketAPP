using MarketAPP.Data;
using MarketAPP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MarketAPP.Business
{
    public class ProductBS
    {
        private readonly MarketAPPEntities ctx;
        public ProductBS()
        {
            if (ctx == null)
            {
                ctx = new MarketAPPEntities();
            }
        }

        public List<ProductViewModel> GetAll()
        {
            List<ProductViewModel> model = ctx.tblProduct.Where(x => x.IsActive == true && x.IsDeleted == false).Select(x => new ProductViewModel
            {

                CategoryId = x.CategoryId,
                Cost = x.Cost,
                Detail = x.Detail,
                IsActive = x.IsActive,
                ProductName = x.ProductName,
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                CategoryName = ctx.tblCategory.Where(z => z.Id == x.CategoryId).FirstOrDefault().CategoryName,
                FilePath = ctx.tblProductPicture.Where(z => z.ProductId == x.Id && z.IsActive == true && z.IsDeleted == false).FirstOrDefault().PicturePath,

            }).ToList();

            return model;
        }
        public List<ProductViewModel> Index(int id)
        {
            List<ProductViewModel> model = new List<ProductViewModel>();
            List<ProductViewModel> basemodel = new List<ProductViewModel>();
            tblCategory cg = ctx.tblCategory.Find(id);
            if (cg.TopCategoryId != null)
            {
                basemodel = ctx.tblProduct.Where(x => x.CategoryId == id && x.IsDeleted == false && x.IsActive == true).Select(x => new ProductViewModel
                {

                    CategoryId = x.CategoryId,
                    Cost = x.Cost,
                    Detail = x.Detail,
                    IsActive = x.IsActive,
                    ProductName = x.ProductName,
                    Id = x.Id,
                    IsDeleted = x.IsDeleted,
                    CategoryName = ctx.tblCategory.Where(z => z.Id == id).FirstOrDefault().CategoryName,
                    TopCategoryId = ctx.tblCategory.Where(z => z.Id == id).FirstOrDefault().TopCategoryId,
                    FilePath = ctx.tblProductPicture.Where(z => z.ProductId == x.Id && z.IsActive == true && z.IsDeleted == false).FirstOrDefault().PicturePath,
                }).ToList();
            }
            else
            {
                List<tblCategory> vmodel = ctx.tblCategory.Where(x => x.TopCategoryId == id && x.IsDeleted == false && x.IsActive == true).ToList();
                foreach (var item in vmodel)
                {
                    model = ctx.tblProduct.Where(x => x.CategoryId == item.Id).Select(x => new ProductViewModel
                    {

                        CategoryId = x.CategoryId,
                        Cost = x.Cost,
                        Detail = x.Detail,
                        IsActive = x.IsActive,
                        ProductName = x.ProductName,
                        Id = x.Id,
                        IsDeleted = x.IsDeleted,
                        CategoryName = ctx.tblCategory.Where(z => z.Id == item.Id).FirstOrDefault().CategoryName,
                        TopCategoryId = ctx.tblCategory.Where(z => z.Id == item.Id).FirstOrDefault().TopCategoryId,
                        FilePath = ctx.tblProductPicture.Where(z => z.ProductId == x.Id && z.IsActive == true && z.IsDeleted == false).FirstOrDefault().PicturePath,
                    }).ToList();
                    foreach (var item2 in model)
                    {
                        basemodel.Add(item2);
                    }
                }
            }

            return basemodel;
        }
        public bool Create(ProductViewModel request, IEnumerable<HttpPostedFileBase> pics)
        {
            bool value = false;
            tblProduct model = new tblProduct();
            model.ProductName = request.ProductName;
            model.IsDeleted = false;
            model.IsActive = true;
            model.Cost = request.Cost;
            model.Detail = request.Detail;
            model.CategoryId = request.CategoryId;

            ctx.tblProduct.Add(model);
            int result = ctx.SaveChanges();
            tblProductPicture pmodel = new tblProductPicture();
            int i = 0;
            foreach (var file in pics)
            {
                if (file != null)
                {
                    var name = file.FileName.ToString();
                    var extension = name.Split('.')[1];

                    if (file.ContentLength > 0)
                    {
                        using (var binaryReader = new BinaryReader(file.InputStream))
                        {
                            tblProductPicture pcf2 = new tblProductPicture
                            {

                                ProductId = model.Id,
                                IsDeleted = false,
                                IsActive = true,
                                PicturePath = request.FileLocationPic[i],
                                FileName = name,
                                FileExtension = extension
                            };
                            i++;
                            pmodel = pcf2;
                            ctx.tblProductPicture.Add(pcf2);
                            ctx.SaveChanges();

                        }
                    }
                }
            }

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

        public ProductViewModel LoadEdit(int id)
        {

            tblProduct model = ctx.tblProduct.Find(id);
            ProductViewModel vmodel = new ProductViewModel();
            vmodel.CategoryId = model.CategoryId;
            vmodel.ProductName = model.ProductName;
            vmodel.Cost = model.Cost;
            vmodel.Detail = model.Detail;
            List<CategoryViewModel> categorylist = ctx.tblCategory.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                TopCategoryId = x.TopCategoryId,
                IsActive = x.IsActive,
                CategoryName = x.CategoryName,
                IsDeleted = x.IsDeleted


            }).ToList();
            vmodel.CategoryList = categorylist;


            List<ProductPictureViewModel> picmodel = ctx.tblProductPicture.Where(z => z.ProductId == model.Id && z.IsActive == true && z.IsDeleted == false).Select(x => new ProductPictureViewModel
            {

                PicturePath = x.PicturePath,
                FileExtension = x.FileExtension,
                ProductId = x.ProductId,
                Id = x.Id,

            }).ToList();
            vmodel.PicList = picmodel;
            return vmodel;
        }
        public bool Edit(ProductViewModel request, IEnumerable<HttpPostedFileBase> pics)
        {
            bool value = false;
            tblProduct model = ctx.tblProduct.Where(x => x.Id == request.Id).FirstOrDefault();
            model.ProductName = request.ProductName;
            model.IsDeleted = false;
            model.IsActive = true;
            model.Cost = request.Cost;
            model.Detail = request.Detail;
            model.CategoryId = request.CategoryId;
            int result = ctx.SaveChanges();

            tblProductPicture pmodel = new tblProductPicture();
            List<tblProductPicture> pmodelList = new List<tblProductPicture>();
            pmodelList = ctx.tblProductPicture.Where(x => x.ProductId == request.Id).ToList();

            if (pmodelList != null)
            {
                foreach (var item in pmodelList)
                {
                    item.IsActive = false;
                    item.IsDeleted = true;
                }
            }

            int i = 0;
            foreach (var file in pics)
            {
                if (file != null)
                {
                    var name = file.FileName.ToString();
                    var extension = name.Split('.')[1];

                    if (file.ContentLength > 0)
                    {
                        using (var binaryReader = new BinaryReader(file.InputStream))
                        {
                            tblProductPicture pcf2 = new tblProductPicture
                            {
                                ProductId = model.Id,
                                IsDeleted = false,
                                IsActive = true,
                                PicturePath = request.FileLocationPic[i],
                                FileName = name,
                                FileExtension = extension

                            };
                            i++;
                            pmodel = pcf2;
                            ctx.tblProductPicture.Add(pcf2);
                          

                        }
                    }
                }
            }
            int result2 = ctx.SaveChanges();
            if (result > 0 || result2>0)
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
            var model = ctx.tblProduct.Find(id);
            model.IsActive = false;
            model.IsDeleted = true;
            ctx.SaveChanges();
            return true;
        }

        public ProductViewModel Detail(int id)
        {
            ProductViewModel model = new ProductViewModel();

            model = ctx.tblProduct.Where(x => x.Id == id).Select(x => new ProductViewModel
            {

                CategoryId = x.CategoryId,
                Cost = x.Cost,
                Detail = x.Detail,
                IsActive = x.IsActive,
                ProductName = x.ProductName,
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                CategoryName = ctx.tblCategory.Where(z => z.Id == x.CategoryId).FirstOrDefault().CategoryName,
                TopCategoryId = ctx.tblCategory.Where(z => z.Id == x.CategoryId).FirstOrDefault().TopCategoryId

            }).FirstOrDefault();
            return model;


        }
    }
}

