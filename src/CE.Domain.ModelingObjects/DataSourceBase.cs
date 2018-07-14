// DataSourceBase.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects;
using CE.Domain.DataObjects.Sqlite;
using CE.Resource.Support;
using System;
using System.IO;

namespace CE.Domain.ModelingObjects
{
    public abstract class DataSourceBase : IDataSource, IDataConnection, IDisposable
    {
        #region Constructor
        public DataSourceBase()
        {

        }
        #endregion

        #region Public Methods
        public void New(string filename)
        {
            if (File.Exists(filename)) throw new FileLoadException(filename);
            DataConnection.New(filename);
        }
        public void Open(string filename)
        {
            if (IsOpen())
            {
                Close();
            }

            DataConnection.Open(filename);
        }
        public bool IsOpen()
        {
            if (dataConnection != null)
                return DataConnection.IsOpen();
            return false;
        }
        public bool Backup(string filename)
        {
            if (!IsOpen()) return false;

            //Must be open in order to execute backup.
            return DataConnection.Backup(filename);
        }
        public void Flush()
        {
            if (!IsOpen()) throw new InvalidOperationException(TextManager.Current["ManagerIsNotOpen"]);
            DataConnection.Flush();
        }
        public void Close()
        {
            Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
        }
        public ISqliteDataConnection GetDataConnection()
        {
            return DataConnection;
        }
		#endregion

		#region Protected Methods
		protected abstract ISqliteDataConnection NewDataConnection();
        protected virtual void ResetConnection()
        {
            dataConnection = null;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (dataConnection != null)
            {
                DataConnection.Close();
                DataConnection.Dispose();
            }
            ResetConnection();
        }
        #endregion

        #region Protected Properties
        protected ISqliteDataConnection DataConnection
        {
            get
            {
                if (dataConnection == null)
                    dataConnection = NewDataConnection();
                return dataConnection;
            }
        }
        #endregion

        #region Private Fields
        private ISqliteDataConnection dataConnection;
        #endregion
    }
}
