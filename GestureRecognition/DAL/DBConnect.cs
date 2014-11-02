using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace GestureRecognition.DAL
{
    class DbConnect
    {
        private MySqlConnection _connection;
        private string _server;
        private string _database;
        private string _uid;
        private string _password;

        public DbConnect()
        {
            Initialize();
        }

        private void Initialize()
        {
            _server = ConfigurationManager.AppSettings["serverDatabase"];
            _database = ConfigurationManager.AppSettings["database"];
            _uid = ConfigurationManager.AppSettings["username"];
            _password = ConfigurationManager.AppSettings["password"];
            var connectionString = "SERVER=" + _server + ";" + "DATABASE=" +
                                      _database + ";" + "UID=" + _uid + ";" + "PASSWORD=" + _password + ";";

            _connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        throw new Exception("Cannot connect to server.  Contact administrator");
                    case 1045:
                        throw new Exception("Invalid username/password, please try again");
                }
                return false;
            }
        }

        //Close connection
        private void CloseConnection()
        {
            try
            {
                _connection.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal void ExecuteNonQuery(string query)
        {
            if (!OpenConnection()) return;
            var cmd = new MySqlCommand(query, _connection);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        internal List<T> ExecuteReader<T>(string query, Func<MySqlDataReader,T> tFunc)
        {
            if(!OpenConnection())
                return null;

            var cmd = new MySqlCommand(query, _connection);
            var dataReader = cmd.ExecuteReader();
            var list = new List<T>();
            while (dataReader.Read())
            {
                list.Add(tFunc(dataReader));
            }
            dataReader.Close();
            CloseConnection();
            return list;
        }

        internal int ExecuteScalar(string query)
        {
            if (!OpenConnection())
                return 0;
            var cmd = new MySqlCommand(query, _connection);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
