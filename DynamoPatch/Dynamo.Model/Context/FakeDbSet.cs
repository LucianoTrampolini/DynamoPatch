using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Dynamo.Model.Context
{
    /// <summary>
    /// Om de dbset te simuleren in de unittests
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FakeDbSet<T> : IDbSet<T>
        where T : class
    {
        #region Member fields

        private readonly HashSet<T> _data;

        #endregion

        public FakeDbSet()
        {
            _data = new HashSet<T>();
        }

        public ObservableCollection<T> Local
        {
            get { return new ObservableCollection<T>(_data); }
        }

        Type IQueryable.ElementType
        {
            get
            {
                return _data.AsQueryable()
                    .ElementType;
            }
        }

        Expression IQueryable.Expression
        {
            get
            {
                return _data.AsQueryable()
                    .Expression;
            }
        }

        IQueryProvider IQueryable.Provider
        {
            get
            {
                return _data.AsQueryable()
                    .Provider;
            }
        }

        #region IDbSet<T> Members

        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        #endregion

        public void Detach(T item)
        {
            _data.Remove(item);
        }
    }
}