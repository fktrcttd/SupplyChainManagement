
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using SCM.DataService.DataContext;

namespace SCM.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }
    } 
}