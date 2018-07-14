// GenericSqliteDataConnectionTestFixture.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects.Sqlite;
using NUnit.Framework;
using System;
using System.IO;

namespace CE.Domain.Test.DataObjects
{
	[TestFixture]
    public class GenericSqliteDataConnectionTestFixture : DomainTestFixtureBase
    {
        #region Constructor
        public GenericSqliteDataConnectionTestFixture()
        {

        }
        #endregion

        #region Setup/Teardown
        protected override void InitializeImpl()
        {
            SqlDataConnection = new GenericSqliteDataConnection();
        }
        protected override void CleanupImpl()
        {
            if (SqlDataConnection != null)
            {
                if (SqlDataConnection.IsOpen())
                    SqlDataConnection.Close();

                SqlDataConnection.Dispose();
            }

            SqlDataConnection = null;

            if (!string.IsNullOrEmpty(Filename) && File.Exists(Filename))
                File.Delete(Filename);
        }
        #endregion

        #region Tests
        [Test]
        public void TestNew_FileExists()
        {
            Filename = Path.GetTempFileName();

            Assert.Throws<FileLoadException>(new TestDelegate(
                delegate
                {
                    SqlDataConnection.New(Filename);
                }));

            File.Delete(Filename);
            SqlDataConnection.New(Filename);

            Assert.Throws<InvalidOperationException>(new TestDelegate(
                delegate
                {
                    SqlDataConnection.New(Filename);
                }));
        }
        [Test]
        public void TestNew()
        {
            Filename = Path.GetTempFileName();
            File.Delete(Filename);

            SqlDataConnection.New(Filename);
            Assert.IsTrue(SqlDataConnection.IsOpen());
        }
        [Test]
        public void TestOpen_InvalidFile()
        {
            Filename = Path.GetTempFileName();

            Assert.Throws<FileLoadException>(new TestDelegate(
                delegate
                {
                    SqlDataConnection.Open(Filename);
                }));
        }
        [Test]
        public void Test_FlushWneClosed()
        {
            SqlDataConnection.Flush();
            SqlDataConnection.Flush(true);
            SqlDataConnection.Flush(false);
        }
        [Test]
        public void TestFlush()
        {
            Filename = Path.GetTempFileName();
            File.Delete(Filename);

            SqlDataConnection.New(Filename);

            SqlDataConnection.Flush();
            SqlDataConnection.Flush(true);
            SqlDataConnection.Flush(false);

            SqlDataConnection.DbConnection.Close();
            SqlDataConnection.Flush();
            SqlDataConnection.Flush(true);
            SqlDataConnection.Flush(false);
        }
        [Test]
        public void TestBackup()
        {
            Filename = Path.GetTempFileName();
            File.Delete(Filename);

            SqlDataConnection.New(Filename);

            string backup = Path.GetTempFileName();
            Assert.IsTrue(SqlDataConnection.Backup(backup));

            SqlDataConnection.Close();
            Assert.IsFalse(SqlDataConnection.Backup(backup));

            File.Delete(backup);
        }
        #endregion

        #region Private Properties
        private ISqliteDataConnection SqlDataConnection
        {
            get;
            set;
        }
        #endregion
    }
}
