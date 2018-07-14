// SqliteLibraryTestFixture.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects.Library;
using CE.Domain.DataObjects.Sqlite;
using NUnit.Framework;
using System;
using System.IO;

namespace CE.Domain.Test.DataObjects.Library
{
	[TestFixture]
    public class SqliteLibraryTestFixture : DomainTestFixtureBase
    {
        #region Constructor
        public SqliteLibraryTestFixture()
        {

        }
        #endregion

        #region Setup/Teardown
        protected override void InitializeImpl()
        {
            Filename = Path.GetTempFileName();
            File.Delete(Filename);

            SQLiteDataConnection = new GenericSqliteDataConnection();
        }
        protected override void CleanupImpl()
        {
            if (SQLiteDataConnection != null)
            {
                SQLiteDataConnection.Close();
                SQLiteDataConnection.Dispose();
            }
            SQLiteDataConnection = null;

            if (Filename != null && File.Exists(Filename))
                File.Exists(Filename);
            Filename = null;
        }
        #endregion

        #region Tests
        [Test]
        public void TestCreateSqliteDatabase()
        {
            SqliteLibrary.CreateSqliteDatabase(Filename);
            Assert.IsTrue(File.Exists(Filename));
        }
        [Test]
        public void TestDateTimeToUnixEpoch()
        {
            double retVal = SqliteLibrary.DateTimeToUnixEpoch(DateTime.Now);
            Assert.IsTrue(retVal > 0.0);
        }
        [Test]
        public void TestGetStringForBoolean()
        {
            Assert.AreEqual(SqliteLibrary.TrueKeyword, SqliteLibrary.GetStringForBoolean(true));
            Assert.AreEqual(SqliteLibrary.FalseKeyword, SqliteLibrary.GetStringForBoolean(false));
        }
        #endregion

        #region Private Properties
        private ISqliteDataConnection SQLiteDataConnection
        {
            get;
            set;
        }
        #endregion
    }
}
