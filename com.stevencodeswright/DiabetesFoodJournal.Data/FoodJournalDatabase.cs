using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using XamarinHelper.Core;
using DiabetesFoodJournal.Entities;

namespace DiabetesFoodJournal.Data
{
    public class FoodJournalDatabase
    {
        private readonly ISqlLiteAsyncConnectionFactory sqlLiteAsyncConnectionFactory;

        public SQLiteAsyncConnection Database { get; private set; }
        static bool initialized = false;

        public FoodJournalDatabase(ISqlLiteAsyncConnectionFactory sqlLiteAsyncConnectionFactory)
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        private async Task InitializeAsync()
        {
            if (!initialized)
            {
                Database = this.sqlLiteAsyncConnectionFactory.BuildConnection().Value;

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(JournalEntry).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(JournalEntry)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }
    }
}
