// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockDataParameterCollection.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the MockDataParameterCollection type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public class MockDataParameterCollection : List<object>, IDataParameterCollection
    {
        public object this[string parameterName]
        {
            get
            {
                var parameter = this.Cast<IDbDataParameter>().SingleOrDefault(x => x.ParameterName == parameterName);

                if (parameter == null)
                {
                    throw new Exception(
                        string.Format("The parameter {0} is not contained in the collection.", parameterName));
                }

                return parameter;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool Contains(string parameterName)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(string parameterName)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(string parameterName)
        {
            throw new NotImplementedException();
        }
    }
}