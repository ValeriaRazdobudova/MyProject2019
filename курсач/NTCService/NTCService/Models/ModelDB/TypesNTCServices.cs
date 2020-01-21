namespace NTCService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TypesNTCServices
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypesNTCServices()
        {
            NTCServices = new HashSet<NTCServices>();
        }

        [Key]
        public int id_type_NTCservice { get; set; }

        [StringLength(50)]
        public string name_type_NTCservice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NTCServices> NTCServices { get; set; }
    }
}
