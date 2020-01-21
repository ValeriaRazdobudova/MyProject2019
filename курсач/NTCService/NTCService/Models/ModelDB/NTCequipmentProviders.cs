namespace NTCService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NTCequipmentProviders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NTCequipmentProviders()
        {
            NTCequipmentInStock = new HashSet<NTCequipmentInStock>();
        }

        [Key]
        public int id_NTCprovider { get; set; }

        [StringLength(50)]
        public string name_NTCprovider { get; set; }

        [StringLength(30)]
        public string phone_number_NTCprovider { get; set; }

        [StringLength(50)]
        public string website_NTCprovider { get; set; }

        [StringLength(50)]
        public string email_NTCprovider { get; set; }

        [StringLength(50)]
        public string address_NTCprovider { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NTCequipmentInStock> NTCequipmentInStock { get; set; }
    }
}
