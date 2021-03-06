//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Two
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Instock_Medicine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Instock_Medicine()
        {
            this.Suppliers1 = new HashSet<Supplier>();
        }
        [Display(Name = "Medicine ID")]
        public int MedID { get; set; }
        [Display(Name = "Medicine name")]
        public string MedName { get; set; }
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Price")]
        public Nullable<decimal> Price { get; set; }
        [Display(Name = "Supplier ID")]
        public Nullable<int> SupplierID { get; set; }
        [Display(Name = "Description")] 
        public string Description { get; set; }
        [Display(Name = "Image")]
        public byte[] Med_Img { get; set; }
        public string Img_Path { get; set; }
        public Nullable<System.DateTime> Instock_Date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supplier> Suppliers1 { get; set; }
    }
}
