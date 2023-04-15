using Microsoft.Data.SqlClient;
using Part3_Mod24_Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_Mod24_Console
{
    class Manager
    {
        private Table userTable;
        private MainConnector connector;
        private DbExecutor dbExecutor;
        public Manager()
        {
            connector = new MainConnector();

            userTable = new();
            userTable.Name = "NetworkUser";
            userTable.ImportantField = "Login";
            userTable.Fields.Add("id");
            userTable.Fields.Add("Name"); 
            userTable.Fields.Add("Login");
        }

        public void Connect()
        {
            var result = connector.ConnectAsync();
            if (result.Result)
            {
                Console.WriteLine("Connected");
                dbExecutor = new(connector);
            }
            else
            {
                Console.WriteLine("Connection error");
            }
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnecting from DB");
            connector.DisconnectAsync();
        }

        public void ShowDataDisconnected()
        {
            string tablename = "NetworkUser";
            Console.WriteLine("Disconnected Mode: getting data from table " + tablename);
            DataTable data = dbExecutor.SelectAll(tablename);
            Console.WriteLine("There are " + data.Rows.Count + " in table " + tablename);
            Console.WriteLine();

            foreach (DataColumn column in data.Columns)
            {
                Console.Write($"{column.ColumnName}\t");
            }
            Console.WriteLine();

            foreach (DataRow row in data.Rows)
            {
                object[] cells = row.ItemArray;
                foreach (object c in cells)
                {
                    Console.Write($"{c}\t");
                }
                Console.WriteLine();
            }
        }

        public void ShowDataConnected()
        {
            string tablename = "NetworkUser";
            Console.WriteLine("\nConnected Mode: getting data from table " + tablename);
            SqlDataReader reader = dbExecutor.SelectAllCommandReader(tablename);
            Console.WriteLine("There are some rows in " + tablename + reader.HasRows);
            Console.WriteLine();

            List<string> columnList = new();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columnList.Add(reader.GetName(i));
                Console.Write(reader.GetName(i) + "\t");
            }
            Console.WriteLine();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    //Console.Write(reader[columnList[i]] + "\t");
                    Console.Write(reader[i] + "\t");

                }
                Console.WriteLine();
            }
            reader.Close();

        }

        public int DeleteUserByLogin(string value)
        {
            return dbExecutor.DeleteByColumn(userTable.Name, userTable.ImportantField, value);
        }

        public int AddingUser(string name, string login)
        {
            return dbExecutor.ExecProcedureAdding(name, login);
        }

        public int UpdatingUser(string value, string newValue)     //(string name, string login)
        {
            //return dbExecutor.ExecProcedureUpdate(name, login);
            return dbExecutor.UpdateByColumn(userTable.Name, userTable.ImportantField, value, userTable.Fields[2], newValue);
        }
    }
}

