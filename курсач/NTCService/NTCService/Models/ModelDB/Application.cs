namespace NTCService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Application")]
    public partial class Application
    {
        [Key]
        public int id_application { get; set; }

        [StringLength(30)]
        public string phone_number_NTCworker { get; set; }

        [StringLength(50)]
        public string email_NTCworker { get; set; }
    }
}
