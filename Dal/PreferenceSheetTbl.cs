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
    
    public partial class PreferenceSheetTbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PreferenceSheetTbl()
        {
            this.AdTbl = new HashSet<AdTbl>();
        }
    
        public int IDPreferenceSheet { get; set; }
        public Nullable<int> ThirdSheet { get; set; }
        public Nullable<int> ExternalOrInternalPage { get; set; }
        public string DescriptionPreferenceSheet { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdTbl> AdTbl { get; set; }
    }
}