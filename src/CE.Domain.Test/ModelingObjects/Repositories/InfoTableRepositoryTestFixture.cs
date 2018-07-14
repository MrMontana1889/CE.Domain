// InfoTableRepositoryTestFixture.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects.Tables;
using CE.Domain.ModelingObjects;
using CE.Domain.ModelingObjects.Repositories;
using FluentAssertions;
using NUnit.Framework;
using System.IO;

namespace CE.Domain.Test.ModelingObjects.Repositories
{
	[TestFixture]
    public class InfoTableRepositoryTestFixture : DomainTestFixtureBase
    {
        #region Constructor
        public InfoTableRepositoryTestFixture()
        {

        }
        #endregion

        #region Setup/Teardown
        protected override void InitializeImpl()
        {
			DataSource = new DataSource();
			Filename = Path.GetTempFileName();
            File.Delete(Filename);
            DataSource.New(Filename);
        }
        protected override void CleanupImpl()
        {
            if (DataSource != null)
            {
                DataSource.Close();
                DataSource.Dispose();
            }

            DataSource = null;
            File.Delete(Filename);
        }
        #endregion

        #region Tests
        [Test]
        public void Get_all()
        {
            var repository = new InfoTableRepository(((DataSource)DataSource).GetDataConnection());
            repository.Should().NotBeNull();

            var items = repository.Items();
            items.Should().NotBeNull();
        }
        [Test]
        public void TestInfotableRepository()
        {
            ITableRepository<InfoTable> repository = DataSource.DataRepository.GetTableRepositoryFor<InfoTable>(SchemaTableName.INFOTABLE_V1);
            repository.Should().NotBeNull();

            var items = repository.Items();
            items.Should().NotBeNull();
        }
        #endregion
    }
}
