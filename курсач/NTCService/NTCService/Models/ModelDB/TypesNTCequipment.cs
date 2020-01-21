namespace NTCService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypesNTCequipment")]
    public partial class TypesNTCequipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypesNTCequipment()
        {
            NTCequipmentInStock = new HashSet<NTCequipmentInStock>();
        }

        [Key]
        public int id_type_NTCequipment { get; set; }

        [StringLength(50)]
        public string name_type_NTCequipment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NTCequipmentInStock> NTCequipmentInStock { get; set; }
    }
}
