namespace NTCService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NTCServices
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NTCServices()
        {
            NTCJournal = new HashSet<NTCJournal>();
        }

        [Key]
        public int id_NTCservice { get; set; }

        [StringLength(50)]
        public string name_NTCservice { get; set; }

        public int? price_NTCservice { get; set; }

        public int id_NTCequipment { get; set; }

        public int id_type_NTCservice { get; set; }

        public virtual NTCequipmentInStock NTCequipmentInStock { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NTCJournal> NTCJournal { get; set; }

        public virtual TypesNTCServices TypesNTCServices { get; set; }
    }
}
