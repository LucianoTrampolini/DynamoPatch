using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dynamo.BL.Base
{
    public interface IRepository<E> : IDisposable
    {
        void Delete(E entity);
        E Load(int Id);
        List<E> Load(Expression<Func<E, bool>> expression);
        List<E> Load();
        bool Save(E entity);
        void Undo(E entity);
    }
}