using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MaxExperiment.Model
{
    public class DBContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public DBContext()
        {
            Initialize();
        }

        public DBContext(DbConnection connection) : base(connection, false)
        {
            Initialize();
        }

        private void Initialize()
        {
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<DBContext>(null);
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    } 
}
