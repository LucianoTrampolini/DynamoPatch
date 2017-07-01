using System;

using Dynamo.Model;
using Dynamo.Model.Base;

namespace Dynamo.BL.Base
{
    public abstract class BusinessRuleBase<E> : IDisposable
        where E : ModelBase
    {
        #region Member fields

        private IDynamoContext _currentContext;

        #endregion

        public BusinessRuleBase(IDynamoContext context)
        {
            _currentContext = context;
        }

        #region IDisposable Members

        public void Dispose()
        {
            OnDispose();
        }

        #endregion

        public virtual bool Execute(E entity)
        {
            throw new NotImplementedException("Implementeer in afgeleide");
        }

        public virtual void OnDispose() {}
    }
}