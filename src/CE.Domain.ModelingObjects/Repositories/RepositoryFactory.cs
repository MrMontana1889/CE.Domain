// RepositoryFactory.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects.Sqlite;

namespace CE.Domain.ModelingObjects.Repositories
{
	public class RepositoryFactory : IRepositoryFactory
    {
        #region Constructor
        public RepositoryFactory(IDataRepository dataRepository)
        {
            DataRepository = dataRepository;
        }
        #endregion

        #region Public Methods
        public virtual ITableRepository<TTableType> CreateRepository<TTableType>(string name, ISqliteDataConnection dataConnection) where TTableType : class, IDomainTable
        {
            switch (name)
            {
                default:
                    return new GenericTableRepository<TTableType>(name, dataConnection);
            }
        }
        #endregion

        #region Protected Properties
        protected IDataRepository DataRepository
        {
            get;
            private set;
        }
        #endregion
    }
}
