using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Dynamo.Model
{
    public interface IDynamoContext : IDisposable
    {
        IDbSet<Band> Bands { get; set; }
        IDbSet<BandType> BandTypes { get; set; }
        IDbSet<BeheerderBericht> BeheerderBerichten { get; set; }
        IDbSet<Beheerder> Beheerders { get; set; }
        IDbSet<Bericht> Berichten { get; set; }
        IDbSet<BerichtType> BerichtTypes { get; set; }
        IDbSet<Betaling> Betalingen { get; set; }
        IDbSet<Boeking> Boekingen { get; set; }
        IDbSet<ChangeLog> ChangeLog { get; set; }

        IDbSet<ContactPersoon> ContactPersoons { get; set; }
        IDbSet<Contract> Contracten { get; set; }
        IDbSet<Dagdeel> Dagdelen { get; set; }
        IDbSet<Gesloten> Gesloten { get; set; }
        IDbSet<Instelling> Instellingen { get; set; }
        IDbSet<Oefenruimte> Oefenruimtes { get; set; }
        IDbSet<OpgetredenFout> OpgetredenFouten { get; set; }
        IDbSet<Planning> Planning { get; set; }
        IDbSet<PlanningsDag> PlanningsDagen { get; set; }
        IDbSet<Taak> Taken { get; set; }
        IDbSet<Tarief> Tarieven { get; set; }
        IDbSet<Vergoeding> Vergoedingen { get; set; }

        DbEntityEntry Entry(object entity);
        int SaveChanges();
    }
}