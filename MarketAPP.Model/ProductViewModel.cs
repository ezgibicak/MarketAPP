using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAPP.Model
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Detail { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public List<CategoryViewModel> CategoryList { get; set; }
        public int? TopCategoryId { get; set; }
        public List<string> FileLocationPic { get; set; }
        public List<ProductPictureViewModel> PicList { get; set; }
        public string FilePath { get; set; }
    }
}
