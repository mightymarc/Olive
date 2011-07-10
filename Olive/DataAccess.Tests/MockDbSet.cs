// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockDbSet.cs" company="Olive">
//   Microsoft Public License (Ms-PL)
//
//    This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.
//    
//    1. Definitions
//    
//    The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
//    
//    A "contribution" is the original software, or any additions or changes to the software.
//    
//    A "contributor" is any person that distributes its contribution under this license.
//    
//    "Licensed patents" are a contributor's patent claims that read directly on its contribution.
//    
//    2. Grant of Rights
//    
//    (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
//    
//    (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.
//    
//    3. Conditions and Limitations
//    
//    (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
//    
//    (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
//    
//    (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
//    
//    (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
//    
//    (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
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

    /// <summary>
    /// The mock db set.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class MockDbSet<T> : IDbSet<T>
        where T : class
    {
        /// <summary>
        ///   The _data.
        /// </summary>
        private readonly HashSet<T> _data;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "MockDbSet{T}" /> class.
        /// </summary>
        public MockDbSet()
        {
            this._data = new HashSet<T>();
        }

        /// <summary>
        ///   Gets Local.
        /// </summary>
        public ObservableCollection<T> Local
        {
            get
            {
                return new ObservableCollection<T>(this._data);
            }
        }

        /// <summary>
        ///   Gets ElementType.
        /// </summary>
        Type IQueryable.ElementType
        {
            get
            {
                return this._data.AsQueryable().ElementType;
            }
        }

        /// <summary>
        ///   Gets Expression.
        /// </summary>
        Expression IQueryable.Expression
        {
            get
            {
                return this._data.AsQueryable().Expression;
            }
        }

        /// <summary>
        ///   Gets Provider.
        /// </summary>
        IQueryProvider IQueryable.Provider
        {
            get
            {
                return this._data.AsQueryable().Provider;
            }
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// </returns>
        public T Add(T item)
        {
            this._data.Add(item);
            return item;
        }

        /// <summary>
        /// The attach.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// </returns>
        public T Attach(T item)
        {
            this._data.Add(item);
            return item;
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// </returns>
        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// The create.
        /// </summary>
        /// <typeparam name="TDerivedEntity">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        /// <summary>
        /// The detach.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Detach(T item)
        {
            this._data.Remove(item);
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="keyValues">
        /// The key values.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// </returns>
        public T Remove(T item)
        {
            this._data.Remove(item);
            return item;
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// </returns>
        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._data.GetEnumerator();
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// </returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this._data.GetEnumerator();
        }
    }
}