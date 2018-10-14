using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace TicketStore.Core.Configurations
{
    public class DataContextConfiguration : DbConfiguration
    {
        public DataContextConfiguration()
        {
            
            this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance); //System.Data.Entity.Core.Common
        }
    }
}