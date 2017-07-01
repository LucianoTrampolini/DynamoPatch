using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dynamo.Model;

namespace Dynamo.BL.Base
{
    public abstract class BusinessRuleBase<E> : IDisposable where E : Model.Base.ModelBase 
    {
        private IDynamoContext _currentContext = null;

        public BusinessRuleBase(IDynamoContext context)
        {
            _currentContext = context;    
        }

        public virtual bool Execute(E entity)
        {
            throw new NotImplementedException("Implementeer in afgeleide");
        }

        public void Dispose()
        {
            OnDispose();
        }

        public virtual void OnDispose()
        { }
    }
}
