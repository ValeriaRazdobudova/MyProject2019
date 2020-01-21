namespace NTCService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NTCClients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NTCClients()
        {
            NTCJournal = new HashSet<NTCJournal>();
        }

        [Key]
        public int id_NTCclient { get; set; }

        [StringLength(50)]
        public string name_NTCclient { get; set; }

        [StringLength(30)]
        public string phone_number_NTCclient { get; set; }

        [StringLength(50)]
        public string email_NTCclient { get; set; }

        [StringLength(50)]
        public string skype_NTCclient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NTCJournal> NTCJournal { get; set; }
    }
}
