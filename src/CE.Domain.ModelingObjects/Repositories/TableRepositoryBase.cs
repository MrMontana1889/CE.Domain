// TableRepositoryBase.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects;
using CE.Domain.DataObjects.Sqlite;
using ServiceStack.OrmLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CE.Domain.ModelingObjects.Repositories
{
    public abstract class TableRepositoryBase<TTableType> : RepositoryBase, ITableRepository<TTableType> where TTableType : class, IDomainTable
    {
        #region Constructor
        public TableRepositoryBase(ISqliteDataConnection dataConnection)
            : base(dataConnection)
        {

        }
        #endregion

        #region Public Methods
        public abstract TTableType Add(TTableType item);
        public abstract TTableType Find(int id);
        public abstract TTableType Find(Expression<Func<TTableType, bool>> expression);
        public abstract List<TTableType> Items();
        public abstract List<TTableType> Items(Expression<Func<TTableType, bool>> predicate);
        public abstract List<TTableType> Items(IEnumerable ids);
        public abstract bool Exists(Expression<Func<TTableType, bool>> expression);
        public abstract TTableType Load(int id);
        public abstract List<TTableType> LoadItems();
        public abstract List<TTableType> LoadItems(Expression<Func<TTableType, bool>> predicate);
        public abstract void Remove(int id);
        public abstract TTableType Save(TTableType item);
        public abstract int SaveAll(IEnumerable<TTableType> items);
        public abstract TTableType Update(TTableType item);
        public abstract int GetCount();
        public abstract int GetCount(Expression<Func<TTableType, bool>> expression);
        public IList<int> IDs()
        {
            var q = DbConnection.From<TTableType>()
                .Select(t => new { t.Id });
            return DbConnection.SqlList<int>(q);
        }
        #endregion

        #region Public Properties
        public TTableType this[int id]
        {
            get { return GetItem(id); }
        }
        #endregion

        #region Protected Methods
        protected abstract TTableType GetItem(int id);
        protected virtual void PostLoad(TTableType item)
        {

        }
        #endregion
    }
}
