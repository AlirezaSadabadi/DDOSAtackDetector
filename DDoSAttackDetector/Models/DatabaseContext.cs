using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Models
{
    public class DatabaseContext : DbContext
    {
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}
        public DatabaseContext()
            : base("DADEntities")
        {

        }

        static DatabaseContext()
        {
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<DatabaseContext, Models.Migrations.Configurations>());
        }

        public DbSet<Fact> Facts { get; set; }
        public  DbSet<SymptomAndAttack> SymptomAndAttacks { get; set; }
        public DbSet<AttackLog> AttackLogs { get; set; }
        public DbSet<SymptomAndAttackRelation> SymptomAndAttackRelations { get; set; }
        public DbSet<CPT> CPTs { get; set; }

    }
}
