namespace NTCService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NTCequipmentInStock")]
    public partial class NTCequipmentInStock
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NTCequipmentInStock()
        {
            NTCServices = new HashSet<NTCServices>();
        }

        [Key]
        public int id_NTCequipment { get; set; }

        [StringLength(50)]
        public string name_NTCequipment { get; set; }

        public int id_type_NTCequipment { get; set; }

        public int id_NTCprovider { get; set; }

        public int? amount_NTCequipment { get; set; }

        public virtual NTCequipmentProviders NTCequipmentProviders { get; set; }

        public virtual TypesNTCequipment TypesNTCequipment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NTCServices> NTCServices { get; set; }
    }
}
