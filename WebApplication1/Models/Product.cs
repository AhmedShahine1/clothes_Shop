//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int Product_id { get; set; }
        public string Product_name { get; set; }
        public Nullable<int> Product_qty { get; set; }
        public Nullable<int> Product_price { get; set; }
        public string Product_desc { get; set; }
        public string Product_image { get; set; }
        public Nullable<int> Product_sizeid { get; set; }
        public Nullable<int> Product_Catagoryid { get; set; }
        public Nullable<int> Product_Colors { get; set; }
        public Nullable<int> Product_Brandid { get; set; }
    
        public virtual Brand Brand { get; set; }
        public virtual Catagory Catagory { get; set; }
        public virtual Color Color { get; set; }
        public virtual Size Size { get; set; }
    }
}
