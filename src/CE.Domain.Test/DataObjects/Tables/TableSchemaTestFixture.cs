// TableSchemaTestFixture.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects;
using CE.Domain.DataObjects.Tables;
using NUnit.Framework;
using ServiceStack.OrmLite;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace CE.Domain.Test.DataObjects.Tables
{
    [TestFixture]
    public class TableSchemaTestFixture : DomainTestFixtureBase
    {
        #region Constructor
        public TableSchemaTestFixture()
        {

        }
        #endregion

        #region Tests
        [Test]
        public void TestTableSchemas()
        {
            List<IDomainTable> tablesToCreate = new List<IDomainTable>();

            tablesToCreate.Add(new InfoTable());

            using (IDbConnection db = ":memory:".OpenDbConnection())
            {
                foreach (ICreatableDomainTable table in tablesToCreate)
                {
                    Assert.IsTrue(table.Create(db), "Create return false");
                    int retVal = table.Create_Index(db);
                    //TraceLibrary.WriteLine(TraceLevel.Info, table.Name + ":  " + retVal.ToString());

                    //var indices = db.GetDialectProvider().ToCreateIndexStatements(table.GetType());
                    //foreach (var item in indices)
                    //    item.PrintDump();

                    //var createTableSql = OrmLiteConfig.DialectProvider.ToCreateTableStatement(table.GetType());
                    //TraceLibrary.WriteLine(TraceLevel.Info, createTableSql);
                }

                SQLiteConnection sql = db.ToDbConnection() as SQLiteConnection;
                Assert.IsNotNull(sql);
                string filename = Path.GetFullPath(@"D:\Desktop\FromMemoryDb.sqlite");
                using (SQLiteConnection db2 = new SQLiteConnection(string.Format("data source={0};Version=3;New=True", filename)))
                {
                    db2.Open();
                    sql.BackupDatabase(db2, "main", "main", -1, null, 0);
                    db2.Close();
                }
            }
        }
        #endregion
    }
}
