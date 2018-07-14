// DomainTableBase.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;
using System.Globalization;

namespace CE.Domain.DataObjects
{
    public abstract class DomainTableBase : EditableElementBase, ICreatableDomainTable
    {
        #region Constructor
        public DomainTableBase(string name)
        {
            Name = name;
        }
        #endregion

        #region Public Methods
        public virtual bool Create(IDbConnection dbConnection)
        {
            bool retVal = dbConnection.CreateTableIfNotExists(GetType());
            if (retVal)
                PostCreateTable(dbConnection);
            return retVal;
        }
        public virtual int Create_Index(IDbConnection dbConnection)
        {
            string sql = string.Format(CultureInfo.InvariantCulture,
                "CREATE INDEX IF NOT EXISTS [{0}] ON {1} ([{2}]);", IndexName, Name, IndexFieldName);
            return dbConnection.ExecuteSql(sql);
        }
        #endregion

        #region Public Properties
		public abstract int TableTypeID { get; }
        public abstract int Id { get; set; }
        [Ignore]
        public string Name
        {
            get;
            private set;
        }
        [Ignore]
        public bool IsNew
        {
            get { return Id == default(int); }
        }
        #endregion

        #region Protected Methods
        protected virtual void PostCreateTable(IDbConnection dbConnection)
        {
            //No-op by default - subclasses can override as required.
        }
        #endregion

        #region Protected Properties
        protected abstract string IndexName { get; }
        protected abstract string IndexFieldName { get; }
        #endregion
    }
}
