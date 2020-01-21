namespace NTCService
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelDB : DbContext
    {
        public ModelDB()
            : base("name=ModelDB")
        {
        }

        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<NTCClients> NTCClients { get; set; }
        public virtual DbSet<NTCequipmentInStock> NTCequipmentInStock { get; set; }
        public virtual DbSet<NTCequipmentProviders> NTCequipmentProviders { get; set; }
        public virtual DbSet<NTCJournal> NTCJournal { get; set; }
        public virtual DbSet<NTCServices> NTCServices { get; set; }
        public virtual DbSet<NTCWorkers> NTCWorkers { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TypesNTCequipment> TypesNTCequipment { get; set; }
        public virtual DbSet<TypesNTCServices> TypesNTCServices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .Property(e => e.phone_number_NTCworker)
                .IsUnicode(false);

            modelBuilder.Entity<Application>()
                .Property(e => e.email_NTCworker)
                .IsUnicode(false);

            modelBuilder.Entity<NTCClients>()
                .Property(e => e.name_NTCclient)
                .IsUnicode(false);

            modelBuilder.Entity<NTCClients>()
                .Property(e => e.phone_number_NTCclient)
                .IsUnicode(false);

            modelBuilder.Entity<NTCClients>()
                .Property(e => e.email_NTCclient)
                .IsUnicode(false);

            modelBuilder.Entity<NTCClients>()
                .Property(e => e.skype_NTCclient)
                .IsUnicode(false);

            modelBuilder.Entity<NTCequipmentInStock>()
                .Property(e => e.name_NTCequipment)
                .IsUnicode(false);

            modelBuilder.Entity<NTCequipmentProviders>()
                .Property(e => e.name_NTCprovider)
                .IsUnicode(false);

            modelBuilder.Entity<NTCequipmentProviders>()
                .Property(e => e.phone_number_NTCprovider)
                .IsUnicode(false);

            modelBuilder.Entity<NTCequipmentProviders>()
                .Property(e => e.website_NTCprovider)
                .IsUnicode(false);

            modelBuilder.Entity<NTCequipmentProviders>()
                .Property(e => e.email_NTCprovider)
                .IsUnicode(false);

            modelBuilder.Entity<NTCequipmentProviders>()
                .Property(e => e.address_NTCprovider)
                .IsUnicode(false);

            modelBuilder.Entity<NTCServices>()
                .Property(e => e.name_NTCservice)
                .IsUnicode(false);

            modelBuilder.Entity<NTCWorkers>()
                .Property(e => e.name_NTCworker)
                .IsUnicode(false);

            modelBuilder.Entity<NTCWorkers>()
                .Property(e => e.phone_number_NTCworker)
                .IsUnicode(false);

            modelBuilder.Entity<NTCWorkers>()
                .Property(e => e.email_NTCworker)
                .IsUnicode(false);

            modelBuilder.Entity<NTCWorkers>()
                .Property(e => e.skype_NTCworker)
                .IsUnicode(false);

            modelBuilder.Entity<TypesNTCequipment>()
                .Property(e => e.name_type_NTCequipment)
                .IsUnicode(false);

            modelBuilder.Entity<TypesNTCServices>()
                .Property(e => e.name_type_NTCservice)
                .IsUnicode(false);
        }
    }
}
