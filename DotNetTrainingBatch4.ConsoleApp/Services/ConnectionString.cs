using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZLODotNetCore.ConsoleApp.Services
{
    internal static class ConnectionString
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "ZLO\\ZLO",
            InitialCatalog = "DotNetTrainingBatch4",
            UserID = "sa",
            Password = "015427",
            TrustServerCertificate = true

        };

    }
}
