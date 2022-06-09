using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceGnusmas.Class
{
    class ConnectionDB
    {
        //public static string conn =
        //          "Data Source=192.168.50.154;" +
        //          "Initial Catalog=ServiceSAM;" +
        //          "User Id=admin;" +
        //          "Password=admin;";
        public static string conn =
                  $@"Data Source=5-115P-04\SQLEXPRESS;" +
                  "Initial Catalog=ServiceSAM;" +
                  "User Id=sa;" +
                  "Password=qwe123;";
    }
}
