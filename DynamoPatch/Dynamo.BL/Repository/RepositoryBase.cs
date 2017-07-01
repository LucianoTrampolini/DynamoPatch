using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

using Dynamo.BL.Enum;
using Dynamo.Model;
using Dynamo.Model.Base;
using Dynamo.Model.Context;

namespace Dynamo.BL
{
    public abstract class RepositoryBase<E> : IDisposable
        where E : ModelBase
    {
        #region Member fields

        private BeheerderRepository _beheerderRepository;
        private IDynamoContext _currentContext;
        private readonly bool _isParentRepository;

        #endregion

        public RepositoryBase()
        {
            //Op het moment dat de repository wordt gemaakt zonder een context mee te geven, dan gaan we er van uit dat ie geen parent heeft. Aparte transactie...
            _isParentRepository = true;
        }

        public RepositoryBase(IDynamoContext context)
        {
            currentContext = context;
        }

        public bool AdminModus
        {
            get { return beheerderRepository.AdminBeheerder; }
            set { beheerderRepository.AdminBeheerder = value; }
        }

        public bool HasMelding { get; set; }

        public string Melding { get; set; }

        protected IDynamoContext currentContext
        {
            get
            {
                if (_currentContext == null)
                {
                    if (Application.Current == null)
                    {
                        //Wordt vanuit de unittesten gedraaid
                        _currentContext = FakeDynamoContext.GetInstance();
                    }
                    else
                    {
                        _currentContext = new DynamoContext();
                    }
                }
                return _currentContext;
            }
            set { _currentContext = value; }
        }

        private BeheerderRepository beheerderRepository
        {
            get
            {
                if (_beheerderRepository == null)
                {
                    _beheerderRepository = new BeheerderRepository(currentContext);
                }
                return _beheerderRepository;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_isParentRepository && _currentContext != null)
            {
                _currentContext.Dispose();
            }
            if (_beheerderRepository != null)
            {
                _beheerderRepository.Dispose();
            }
            OnDispose();
        }

        #endregion

        public virtual void Delete(E entity)
        {
            DeleteEntity(entity);
        }

        public List<Oefenruimte> GetOefenruimtes()
        {
            return currentContext.Oefenruimtes.ToList();
        }

        public DbEntityEntry GetOriginalEntry(ModelBase entity)
        {
            DbEntityEntry returnValue = null;

            switch (GetEntityName(entity))
            {
                case "Beheerder":
                    if (entity.IsTransient())
                    {
                        currentContext.Beheerders.Add((Beheerder)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(currentContext.Beheerders.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                case "Band":
                    if (entity.IsTransient())
                    {
                        currentContext.Bands.Add((Band)entity);
                    }
                    else
                    {
                        returnValue = currentContext.Entry(currentContext.Bands.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                case "Bericht":
                    if (entity.IsTransient())
                    {
                        currentContext.Berichten.Add((Bericht)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(currentContext.Berichten.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                case "BeheerderBericht":
                    if (entity.IsTransient())
                    {
                        currentContext.BeheerderBerichten.Add((BeheerderBericht)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(
                                currentContext.BeheerderBerichten.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                case "Boeking":
                    if (entity.IsTransient())
                    {
                        currentContext.Boekingen.Add((Boeking)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(currentContext.Boekingen.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                case "Gesloten":
                    if (entity.IsTransient())
                    {
                        currentContext.Gesloten.Add((Gesloten)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(currentContext.Gesloten.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                case "Instelling":
                    if (entity.IsTransient())
                    {
                        currentContext.Instellingen.Add((Instelling)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(currentContext.Instellingen.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                default:
                case "Planning":
                    if (entity.IsTransient())
                    {
                        currentContext.Planning.Add((Planning)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(currentContext.Planning.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                case "PlanningsDag":
                    if (entity.IsTransient())
                    {
                        currentContext.PlanningsDagen.Add((PlanningsDag)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(currentContext.PlanningsDagen.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                case "Vergoeding":
                    if (entity.IsTransient())
                    {
                        currentContext.Vergoedingen.Add((Vergoeding)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(currentContext.Vergoedingen.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                case "ContactPersoon":
                    if (entity.IsTransient())
                    {
                        currentContext.ContactPersoons.Add((ContactPersoon)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(currentContext.ContactPersoons.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                case "Contract":
                    if (entity.IsTransient())
                    {
                        currentContext.Contracten.Add((Contract)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(currentContext.Contracten.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
                case "Betaling":
                    if (entity.IsTransient())
                    {
                        currentContext.Betalingen.Add((Betaling)entity);
                    }
                    else
                    {
                        returnValue =
                            currentContext.Entry(currentContext.Betalingen.FirstOrDefault(x => x.Id == entity.Id));
                    }
                    break;
            }

            return returnValue;
        }

        public List<StamgegevenBase> GetStamGegevens(Stamgegevens stamgegevenType)
        {
            switch (stamgegevenType)
            {
                case Stamgegevens.Dagdelen:
                    var dagdelen = currentContext.Dagdelen;
                    return dagdelen.ToList<StamgegevenBase>();
                case Stamgegevens.Taken:
                    var taken = currentContext.Taken;
                    return taken.ToList<StamgegevenBase>();
                case Stamgegevens.BerichtTypes:
                    var berichtTypes = currentContext.BerichtTypes;
                    return berichtTypes.ToList<StamgegevenBase>();
                default:
                    return null;
            }
        }

        public void HandleChanges(ModelBase entity)
        {
            var currentBeheerder = beheerderRepository.CurrentBeheerder;

            bool hasChanged = false;
            var newEntry = currentContext.Entry(entity);
            DbEntityEntry oldEntry = GetOriginalEntry(entity);

            if (oldEntry == null)
            {
                if (entity.IsTransient())
                {
                    var changeLog = new ChangeLog();

                    changeLog.Datum = DateTime.Now;
                    changeLog.Entiteit = GetEntityName(entity);
                    changeLog.Omschrijving = entity.GetKorteOmschrijving();
                    changeLog.Eigenschap = "Nieuw gegeven";
                    changeLog.AangemaaktDoor = currentBeheerder;
                    currentContext.ChangeLog.Add(changeLog);

                    entity.AangemaaktDoor = currentBeheerder;
                    entity.Aangemaakt = DateTime.Now;
                }
            }
            else
            {
                var namesOfChangedProperties = oldEntry.CurrentValues.PropertyNames;

                foreach (var name in namesOfChangedProperties)
                {
                    var oudeWaarde = oldEntry.OriginalValues.GetValue<object>(name) ?? null;

                    object nieuweWaarde = newEntry.Property(name)
                        .CurrentValue ?? null;
                    if (!MyEquals(oudeWaarde, nieuweWaarde))
                    {
                        if (name.Equals("Gewijzigd")
                            && !MyDatumGroterDan(nieuweWaarde, oudeWaarde))
                        {
                            throw new InvalidOperationException(
                                string.Format(
                                    "Er is een gegeven op een ander tabblad aangepast, kan {0} niet updaten!",
                                    GetEntityName(entity)));
                        }
                        oldEntry.Property(name)
                            .CurrentValue = nieuweWaarde;
                        var changeLog = new ChangeLog();

                        changeLog.Datum = DateTime.Now;
                        changeLog.Entiteit = GetEntityName(entity);
                        changeLog.Omschrijving = entity.GetKorteOmschrijving();
                        changeLog.Eigenschap = name;
                        changeLog.OudeWaarde = oudeWaarde == null
                            ? null
                            : oudeWaarde.ToString();
                        changeLog.NieuweWaarde = nieuweWaarde == null
                            ? null
                            : nieuweWaarde.ToString();
                        changeLog.AangemaaktDoor = currentBeheerder;
                        currentContext.ChangeLog.Add(changeLog);
                        hasChanged = true;
                    }
                }
                if (hasChanged)
                {
                    oldEntry.Property("GewijzigdDoorId")
                        .CurrentValue = currentBeheerder.Id;
                    oldEntry.Property("Gewijzigd")
                        .CurrentValue = DateTime.Now;
                    hasChanged = false;
                }
            }

            HandleComplexPropertyChanges(entity);
        }

        public virtual E Load(int Id)
        {
            throw new NotImplementedException("Implementeer in afgeleide!");
        }

        public virtual List<E> Load(Expression<Func<E, bool>> expression)
        {
            throw new NotImplementedException("Implementeer in afgeleide!");
        }

        public virtual List<E> Load()
        {
            throw new NotImplementedException("Implementeer in afgeleide!");
        }

        public virtual void OnDispose() {}

        public virtual void Save(E entity, bool supressMessage = false)
        {
            try
            {
                SaveChanges(entity);
            }
            catch (InvalidOperationException ex)
            {
                if (supressMessage)
                {
                    return;
                }

                MessageBox.Show(ex.Message);
            }
        }

        public virtual void Undo(E entity)
        {
            UndoChanges(entity);
        }

        protected virtual void HandleComplexPropertyChanges(ModelBase entity) {}

        protected bool HasEntityChanged(ModelBase entity)
        {
            if (entity == null)
            {
                return false;
            }

            var band = entity as Band;
            if (band != null)
            {
                var originalEntry = GetOriginalEntry(entity);
                if (originalEntry != null
                    && originalEntry.Entity != null)
                {
                    var originalBand = originalEntry.Entity as Band;
                    return (originalBand != null && originalBand.Naam != band.Naam);
                }
            }

            var contract = entity as Contract;
            if (contract != null)
            {
                var originalEntry = GetOriginalEntry(entity);
                if (originalEntry != null
                    && originalEntry.Entity != null)
                {
                    var originalcontract = originalEntry.Entity as Contract;
                    return (originalcontract != null && (
                        originalcontract.Oefendag != contract.Oefendag ||
                            originalcontract.DagdeelId != contract.DagdeelId ||
                            originalcontract.OefenruimteId != contract.OefenruimteId
                        ));
                }
            }

            return false;
        }

        protected void SaveChanges(ModelBase entity)
        {
            HandleChanges(entity);
            currentContext.SaveChanges();
        }

        private void DeleteEntity(ModelBase entity)
        {
            entity.Verwijderd = true;
            SaveChanges(entity);
        }

        private string GetEntityName(ModelBase entity)
        {
            var entityName = entity.GetType()
                .Name;
            var pos = entityName.IndexOf("_");
            if (pos > 0)
            {
                entityName = entityName.Substring(0, pos);
            }
            return entityName;
        }

        private bool MyDatumGroterDan(object d1, object d2)
        {
            if (d2 == null
                && d1 != null)
            {
                return true;
            }

            if (d2 != null
                && d1 != null)
            {
                if (((DateTime)d1).CompareTo(((DateTime)d2)) > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool MyEquals(object o1, object o2)
        {
            if (o1 == o2)
            {
                return true;
            }

            if (o1 != null)
            {
                return o1.Equals(o2);
            }

            if (o2 != null)
            {
                return o2.Equals(o1);
            }
            return false;
        }

        private void UndoChanges(ModelBase entity)
        {
            var entry = currentContext.Entry(entity);

            if (entry.State == EntityState.Modified)
            {
                var namesOfChangedProperties = entry.CurrentValues.PropertyNames
                    .Where(
                        p => entry.Property(p)
                            .IsModified);

                foreach (var name in namesOfChangedProperties)
                {
                    entry.CurrentValues[name] = entry.OriginalValues[name];
                }
            }
        }
    }
}