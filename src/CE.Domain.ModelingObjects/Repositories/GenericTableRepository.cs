// GenericTableRepository.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects;
using CE.Domain.DataObjects.Sqlite;
using ServiceStack.OrmLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CE.Domain.ModelingObjects.Repositories
{
    public class GenericTableRepository<TTableType> : TableRepositoryBase<TTableType>, ITableRepository<TTableType> where TTableType : class, IDomainTable
    {
        #region Constructor
        public GenericTableRepository(string name, ISqliteDataConnection dataConnection)
            : base(dataConnection)
        {
            _name = name;
        }
        #endregion

        #region Public Methods
        public override TTableType Add(TTableType item)
        {
            try
            {
                DbConnection.Insert(item);
                item.Id = (int)DbConnection.LastInsertId();
                return item;
            }
            finally
            {
                Connection.Flush();
            }
        }
        public override TTableType Find(int id)
        {
            return DbConnection.SingleById<TTableType>(id);
        }
        public override TTableType Find(Expression<Func<TTableType, bool>> expression)
        {
            return DbConnection.Select(expression).FirstOrDefault();
        }
        public override bool Exists(Expression<Func<TTableType, bool>> expression)
        {
            return DbConnection.Exists(expression);
        }
        public override List<TTableType> Items()
        {
            return DbConnection.Select<TTableType>();
        }
        public override List<TTableType> Items(Expression<Func<TTableType, bool>> predicate)
        {
            return DbConnection.Select(predicate);
        }
        public override List<TTableType> Items(IEnumerable ids)
        {
            return DbConnection.SelectByIds<TTableType>(ids);
        }
        public override int GetCount()
        {
            return Convert.ToInt32(DbConnection.Count<TTableType>());
        }
        public override int GetCount(Expression<Func<TTableType, bool>> expression)
        {
            return Convert.ToInt32(DbConnection.Count(DbConnection.From<TTableType>().Where(expression)));
        }
        public override TTableType Load(int id)
        {
            return DbConnection.LoadSingleById<TTableType>(id);
        }
        public override List<TTableType> LoadItems()
        {
            return DbConnection.Select<TTableType>();
        }
        public override List<TTableType> LoadItems(Expression<Func<TTableType, bool>> predicate)
        {
            return DbConnection.LoadSelect(predicate);
        }
        public override void Remove(int id)
        {
            try { DbConnection.DeleteById<TTableType>(id); }
            finally { Connection.Flush(); }
        }
        public override TTableType Save(TTableType item)
        {
            try
            {
                DbConnection.Save(item, references: true);
                return item;
            }
            finally
            {
                Connection.Flush();
            }
        }
        public override int SaveAll(IEnumerable<TTableType> items)
        {
            try { return DbConnection.SaveAll(items); }
            finally { Connection.Flush(); }
        }
        public override TTableType Update(TTableType item)
        {
            try
            {
                DbConnection.Update(item);
                return item;
            }
            finally
            {
                Connection.Flush();
            }
        }
        #endregion

        #region Public Properties
        public override string Name
        {
            get { return _name; }
        }
        #endregion

        #region Protected Methods
        protected override TTableType GetItem(int id)
        {
            return Find(id);
        }
        #endregion

        #region Private Fields
        private string _name;
        #endregion
    }
}
