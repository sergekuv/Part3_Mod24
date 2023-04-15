using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3_Mod24_Lib
{
    class ConnectionString
    {
        //public static string MsSqlConnection = @"Server=192.168.1.220;Database=testing;User Id=sa;Password=Qwerty12;TrustServerCertificate=true;";
        public static string MsSqlConnection = @"Server=(localdb)\\mssqllocaldb;Database=testing;Trusted_Connection=True;MultipleActiveResultSets=true";
    }
}
