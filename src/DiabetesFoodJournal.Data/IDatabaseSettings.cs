using SQLite;

namespace DiabetesFoodJournal.Data
{
    public interface IDatabaseSettings
    {
        string DbPath { get; }
        SQLiteOpenFlags Flags { get; }
    }
}