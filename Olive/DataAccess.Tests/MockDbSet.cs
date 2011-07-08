// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockDbSet.cs" company="Olive">
//   
// </copyright>
// <summary>
//   Defines the MockDbSet type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Olive.DataAccess.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class MockDbSet<T> : IDbSet<T>
        where T : class
    {
        private readonly HashSet<T> _data;

        public MockDbSet()
        {
            this._data = new HashSet<T>();
        }

        public ObservableCollection<T> Local
        {
            get
            {
                return new ObservableCollection<T>(this._data);
            }
        }

        Type IQueryable.ElementType
        {
            get
            {
                return this._data.AsQueryable().ElementType;
            }
        }

        Expression IQueryable.Expression
        {
            get
            {
                return this._data.AsQueryable().Expression;
            }
        }

        IQueryProvider IQueryable.Provider
        {
            get
            {
                return this._data.AsQueryable().Provider;
            }
        }

        public T Add(T item)
        {
            this._data.Add(item);
            return item;
        }

        public T Attach(T item)
        {
            this._data.Add(item);
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

        public void Detach(T item)
        {
            this._data.Remove(item);
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        public T Remove(T item)
        {
            this._data.Remove(item);
            return item;
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this._data.GetEnumerator();
        }
    }
}