//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarketAPP.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblProductPicture
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