// GlobalOptionsDataSourceTestFixture.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.ModelingObjects;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack.OrmLite;
using System.IO;

namespace CE.Domain.Test.ModelingObjects
{
    [TestFixture]
    public class GlobalOptionsDataSourceTestFixture : DomainTestFixtureBase
    {
        #region Constructor
        public GlobalOptionsDataSourceTestFixture()
        {

        }
        #endregion

        #region Tests
        [Test]
        public void TestNew()
        {
            string filename = Path.GetTempFileName();
            File.Delete(filename);

            using (IDataSource dataSource = new GlobalOptionsDataSource())
            {
                dataSource.New(filename);
                dataSource.IsOpen().Should().BeTrue();
                dataSource.Close();
            }

            File.Delete(filename);
        }
        [Test]
        public void TestOpen_Invalid()
        {
            string filename = Path.GetTempFileName();

            using (IDataSource dataSource = new GlobalOptionsDataSource())
            {
                Assert.Throws<FileLoadException>(new TestDelegate(
                    delegate
                    {
                        dataSource.Open(filename);
                    }));
            }

            File.Delete(filename);
        }
        [Test]
        public void TestOpen()
        {
            string filename = Path.GetTempFileName();
            File.Delete(filename);

            using (IDataSource dataSource = new GlobalOptionsDataSource())
            {
                dataSource.New(filename);
                dataSource.IsOpen().Should().BeTrue();

                dataSource.Close();
                dataSource.IsOpen().Should().BeFalse();
                dataSource.Open(filename);
                dataSource.IsOpen().Should().BeTrue();

                dataSource.Close();
            }

            File.Delete(filename);
        }
        [Test]
        public void TestTablesExist()
        {
            string filename = Path.GetTempFileName();
            File.Delete(filename);

            using (GlobalOptionsDataSource dataSource = new GlobalOptionsDataSource())
            {
                dataSource.New(filename);
                dataSource.IsOpen().Should().BeTrue();

                dataSource.GetDataConnection().DbConnection.TableExists(SchemaTableName.SETTING_V1).Should().BeTrue();
                dataSource.GetDataConnection().DbConnection.TableExists(SchemaTableName.USAGE_V1).Should().BeTrue();
                dataSource.GetDataConnection().DbConnection.TableExists(SchemaTableName.INFOTABLE_V1).Should().BeFalse();
            }
        }
        [Test]
        public void TestSetAndGetValues()
        {
            string filename = Path.GetTempFileName();
            File.Delete(filename);

            using (GlobalOptionsDataSource dataSource = new GlobalOptionsDataSource())
            {
                dataSource.New(filename);
                dataSource.IsOpen().Should().BeTrue();

                dataSource.SetGlobalSetting(SettingName.EXPAND_CATEGS_TREE, true);
                dataSource.SetGlobalSetting(SettingName.SHOWBEGINAPP, true);
                dataSource.SetGlobalSetting(SettingName.SHOW_HIDDEN_CATEGS, false);

                dataSource.SetGlobalSetting(SettingName.LASTFILENAME, filename);
                dataSource.SetGlobalSetting(SettingName.RECENT_DB_1, filename);

                dataSource.SetGlobalSetting(SettingName.SIZEW, 600);
                dataSource.SetGlobalSetting(SettingName.SIZEH, 800);

                dataSource.SetGlobalSetting(SettingName.VIEWACCOUNTS, "ALL");

                dataSource.GetGlobalSetting(SettingName.EXPAND_CATEGS_TREE, false).Should().BeTrue();
                dataSource.GetGlobalSetting(SettingName.SHOWBEGINAPP, true).Should().BeTrue();
                dataSource.GetGlobalSetting(SettingName.SHOW_HIDDEN_CATEGS, false).Should().BeFalse();

                dataSource.GetGlobalSetting<string>(SettingName.LASTFILENAME, string.Empty).Should().Be(filename);
                dataSource.GetGlobalSetting<string>(SettingName.RECENT_DB_1, string.Empty).Should().Be(filename);

                dataSource.GetGlobalSetting(SettingName.SIZEW, 100).Should().Be(600);
                dataSource.GetGlobalSetting(SettingName.SIZEH, 100).Should().Be(800);

                dataSource.GetGlobalSetting<string>(SettingName.VIEWACCOUNTS, "ALL").Should().Be("ALL");
            }

            File.Delete(filename);
        }
        #endregion
    }
}
