// DataSource.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects.Sqlite;

namespace CE.Domain.ModelingObjects
{
    public class DataSource : DataSourceBase, IRepositoryDataSource
    {
        #region Constructor
        public DataSource()
        {

        }
        #endregion

        #region Public Properties
        public IDataRepository DataRepository
        {
            get
            {
                if (dataRepository == null)
                    dataRepository = NewDataRepository();
                return dataRepository;
            }
        }
        #endregion

        #region Protected Methods
        protected override void Dispose(bool disposing)
        {
            dataRepository = null;
            base.Dispose(disposing);
        }
        protected virtual IDataRepository NewDataRepository()
        {
            return new DataRepository(DataConnection);
        }
		protected override ISqliteDataConnection NewDataConnection()
		{
			return new GenericSqliteDataConnection();
		}
		#endregion

		#region Private Fields
		private IDataRepository dataRepository;
        #endregion
    }
}
