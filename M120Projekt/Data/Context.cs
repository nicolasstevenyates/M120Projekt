using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace M120Projekt.Data
{
    class Context : DbContext
    {
        public Context() : base("name=M120Connectionstring")
        {
            this.Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Data.Context, M120Projekt.Migrations.Configuration>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aufgabe>().ToTable("Aufgabe"); // Damit kein "s" angehängt wird an Tabelle
            modelBuilder.Entity<Aufgabensammlung>().ToTable("Aufgabensammlung"); // Damit kein "s" angehängt wird an Tabelle
        }
        public DbSet<Aufgabe> Aufgabe { get; set; }
        public DbSet<Aufgabensammlung> Aufgabensammlung { get; set; }
    }
}
