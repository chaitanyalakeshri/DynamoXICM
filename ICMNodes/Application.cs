using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSClass;

namespace ICMNodes
{
    public class Application
    {
        public static WSDatabase OpenDatabase(string databasePath)
        {
            if (databasePath == null)
            {
                throw new ArgumentNullException(nameof(databasePath), "Database cannot be null.");
            }
            if (string.IsNullOrEmpty(databasePath))
            {
                throw new ArgumentException("Database path cannot be null or empty.", nameof(databasePath));
            }
            WSDatabase response = (WSDatabase)DynamoXICM.ExecuteFunction.Execute("open_database", new string[] { databasePath });
            if (response == null)
            {
                throw new InvalidOperationException("Failed to deserialize the response to WSDatabase.");
            }
            return response;
        }
        public static WSDatabase CreateDatabase(string databasePath)
        {
            if (databasePath == null)
            {
                throw new ArgumentNullException(nameof(databasePath), "Database cannot be null.");
            }
            WSDatabase response = (WSDatabase)DynamoXICM.ExecuteFunction.Execute("create_database", new string[] { databasePath });
            return response;
        }
    }
}
