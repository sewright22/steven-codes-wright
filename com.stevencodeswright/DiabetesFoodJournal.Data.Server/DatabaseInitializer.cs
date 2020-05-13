using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesFoodJournal.Data.Server
{
    public class DatabaseInitializer : CreateDatabaseIfNotExists<DiabetesFoodJournalContext>
    {
        protected override void Seed(DiabetesFoodJournalContext context)
        {
            base.Seed(context);

            context.SaveChanges();
        }
    }
}
