using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;



namespace DataBaseAttachingTest.BusinessLayer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BusinessManager.Initialize();
            
               

            //DatabaseHelper.AddSampleBooksFromCsv(@"..\..\Files\books.csv");
        }
    }
}