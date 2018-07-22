// GlobalOptionsSqliteDataConnection.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects.Sqlite;
using CE.Domain.DataObjects.Tables;
using System.Collections.Generic;

namespace CE.Domain.DataObjects
{
    public class GlobalOptionsSqliteDataConnection : GenericSqliteDataConnection
    {
        #region Constructor
        public GlobalOptionsSqliteDataConnection()
        {

        }
		#endregion

		#region Protected Methods
		protected override void LoadCoreSchema()
		{
			List<ICreatableDomainTable> tablesToCreate = new List<ICreatableDomainTable>();
			tablesToCreate.Add(new Setting());
			tablesToCreate.Add(new Usage());

			foreach (ICreatableDomainTable table in tablesToCreate)
			{
				table.Create(DbConnection);
				table.Create_Index(DbConnection);
			}
		}
		protected override void LoadSchemaImpl()
        {
        }
        #endregion

        #region Protected Properties
        protected override string SchemaInfoTableName
        {
            get { return CoreSchemaTableName.SETTING_V1; }
        }
        #endregion
    }
}
