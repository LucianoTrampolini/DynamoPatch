using System.Data.Entity;

using Dynamo.Model.Base;

namespace Dynamo.Model.Context
{
    public class DynamoContext : DbContext,
        IDynamoContext
    {
        public DynamoContext()
            : base("Dynamo") {}

        public IDbSet<Band> Bands { get; set; }
        public IDbSet<BandType> BandTypes { get; set; }
        public IDbSet<BeheerderBericht> BeheerderBerichten { get; set; }
        public IDbSet<Beheerder> Beheerders { get; set; }
        public IDbSet<Bericht> Berichten { get; set; }
        public IDbSet<BerichtType> BerichtTypes { get; set; }
        public IDbSet<Betaling> Betalingen { get; set; }
        public IDbSet<Boeking> Boekingen { get; set; }
        public IDbSet<ChangeLog> ChangeLog { get; set; }
        public IDbSet<ContactPersoon> ContactPersoons { get; set; }
        public IDbSet<Contract> Contracten { get; set; }
        public IDbSet<Dagdeel> Dagdelen { get; set; }
        public IDbSet<Gesloten> Gesloten { get; set; }
        public IDbSet<Instelling> Instellingen { get; set; }
        public IDbSet<Oefenruimte> Oefenruimtes { get; set; }
        public IDbSet<OpgetredenFout> OpgetredenFouten { get; set; }
        public IDbSet<Planning> Planning { get; set; }
        public IDbSet<PlanningsDag> PlanningsDagen { get; set; }
        public IDbSet<Taak> Taken { get; set; }
        public IDbSet<Tarief> Tarieven { get; set; }
        public IDbSet<Vergoeding> Vergoedingen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            MapBase<OpgetredenFout>(modelBuilder);
            MapBase<Beheerder>(modelBuilder);
            MapBase<Band>(modelBuilder);
            MapBase<Contract>(modelBuilder);
            MapBase<ContactPersoon>(modelBuilder);
            MapBase<BandType>(modelBuilder);
            MapBase<PlanningsDag>(modelBuilder);
            MapBase<Planning>(modelBuilder);
            MapBase<Boeking>(modelBuilder);
            modelBuilder.Entity<Boeking>()
                .HasOptional(x => x.Band)
                .WithMany()
                .HasForeignKey(x => x.BandId);
            MapBase<Oefenruimte>(modelBuilder);
            MapBase<Tarief>(modelBuilder);
            MapBase<Dagdeel>(modelBuilder);
            MapBase<Taak>(modelBuilder);
            MapBase<Betaling>(modelBuilder);
            MapBase<Vergoeding>(modelBuilder);
            MapBase<Gesloten>(modelBuilder);
            MapBase<BerichtType>(modelBuilder);
            MapBase<Bericht>(modelBuilder);
            //modelBuilder.Entity<Bericht>().
            //  HasMany(b => b.Beheerders).
            //  WithMany(p => p.Berichten).
            //  Map(m =>
            //   {
            //       m.MapLeftKey("BerichtId");
            //       m.MapRightKey("BeheerderId");
            //       m.ToTable("BeheerderBerichten");
            //   });
            MapBase<BeheerderBericht>(modelBuilder);
            MapBase<ChangeLog>(modelBuilder);
            MapBase<Instelling>(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void MapBase<T>(DbModelBuilder modelBuilder) where T : ModelBase, new()
        {
            T t = new T();
            modelBuilder.Entity<T>()
                .ToTable(
                    t.GetType()
                        .Name);
            modelBuilder.Entity<T>()
                .HasOptional(x => x.GewijzigdDoor)
                .WithMany()
                .HasForeignKey(b => b.GewijzigdDoorId);
            modelBuilder.Entity<T>()
                .HasOptional(x => x.AangemaaktDoor)
                .WithMany()
                .HasForeignKey(b => b.AangemaaktDoorId);
        }
    }
}