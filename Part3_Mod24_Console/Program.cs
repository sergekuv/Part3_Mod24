using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Part3_Mod24_Lib;

namespace Part3_Mod24_Console
{
    class Program
    {
        public static Manager manager { get; set; } = new();
        //static Manager manager = new();

        static void Main(string[] args)
        {
            manager.Connect();
            manager.ShowDataDisconnected();
            manager.ShowDataConnected();
            string command;
            do
            {
                Console.Write("\nEnter stop, add, update, delete, show: ");
                command = Console.ReadLine();
                switch (command)
                {
                    case "add" or "a":
                        Add();
                        break;
                    case "update" or "u":
                        Update();
                        break;
                    case "delete" or "d":
                        Delete();
                        break;
                    case "show" or "s":
                        manager.ShowDataConnected();
                        break;
                    default:
                        Console.WriteLine("Incorrect command entered" );
                        break;
                }
            } while (command != "stop");

            manager.Disconnect();
            Console.WriteLine("-- end --");
        }

        static void Update()
        {
            Console.Write("\nEnter user's login to change name: ");
            string login = Console.ReadLine();
            Console.Write("\nEnter user's name to change: ");
            string name = Console.ReadLine();

            int affectedUsers = manager.UpdatingUser(login, name);
            Console.WriteLine("Updated " + affectedUsers + " users\n");
        }

        static void Add()
        {
            Console.Write("\nEnter user's name/login to add: ");
            string nameLogin = Console.ReadLine();
            int affectedUsers = manager.AddingUser(nameLogin, nameLogin);
            Console.WriteLine("Added " + affectedUsers + " users\n");
        }

        static void Delete()
        {
            Console.Write("\nEnter user's login to delete: ");
            int affectedUsers = manager.DeleteUserByLogin(Console.ReadLine());
            Console.WriteLine("Deleted " + affectedUsers + " users\n");
        }
    }
    public enum Commands
    {
        stop, add, update, delete, show 
    }
}
