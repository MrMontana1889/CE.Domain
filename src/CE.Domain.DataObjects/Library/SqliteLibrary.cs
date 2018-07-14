// SqliteLibrary.cs
// Copyright (c) 2018 Culin Enterprises, Incorporated. All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;

namespace CE.Domain.DataObjects.Library
{
    public static class SqliteLibrary
    {
        public static readonly String NullKeyword = "NULL";
        public static readonly String FalseKeyword = "0";
        public static readonly String TrueKeyword = "1";
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);
        public static readonly int DecimalDigitsToRoundTo = 15;
        public static readonly Char EscapeChar = '\\';

        public static double DateTimeToUnixEpoch(DateTime dateTime)
        {
            TimeSpan timeSpan = dateTime.Subtract(UnixEpoch);
            return timeSpan.TotalSeconds;
        }
        public static void CreateSqliteDatabase(String fileName)
        {
            SQLiteConnection.CreateFile(fileName);

            using (SQLiteConnection conn = new SQLiteConnection(GetConnectionString(fileName)))
            {
                conn.Open();

                DbTransaction trans = conn.BeginTransaction();

                ConfigureSqliteDatabase(conn);

                trans.Commit();
            }
        }
        public static void ConfigureSqliteDatabase(SQLiteConnection conn)
        {
            List<String> setupCmds = new List<String>();
            setupCmds.Add("PRAGMA page_size = 8192;");
            setupCmds.Add("PRAGMA legacy_file_format = off;");
            setupCmds.Add("PRAGMA encoding = \"UTF-8\";");
            setupCmds.Add("PRAGMA foreign_keys = ON;");

            // Creating a dummy table so these pragma's are permanently stored
            setupCmds.Add("CREATE TABLE DummyTable(ID INTEGER PRIMARY KEY);");
            setupCmds.Add("DROP TABLE DummyTable;");

            foreach (String sql in setupCmds)
            {
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static String GetStringForBoolean(bool value)
        {
            return (value) ? TrueKeyword : FalseKeyword;
        }
        public static string GetConnectionString(string filename)
        {
            if (filename == ":memory:")
                return "Data Source=:memory:;Version=3;";

            return string.Format("Data Source={0};Version=3;", filename);
        }
    }
}
