//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace prjFinalProject3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TableClasss1081747
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TableClasss1081747()
        {
            this.TableRoutines1081747 = new HashSet<TableRoutines1081747>();
        }
    
        public string ClaId { get; set; }
        public string ClaName { get; set; }
        public Nullable<int> ClaCost { get; set; }
        public Nullable<int> CouId { get; set; }
    
        public virtual TableCourses1081747 TableCourses1081747 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TableRoutines1081747> TableRoutines1081747 { get; set; }
    }
}
