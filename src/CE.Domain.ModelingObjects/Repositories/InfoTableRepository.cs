// InfoTableRepository.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects;
using CE.Domain.DataObjects.Sqlite;
using CE.Domain.DataObjects.Tables;

namespace CE.Domain.ModelingObjects.Repositories
{
    public class InfoTableRepository : GenericTableRepository<InfoTable>
    {
        #region Constructor
        public InfoTableRepository(ISqliteDataConnection dataConnection)
            : base(CoreSchemaTableName.INFOTABLE_V1, dataConnection)
        {

        }
        #endregion
    }
}
