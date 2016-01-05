using System.Data.Entity;
using MaxExperiment.Util;

namespace MaxExperiment.Model
{
    public class SQLiteConfig : DbConfiguration
    {
        public SQLiteConfig()
        {
            SetDefaultConnectionFactory(new SQLiteConnector()); 
        }
    }
}
