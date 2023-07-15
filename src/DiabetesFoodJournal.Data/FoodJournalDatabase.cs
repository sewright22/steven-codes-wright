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
    public interface IFoodJournalDatabase
    {
        SQLiteAsyncConnection Database { get; }
    }

    public class FoodJournalDatabase : IFoodJournalDatabase
    {
        public SQLiteAsyncConnection Database { get; private set; }
        static bool initialized = false;

        public FoodJournalDatabase(ISqlLiteAsyncConnectionFactory sqlLiteAsyncConnectionFactory)
        {
            InitializeAsync(sqlLiteAsyncConnectionFactory).SafeFireAndForget(false);
        }

        private async Task InitializeAsync(ISqlLiteAsyncConnectionFactory sqlLiteAsyncConnectionFactory)
        {
            if (!initialized)
            {
                Database = sqlLiteAsyncConnectionFactory.BuildConnection().Value;

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(JournalEntry).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(JournalEntry)).ConfigureAwait(false);
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Dose).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Dose)).ConfigureAwait(false);
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(NutritionalInfo).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(NutritionalInfo)).ConfigureAwait(false);
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Tag).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Tag)).ConfigureAwait(false);
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(JournalEntryDose).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(JournalEntryDose)).ConfigureAwait(false);
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(JournalEntryNutritionalInfo).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(JournalEntryNutritionalInfo)).ConfigureAwait(false);
                }

                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(JournalEntryTag).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(JournalEntryTag)).ConfigureAwait(false);
                }
                initialized = true;
            }
        }
    }
}
