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
            SplashScreen();
            while (true)
            {
                MainMenu();
                PressAnyKey();
            }


                //DatabaseHelper.AddSampleBooksFromCsv(@"..\..\Files\books.csv");
            }

        public static void PressAnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();  // Waits for a key press
            Console.WriteLine("\n");

        }

        static void SplashScreen()
        {
            Console.WriteLine("┌───────────────────────────────────────┐\r\n│WELCOME TO THE ANIMATES ORGANISM EDITOR│\r\n└───────────────────────────────────────┘");
        }
        static void MainMenu()
        {
            MainMenuScreen();
            BusinessManager.MainMenuOptions();
        }

        static void MainMenuScreen()
        {
            Console.WriteLine("What would you like to do?" +
                "\n1. View all Organisms" +
                "\n2. View all Animals" +
                "\n3. View all Plants" +
                "\n4. View all Native Organisms" +
                "\n5. View all Foreign Organisms" +
                "\n6. Add Animal" +
                "\n7. Add Plant" +
                "\n8. Export all Organisms as .csv" +
                "\n9. Exit program");
        }

    }
}
