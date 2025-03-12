using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseAttachingTest.BusinessLayer
{
    class BusinessManager
    {

        public static void Initialize()
        {
            // It yells at me to make an instance to retrieve the info but
            // Not for the other methods? I dunno why. Im tired. -C
            DatabaseHelper dbHelper = new DatabaseHelper(); // Create an instance
            DatabaseHelper.InitializeDatabase();
            DatabaseHelper.AddSampleUsers();
            DatabaseHelper.AddSampleBooks();
            dbHelper.GetAllBooks();  // Call the method on the instance

        }
    }

}