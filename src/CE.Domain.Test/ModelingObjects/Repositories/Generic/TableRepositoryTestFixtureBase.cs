// TableRepositoryTestFixtureBase.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.ModelingObjects;
using FluentAssertions;
using NUnit.Framework;
using System.IO;

namespace CE.Domain.Test.ModelingObjects.Repositories.Generic
{
	public abstract class TableRepositoryTestFixtureBase<TTableType> where TTableType : class, IDomainTable
    {
        #region Constructor
        public TableRepositoryTestFixtureBase()
        {

        }
        #endregion

        #region Setup/Teardown
        [OneTimeSetUp]
        public void Initialize()
        {
            Filename = Path.GetTempFileName();
			DataSource = new DataSource();

			if (File.Exists(Filename)) File.Delete(Filename);
            DataSource.New(Filename);
        }
        [OneTimeTearDown]
        public void Cleanup()
        {
            if (DataSource != null)
                DataSource.Close();
            DataSource = null;

            //File.Copy(Filename, @"D:\Desktop\TestModel.sqlite", true);
            if (File.Exists(Filename)) File.Delete(Filename);
            Filename = null;
        }
        #endregion

        #region Tests
        [Test, Order(1)]
        public void TestCRUD_Create()
        {
            ITableRepository<TTableType> repository = DataSource.DataRepository.GetTableRepositoryFor<TTableType>(TableName);
            Assert.AreEqual(TableName, repository.Name);

            TTableType data = repository.Add(NewDomainTableData());
            data.Should().NotBeNull();
            TableID = data.Id;

            TableID.Should().BeGreaterThan(0);
        }
        [Test, Order(2)]
        public void TestCRUD_Retrieve()
        {
            ITableRepository<TTableType> repository = DataSource.DataRepository.GetTableRepositoryFor<TTableType>(TableName);
            Assert.AreEqual(TableName, repository.Name);

            TTableType actual = repository.Find(TableID);
            actual.Should().NotBeNull();

            Assert.IsTrue(CompareData(actual, NewDomainTableData()));

            actual = repository[TableID];
            actual.Should().NotBeNull();

            Assert.IsTrue(CompareData(actual, NewDomainTableData()));

            var items = repository.Items(t => t.Id == TableID);
            items.Count.Should().Be(1);

            Assert.IsTrue(CompareData(items[0], NewDomainTableData()));
        }
        [Test, Order(3)]
        public void TestCRUD_Update()
        {
            ITableRepository<TTableType> repository = DataSource.DataRepository.GetTableRepositoryFor<TTableType>(TableName);
            Assert.AreEqual(TableName, repository.Name);

            TTableType actual = repository.Update(UpdatedTableData(TableID));
            actual.Should().NotBeNull();

            actual.Id.Should().Be(TableID);

            Assert.IsTrue(CompareData(actual, UpdatedTableData(TableID)));
        }
        [Test, Order(4)]
        public void TestCRUD_Delete()
        {
            ITableRepository<TTableType> repository = DataSource.DataRepository.GetTableRepositoryFor<TTableType>(TableName);
            Assert.AreEqual(TableName, repository.Name);

            TTableType actual = repository.Find(TableID);
            actual.Should().NotBeNull();

            repository.Remove(TableID);

            actual = repository.Find(TableID);
            actual.Should().BeNull();
        }
        [Test, Order(5)]
        public void TestCRUD_Save()
        {
            //Handles if item is new or existing and either inserts or updates accordingly.
            ITableRepository<TTableType> repository = DataSource.DataRepository.GetTableRepositoryFor<TTableType>(TableName);
            Assert.AreEqual(TableName, repository.Name);

            TTableType newItem = NewDomainTableData();
            newItem.Should().NotBeNull();
            Assert.IsNotNull(newItem);

            TTableType actual = repository.Save(newItem);
            actual.Should().NotBeNull();

            Assert.IsTrue(CompareData(actual, newItem));

            TTableType updatedItem = UpdatedTableData(actual.Id);
            updatedItem.Should().NotBeNull();

            actual = repository.Save(updatedItem);
            Assert.IsTrue(CompareData(actual, updatedItem));
        }
        [Test, Order(6)]
        public void TestCRUD_AllItems()
        {
            ITableRepository<TTableType> repository = DataSource.DataRepository.GetTableRepositoryFor<TTableType>(TableName);
            Assert.AreEqual(TableName, repository.Name);

            repository.Add(NewDomainTableData());

            var items = repository.Items();
            items.Should().NotBeNull();
            items.Count.Should().BeGreaterThan(0);
        }
        #endregion

        #region Protected Methods
        protected abstract TTableType NewEmptyDomainTable();
        protected abstract TTableType NewDomainTableData();
        protected abstract TTableType UpdatedTableData(int id);
        protected abstract bool CompareData(TTableType actual, TTableType expected);
        protected abstract string GetSelectStatement(int tableID);
        #endregion

        #region Protected Properties
        protected string Filename
        {
            get;
            private set;
        }
        protected DataSource DataSource
        {
            get;
            private set;
        }
        protected abstract string TableName { get; }
        #endregion

        #region Private Fields
        private static int TableID;
        #endregion
    }
}
