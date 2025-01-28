using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using OrganismClasses;

namespace OrganismeClasses_3._0
{
    public class LocalDBService
    {
        private readonly SQLiteAsyncConnection _connection;

        // Might need readjusting
        public LocalDBService(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath
                );
        }

        public async Task InitializeDatabase()
        {
            try
            {
                await _connection.CreateTableAsync<LivingEnvironment>();
            }
            catch
            {
                Console.WriteLine("Database Initizilization failed");
            }


        }
    }
}