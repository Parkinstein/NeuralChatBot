using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Vocabulary.DbContext
{
    using System.Data.Entity;
    using Models;

    public class VocabularyDb: DbContext
    {
        public VocabularyDb() : base("VocabularyDb")
        {
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<Phrase> Phrases { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
