// Interfaces.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using System.Data;

namespace CE.Domain.DataObjects.Sqlite
{
	public interface ISqliteDataConnection : IDataConnection
	{
		IDbConnection DbConnection { get; }

		void Flush(bool newTransaction);
	}
}
