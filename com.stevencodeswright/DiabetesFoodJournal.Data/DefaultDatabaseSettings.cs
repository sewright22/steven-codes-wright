using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DiabetesFoodJournal.Data
{
    public class DefaultDatabaseSettings : IDatabaseSettings
    {
        public string DbPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FoodDatabase.db3");

        public SQLiteOpenFlags Flags => // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;
    }
}
