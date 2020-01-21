namespace NTCService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NTCJournal")]
    public partial class NTCJournal
    {
        [Key]
        public int id_NTCjournal { get; set; }

        public int id_NTCclient { get; set; }

        public int id_NTCworker { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date_NTCjournal { get; set; }

        public TimeSpan? time_NTCjournal { get; set; }

        public int id_NTCservice { get; set; }

        public int? amount_NTCequipment { get; set; }

        public int? total_price { get; set; }

        public int? NTCworker_percentage { get; set; }

        public virtual NTCClients NTCClients { get; set; }

        public virtual NTCWorkers NTCWorkers { get; set; }

        public virtual NTCServices NTCServices { get; set; }
    }
}
