// InfoTableRepositoryTestFixture.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects.Tables;
using NUnit.Framework;

namespace CE.Domain.Test.ModelingObjects.Repositories.Generic
{
    [TestFixture]
    public class InfoTableRepositoryTestFixture : TableRepositoryTestFixtureBase<InfoTable>
    {
        #region Protected Methods
        protected override InfoTable NewEmptyDomainTable()
        {
            return new InfoTable();
        }
        protected override InfoTable NewDomainTableData()
        {
            return new InfoTable
            {
                InfoName = "TestName1",
                Value = "Value1"
            };
        }
        protected override bool CompareData(InfoTable actual, InfoTable expected)
        {
            return actual.InfoName == expected.InfoName &&
                actual.Value == expected.Value;
        }
        protected override InfoTable UpdatedTableData(int id)
        {
            return new InfoTable
            {
                Id = id,
                InfoName = "UpdatedName",
                Value = "UpdatedValue"
            };
        }
        protected override string GetSelectStatement(int tableID)
        {
            return string.Format("SELECT * FROM {0} WHERE {1}={2}", TableName, CoreSchemaFieldName.INFOID, tableID);
        }
        #endregion

        #region Protected Properties
        protected override string TableName
        {
            get { return CoreSchemaTableName.INFOTABLE_V1; }
        }
        #endregion
    }
}
