//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MemberType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MemberType()
        {
            this.Members = new HashSet<Member>();
        }
    
        public int MemberTypeId { get; set; }
        public string MemberType1 { get; set; }
        public int NoOfBookAllowed { get; set; }
        public int DaysAllowed { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Members { get; set; }
    }
}
