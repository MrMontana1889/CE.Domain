// GenericSqliteDataConnection.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects.Library;
using CE.Domain.DataObjects.Tables;
using CE.Resource.Support;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace CE.Domain.DataObjects.Sqlite
{
	public class GenericSqliteDataConnection : IDataConnection, ISqliteDataConnection, IDisposable
    {
        #region Constructor
        public GenericSqliteDataConnection()
		{
			OrmLiteConfig.DialectProvider = SqliteDialect.Provider;

			RegisterLicense();
			RegisterConverters();

			Filename = string.Empty;
		}
		#endregion

		#region Public Methods
		public virtual void New(string filename)
        {
            if (IsOpen()) throw new InvalidOperationException(TextManager.Current["ConnectionAlreadyOpen"]);
            if (File.Exists(filename)) throw new FileLoadException(filename);

            SqliteLibrary.CreateSqliteDatabase(filename);
            Filename = filename;
            LoadSchema();
            Open(filename);
        }
        public void Open(string filename)
        {
            Filename = filename;
            OpenImpl();
        }
        public bool IsOpen()
        {
            return DbConnection != null && DbConnection.State == ConnectionState.Open;
        }
        public bool Backup(string filename)
        {
            if (!IsOpen()) return false;

            Flush(false);       //Flushign any pending changes but do not create a new transaction.

            using (SQLiteConnection conn = new SQLiteConnection(SqliteLibrary.GetConnectionString(filename)))
            {
                try
                {
                    SQLiteConnection db = DbConnection.ToDbConnection() as SQLiteConnection;
                    conn.Open();
                    db.BackupDatabase(conn, "main", "main", -1, null, 0);
                }
                finally
                {
                    conn.Close();
                }
            }

            //A backup to an in-memory database returns false
            if (filename == ":memory:")
                return false;

            return File.Exists(filename);
        }
        public void Flush()
        {
            Flush(true);
        }
        public void Flush(bool newTransaction)
        {
            lock (this)
            {
                if (currentTransaction != null) currentTransaction.Commit();
                currentTransaction = null;
                if (newTransaction && DbConnection != null && DbConnection.State != ConnectionState.Closed) currentTransaction = DbConnection.BeginTransaction();
            }
        }
        public void Close()
        {
            CloseConnection();
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

        #region Public Properties
        public IDbConnection DbConnection
        {
            get;
            private set;
        }
		#endregion

		#region Protected Methods
		protected virtual void RegisterConverters()
		{
			
		}
		protected virtual void RegisterLicense()
		{
		}
		protected virtual void OpenImpl()
        {
            if (!File.Exists(Filename)) throw new FileNotFoundException(Filename);
            if (!IsDataSourceImpl())
            {
                CloseConnection();
                throw new FileLoadException(TextManager.Current["DomainDatabaseFormatNotRecognized"]);
            }
            OpenConnection(false);
        }
        protected void OpenConnection(bool openExclusively)
        {
            if (!IsOpen())
            {
                OrmLiteConfig.DialectProvider = SqliteDialect.Provider;
                OrmLiteConfig.DialectProvider.NamingStrategy = new OrmLiteNamingStrategyBase();

                ConnectionFactory.ConnectionString = SqliteLibrary.GetConnectionString(Filename);
                DbConnection = ConnectionFactory.OpenDbConnection();
                currentTransaction = DbConnection.BeginTransaction();
            }
        }
        protected void CloseConnection()
        {
            if (DbConnection != null)
            {
                Flush(false);

                DbConnection.Close();
                DbConnection.Dispose();
            }

            DbConnection = null;
            connectionFactory = null;
        }
        protected virtual bool IsDataSourceBasic()
        {
            return DbConnection.TableExists(SchemaInfoTableName);
        }
        protected void LoadSchema()
        {
            try
            {
                OpenConnection(true);
				LoadCoreSchema();
                LoadSchemaImpl();
            }
            finally
            {
                CloseConnection();
            }
        }
		protected virtual void LoadCoreSchema()
		{
			(new InfoTable()).Create(DbConnection);
			(new InfoTable()).Create_Index(DbConnection);
		}
		protected virtual void LoadSchemaImpl()
        {

        }
        protected virtual void Dispose(bool disposing)
        {
            CloseConnection();
        }
        #endregion

        #region Protected Properties
        protected virtual string SchemaInfoTableName
        {
            get { return CoreSchemaTableName.INFOTABLE_V1; }
        }
        protected string Filename
        {
            get;
            set;
        }
        #endregion

        #region Private Methods
        protected bool IsDataSourceImpl()
        {
            OpenConnection(false);
            return IsDataSourceBasic();
        }
        #endregion

        #region Private Properties
        private OrmLiteConnectionFactory ConnectionFactory
        {
            get
            {
                if (connectionFactory == null)
                    connectionFactory = new OrmLiteConnectionFactory() { AutoDisposeConnection = true };
                return connectionFactory;
            }
        }
        #endregion

        #region Private Fields
        private OrmLiteConnectionFactory connectionFactory;
        private IDbTransaction currentTransaction;
        #endregion
    }
}
