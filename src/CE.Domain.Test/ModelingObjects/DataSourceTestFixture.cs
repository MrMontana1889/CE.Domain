// DataSourceTestFixture.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects.Tables;
using CE.Domain.ModelingObjects;
using NUnit.Framework;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace CE.Domain.Test.ModelingObjects
{
	[TestFixture]
    public class DataSourceTestFixture : DomainTestFixtureBase
    {
        #region Constructor
        public DataSourceTestFixture()
        {

        }
        #endregion

        #region Tests
        [Test]
        public void TestNew_FileDoesNotExist()
        {
            string filename = Path.GetTempFileName();
            try
            {
                File.Delete(filename);

                using (DataSource dataSource = new DataSource())
                {
                    dataSource.New(filename);

                    Assert.IsTrue(File.Exists(filename));
                    Assert.IsTrue(dataSource.IsOpen());

                    dataSource.Close();
                }
            }
            finally
            {
                if (File.Exists(filename))
                    File.Delete(filename);
            }
        }
        [Test]
        public void TestNew_FileExists()
        {
            string filename = Path.GetTempFileName();
            try
            {
                using (DataSource dataSource = new DataSource())
                {
                    Assert.Throws<FileLoadException>(new TestDelegate(
                        delegate
                        {
                            dataSource.New(filename);
                        }));
                    if (dataSource.IsOpen())
                        dataSource.Close();
                }
            }
            finally
            {
                if (File.Exists(filename))
                    File.Delete(filename);
            }
        }
        [Test]
        public void TestOpen()
        {
            string filename = Path.GetTempFileName();
            File.Delete(filename);

            try
            {
                using (DataSource dataSource = new DataSource())
                {
                    dataSource.New(filename);
                    Assert.IsTrue(dataSource.IsOpen());
                    Assert.IsTrue(File.Exists(filename));

                    dataSource.Close();
                    Assert.IsFalse(dataSource.IsOpen());

                    dataSource.Open(filename);
                    Assert.IsTrue(dataSource.IsOpen());
                    Assert.IsTrue(File.Exists(filename));

                    dataSource.Open(filename);
                    Assert.IsTrue(dataSource.IsOpen());

                    dataSource.Flush();

                    dataSource.Close();

                    Assert.Throws<InvalidOperationException>(new TestDelegate(
                        delegate
                        {
                            dataSource.Flush();
                        }));
                }
            }
            finally
            {
                if (File.Exists(filename))
                    File.Delete(filename);
            }
        }
        [Test]
        public void TestOpen_FileNotFound()
        {
            string filename = Path.GetTempFileName();
            File.Delete(filename);

            try
            {
                using (DataSource dataSource = new DataSource())
                {
                    Assert.Throws<FileNotFoundException>(new TestDelegate(
                        delegate
                        {
                            dataSource.Open(filename);
                        }));
                }
            }
            finally
            {
                if (File.Exists(filename))
                    File.Delete(filename);
            }
        }
        [Test]
        public void TestBackup()
        {
            string filename = Path.GetTempFileName();
            File.Delete(filename);

            try
            {
                using (DataSource dataSource = new DataSource())
                {
                    Assert.IsFalse(dataSource.IsOpen());
                    dataSource.New(filename);
                    Assert.IsTrue(dataSource.IsOpen());
                    Assert.IsTrue(File.Exists(filename));

                    string backup = Path.GetTempFileName();
                    File.Exists(backup);
                    Assert.IsTrue(dataSource.Backup(backup));

                    dataSource.Close();

                    Assert.IsFalse(dataSource.Backup(backup));

                    File.Delete(backup);
                }
            }
            finally
            {
                if (File.Exists(filename))
                    File.Delete(filename);
            }
        }
        [Test]
        public void TestNew_AttempRecreateTable()
        {
            string filename = Path.GetTempFileName();
            File.Delete(filename);

            using (DataSource dataSource = new DataSource())
            {
                dataSource.New(filename);
                Assert.IsTrue(dataSource.IsOpen());

                (new InfoTable()).Create(dataSource.GetDataConnection().DbConnection);

                dataSource.Close();
            }

            if (File.Exists(filename))
                File.Delete(filename);
        }
        [Test]
        public void TestVerifyTablesExist()
        {
            IList<IDomainTable> expectedTables = new List<IDomainTable>();

            expectedTables.Add(new InfoTable());

            string filename = Path.GetTempFileName();
            File.Delete(filename);

            using (DataSource dataSource = new DataSource())
            {
                dataSource.New(filename);

                foreach (IDomainTable table in expectedTables)
                {
                    Assert.IsTrue(dataSource.GetDataConnection().DbConnection.TableExists(table.Name));
                    Assert.IsTrue(table.IsNew);
                }

                Assert.IsFalse(dataSource.GetDataConnection().DbConnection.TableExists(CoreSchemaTableName.SETTING_V1));

                dataSource.Close();
            }

            File.Delete(filename);
        }
        [Test]
        public void TestBackupToInMemoryDatabase()
        {
            string filename = Path.GetTempFileName();
            File.Delete(filename);

            using (DataSource dataSource = new DataSource())
            {
                dataSource.New(filename);

                //Backing up to an in-memory database should return false.
                Assert.IsFalse(dataSource.Backup(":memory:"));

                dataSource.Close();
            }
        }
        #endregion
    }
}
