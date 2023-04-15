using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Part3_Mod24_Lib
{
    public class MainConnector
    {
        SqlConnection connection;
        public async Task <bool> ConnectAsync()
        {
            bool result;
            try
            {
                connection = new SqlConnection(ConnectionString.MsSqlConnection);
                await connection.OpenAsync();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                result = false;
            }
            return result;
        }
        public async void DisconnectAsync()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                await connection.CloseAsync();
            }
        }

        public SqlConnection GetConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                return connection;
            }
            else
            {
                throw new Exception("Connection closed");
            }
        }
    }
}
