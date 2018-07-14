// Interfaces.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using CE.Domain.DataObjects.Sqlite;
using CE.Support.User;

namespace CE.Domain.ModelingObjects
{

	public delegate MessageHandlerResult AutoExecuteManualTransactionDelegate(int id);

    public interface IRepositoryFactory
    {
        ITableRepository<TTableType> CreateRepository<TTableType>(string name, ISqliteDataConnection dataConnection) where TTableType : class, IDomainTable;
    }

    public interface IJsonReader
    {
        void from_json(string json);
    }

    public interface IJsonWriter
    {
        string to_json();
    }
}
