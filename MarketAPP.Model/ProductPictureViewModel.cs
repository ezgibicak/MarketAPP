using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketAPP.Model
{
   public  class ProductPictureViewModel
    {

        public int Id { get; set; }
        public Nullable<int> ProductId { get; set; }
        public string PicturePath { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
