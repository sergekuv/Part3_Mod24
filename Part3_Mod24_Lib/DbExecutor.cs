using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_Mod24_Lib
{
    public class DbExecutor
    {
        private MainConnector connector;
        public DbExecutor(MainConnector connector)
        {
            this.connector = connector;
        }

        public DataTable SelectAll(string table)    // Чтение для автономной модели
        {
            string selectcommandtext = "select * from " + table;
            SqlDataAdapter adapter = new(selectcommandtext, connector.GetConnection());
            DataSet ds = new();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public SqlDataReader SelectAllCommandReader(string table)   // Чтение для подключенной модели
        {
            SqlCommand command = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = "select * from " + table,
                Connection = connector.GetConnection()
            };
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                return reader;
            }
            return null;
        }

        public int DeleteByColumn(string table, string column, string value)
        {
            SqlCommand command = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = "delete from " + table + " where " + column + " = '" + value + "';",
                Connection = connector.GetConnection()
            };
            return command.ExecuteNonQuery();
        }

        public int ExecProcedureAdding(string name, string login)
        {
            SqlCommand command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddingUserProc",
                Connection = connector.GetConnection()
            };
            command.Parameters.Add(new SqlParameter("@Name", name));
            command.Parameters.Add(new SqlParameter("@Login", login));

            return command.ExecuteNonQuery();
        }

        public int ExecProcedureUpdate(string name, string login)
        {
            SqlCommand command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "dbo.UpdatingUserProc",
                Connection = connector.GetConnection()
            };
            command.Parameters.Add(new SqlParameter("@Name", name));
            command.Parameters.Add(new SqlParameter("@Login", login));

            return command.ExecuteNonQuery();
        }

        public int UpdateByColumn(string table, string columnToCheck, string valueCheck, string columnToUpdate, string valueUpdate)
        {
            SqlCommand command = new()
            {
                CommandType = CommandType.Text,
                CommandText = "update " + table + " set " + columnToUpdate + " = '" + valueUpdate + "' where " + columnToCheck + " = '" + valueCheck + "';",
                Connection = connector.GetConnection()
            };
            Console.WriteLine(command.CommandText);
            return command.ExecuteNonQuery();
        }


    }
}
