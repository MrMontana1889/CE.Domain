// DataRepository.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using System;
using CE.Domain.DataObjects;
using CE.Domain.DataObjects.Sqlite;
using CE.Domain.ModelingObjects.Repositories;

namespace CE.Domain.ModelingObjects
{
    public class DataRepository : IDataRepository
    {
        #region Constructor
        public DataRepository(ISqliteDataConnection dataConnection)
        {
            DataConnection = dataConnection;
        }
        #endregion

        #region Public Methods
        public ITableRepository<TTableType> GetTableRepositoryFor<TTableType>(string name) where TTableType : class, IDomainTable
        {
            return RepositoryFactory.CreateRepository<TTableType>(name, DataConnection);
        }
        #endregion

        #region Private Properties
        private ISqliteDataConnection DataConnection
        {
            get;
            set;
        }
        private IRepositoryFactory RepositoryFactory
        {
            get
            {
                if (_factory == null)
                    _factory = NewRepositoryFactory();
                return _factory;
            }
        }
		#endregion

		#region Protected Methods
		protected virtual IRepositoryFactory NewRepositoryFactory()
		{
			return new RepositoryFactory(this);
		}
		#endregion

		#region Private Fields
		private IRepositoryFactory _factory;
        #endregion
    }
}
