using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DiabetesFoodJournal.Data
{
    public class SqlLiteAsyncConnectionFactory : ISqlLiteAsyncConnectionFactory
    {
        private readonly IDatabaseSettings databaseSettings;

        public SqlLiteAsyncConnectionFactory(IDatabaseSettings databaseSettings)
        {
            this.databaseSettings = databaseSettings;
        }

        public Lazy<SQLiteAsyncConnection> BuildConnection()
        {
            return new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(this.databaseSettings.DbPath, this.databaseSettings.Flags));
        }
    }

    public interface ISqlLiteAsyncConnectionFactory
    {
        Lazy<SQLiteAsyncConnection> BuildConnection();
    }
}
