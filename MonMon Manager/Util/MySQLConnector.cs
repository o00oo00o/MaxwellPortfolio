using System.Windows;
using MySql.Data.MySqlClient;

namespace MaxExperiment.Util
{
    public class MySQLConnector : IDBConnector
    {
        private MySqlConnection connection;
        private string db_server;
        private string db_name;
        private string db_username;
        private string db_password;


        //Constructor
        public MySQLConnector()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            db_server = "mysql8.000webhost.com";
            db_name = "a6943757_cdn";
            db_username = "a6943757_admin";
            db_password = "kokoPop2701";

            string connectionString;
            connectionString = "SERVER=" + db_server + ";DATABASE=" +
            db_name + ";UID=" + db_username + ";PASSWORD=" + db_password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool Open()
        {
            bool result = false;

            try
            {
                connection.Open();
                result = true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        //Close connection
        public bool Close()
        {
            bool result = false;

            try
            {
                connection.Close();
                result = true;
            }
            catch
            {
            }

            return result;
        }
    }
}
