using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ATS.DataAccess
{
    /// <summary>
    /// This interface specifies the signature for a factory that
    /// takes a DataReader and creates a domain object from it.
    /// </summary>
    /// <typeparam name="TDomainObject">type of domain object to create.</typeparam>
    public interface IDomainObjectFactory<TDomainObject>
    {
        TDomainObject Construct(IDataReader reader);
    }
}
