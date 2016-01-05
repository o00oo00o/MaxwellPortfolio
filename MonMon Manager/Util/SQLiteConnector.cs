using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;

namespace MaxExperiment.Util
{
    public class SQLiteConnector : IDBConnectorEF, IDbConnectionFactory
    {
        private SQLiteConnection connection;
        private SQLiteConnectionStringBuilder connectionString;

        #region CONSTRUCTOR
        public SQLiteConnector()
        {
        }

        public SQLiteConnector(string filename)
        {
            Initialize(filename, null);
        }

        public SQLiteConnector(string filename, string password)
        {
            Initialize(filename, password);
        }

        //Initialize values
        private void Initialize(string db_name, string db_password)
        {
            connectionString = new SQLiteConnectionStringBuilder();
            connectionString.DataSource = db_name;
            connectionString.Version = 3;

            if(!String.IsNullOrEmpty(db_password))
                connectionString.Password = db_password;

            connection = new SQLiteConnection(connectionString.ToString());
        }
        #endregion

        #region IDBConnectorEF
        //open connection to database
        public bool Open()
        {
            connection.Open();
            return true;
        }

        //Close connection
        public bool Close()
        {
            connection.Close();
            return true;
        }

        public DbConnection GetConnection()
        {
            return connection;
        }
        #endregion

        public string GetConnectionString()
        {
            return connectionString.ToString();
        }

        #region IDbConnectionFactory
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            return new SQLiteConnection(nameOrConnectionString);
        }
        #endregion
    }
}
