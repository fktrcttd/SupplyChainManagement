
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace TicketStore.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<TicketStore.DataService.ApplicationDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    } 
}