//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Karizi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Portfolio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Portfolio()
        {
            this.PortfolioPhotoes = new HashSet<PortfolioPhoto>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public bool Active { get; set; }
        public System.DateTime CreateDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PortfolioPhoto> PortfolioPhotoes { get; set; }
    }
}
