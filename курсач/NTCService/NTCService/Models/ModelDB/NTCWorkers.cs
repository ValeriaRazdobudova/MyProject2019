namespace NTCService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NTCWorkers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NTCWorkers()
        {
            NTCJournal = new HashSet<NTCJournal>();
        }

        [Key]
        public int id_NTCworker { get; set; }

        [StringLength(50)]
        public string name_NTCworker { get; set; }

        [StringLength(30)]
        public string phone_number_NTCworker { get; set; }

        [StringLength(50)]
        public string email_NTCworker { get; set; }

        [StringLength(50)]
        public string skype_NTCworker { get; set; }

        public int? experience { get; set; }

        public int? salary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NTCJournal> NTCJournal { get; set; }
    }
}
