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
    
    public partial class PriorityTbl
    {
        public int IDPriority { get; set; }
        public Nullable<int> IDUser { get; set; }
        public Nullable<int> IDScore { get; set; }
    
        public virtual ScoreTbl ScoreTbl { get; set; }
        public virtual UserTbl UserTbl { get; set; }
    }
}