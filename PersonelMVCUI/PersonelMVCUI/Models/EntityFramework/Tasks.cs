//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonelMVCUI.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tasks
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tasks()
        {
            this.Sorunlar = new HashSet<Sorunlar>();
        }
    
        public int Id { get; set; }
        public string TITLE { get; set; }
        public int? CustomerId { get; set; }
        public System.DateTime CREATED_AT { get; set; }
        public DateTime? DEADLINE { get; set; }
        public Nullable<int> StatusId { get; set; }
        public int PersonelId { get; set; }
    
        public virtual Customers Customers { get; set; }
        public virtual Personel Personel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sorunlar> Sorunlar { get; set; }
        public virtual Status Status { get; set; }
    }
}