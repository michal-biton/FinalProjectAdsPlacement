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
    
    public partial class PreferencePageTbl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PreferencePageTbl()
        {
            this.AdTbl = new HashSet<AdTbl>();
        }
    
        public int IDPreferencePage { get; set; }
        public Nullable<int> QuarterLengthPage { get; set; }
        public Nullable<int> QuarterWidthPage { get; set; }
        public Nullable<int> IDSize { get; set; }
        public Nullable<int> GradePlace { get; set; }
        public string DescriptionPreferencePage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdTbl> AdTbl { get; set; }
    }
}
