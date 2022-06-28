//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dal
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserTbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserTbl()
        {
            this.AdTbl = new HashSet<AdTbl>();
            this.DealTbl = new HashSet<DealTbl>();
            this.PriorityTbl = new HashSet<PriorityTbl>();
        }
    
        public int IDUser { get; set; }
        public string IDCustomer { get; set; }
        public string UserFName { get; set; }
        public string UserLName { get; set; }
        public string Address { get; set; }
        public Nullable<int> AddressNum { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<int> IDPermission { get; set; }
        public Nullable<int> SumScore { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdTbl> AdTbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DealTbl> DealTbl { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriorityTbl> PriorityTbl { get; set; }
        public virtual PermissionTbl PermissionTbl { get; set; }
    }
}