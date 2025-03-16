using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataBaseAttachingTest.BusinessLayer
{
    class BusinessManager
    {
        public static void Initialize()
        {
            Console.WriteLine("Initializing database..");
            // It yells at me to make an instance to retrieve the info but
            // Not for the other methods? I dunno why. Im tired. -C

            //DatabaseHelper dbHelper = new DatabaseHelper(); // Create an instance
            DatabaseHelper.InitializeDatabase();
            DatabaseHelper.InitializeRealDatabase();
            DatabaseHelper.AddSampleAnimals();
            DatabaseHelper.AddSamplePlants();
            
            //dbHelper.GetAllAnimals();  // Call the method on the instance
            //dbHelper.GetAllPlants();  // Call the method on the instance
            //dbHelper.GetAllOrganisms();
            //DatabaseHelper.GetAllOrganisms();

        }

       
        public static void MainMenuOptions()
        {

            int result = 0;
            bool validInput = false;

            while (!validInput)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out result) && result <= 17)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }

            Console.WriteLine("\n");

     

            switch (result)
            {
                case 1:
                    Console.WriteLine("VIEWING ALL ORGANISMS");
                    DatabaseHelper.GetAllOrganisms();
                    break;
                case 2:
                    Console.WriteLine("VIEWING ALL ANIMALS");
                    DatabaseHelper.GetAllAnimals();
                    break;
                case 3:
                    Console.WriteLine("VIEWING ALL PLANTS");
                    DatabaseHelper.GetAllPlants();
                    break;
                case 4:
                    Console.WriteLine("VIEWING ALL NATIVE ORGANISMS");
                    DatabaseHelper.GetAllNatives();
                    break;
                case 5:
                    Console.WriteLine("VIEWING ALL FOREIGN ORGANISMS");
                    DatabaseHelper.GetAllForeigns();
                    break;
                case 6:
                    Console.WriteLine("VIEWING ALL NATIVE ANIMALS");
                    DatabaseHelper.GetAllAnimalNatives();
                    break;
                case 7:
                    Console.WriteLine("VIEWING ALL NATIVE PLANTS");
                    DatabaseHelper.GetAllPlantNatives();
                    break;
                case 8:
                    Console.WriteLine("VIEWING ALL FOREIGN ANIMALS");
                    DatabaseHelper.GetAllAnimalForeigns();
                    break;
                case 9:
                    Console.WriteLine("VIEWING ALL FOREIGN PLANTS");
                    DatabaseHelper.GetAllPlantForeigns();
                    break;
                case 10:
                    Console.WriteLine("ADDING ANIMAL");
                    UserAddingAnimal();
                    break;
                case 11:
                    Console.WriteLine("ADDING PLANT");
                    UserAddingPlant();
                    break;
                case 12:
                    Console.WriteLine("EXPORTING ANIMALS AS .csv");
                    DatabaseHelper.ExportAnimalsAsCsv();
                    break;
                case 13:
                    Console.WriteLine("EXPORTING PLANTS AS .csv");
                    DatabaseHelper.ExportPlantsAsCsv();
                    break;
                case 14:
                    Console.WriteLine("COPYING ANIMAL DATA FROM JUNK DATABASE TO REAL DATABASE");
                    UserCopyingAnimal();
                    break;
                case 15:
                    Console.WriteLine("COPYING PLANT DATA FROM JUNK DATABASE TO REAL DATABASE");
                    UserCopyingPlant();
                    break;
                case 16:
                    Console.WriteLine("VIEWING ALL ENTRIES IN REAL DATABASE");
                    DatabaseHelper.GetAllRealOrganisms();
                    break;
                case 17:
                    ExitPrgm();
                    break;
                default:
                    Console.WriteLine("How did you get here?!");
                    break;
            }


        }

        static void UserAddingAnimal()
        {
            Console.Write("Animal Name? ");
            string inputName = Console.ReadLine();

            Console.Write("Animal Origin? [Native/Foreign] ");
            string inputOrigin = Console.ReadLine();

            Console.Write("Animal Description? ");
            string inputDescription = Console.ReadLine();

            Console.Write("Animal Latitude? ");
            string inputLatitude = Console.ReadLine();

            Console.Write("Animal Latitude? ");
            string inputLongitude = Console.ReadLine();

            //DatabaseHelper.AddAnimal("Penguin", "Foreign", "Pinguïns of vetganzen zijn een orde van niet-vliegende zeevogels die alleen voorkomen op het zuidelijk halfrond.", "50.86963445", "6.04903023345004");
            DatabaseHelper.AddAnimal(inputName, inputOrigin, inputDescription, inputLatitude, inputLongitude);
        }

        static void UserAddingPlant()
        {
            Console.Write("Plant Name? ");
            string inputName = Console.ReadLine();

            Console.Write("Plant Origin? [Native/Foreign] ");
            string inputOrigin = Console.ReadLine();

            Console.Write("Plant Description? ");
            string inputDescription = Console.ReadLine();

            Console.Write("Plant Latitude? ");
            string inputLatitude = Console.ReadLine();

            Console.Write("Plant Longitude? ");
            string inputLongitude = Console.ReadLine();

            //DatabaseHelper.AddAnimal("Penguin", "Foreign", "Pinguïns of vetganzen zijn een orde van niet-vliegende zeevogels die alleen voorkomen op het zuidelijk halfrond.", "50.86963445", "6.04903023345004");
            DatabaseHelper.AddPlant(inputName, inputOrigin, inputDescription, inputLatitude, inputLongitude);
        }


        static void UserCopyingAnimal()
        {
            int junkID;

            while (true)
            {
                Console.Write("Which Animal ID do you want to copy to the real database? ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out junkID))
                {
                    DatabaseHelper.CopyJunkAnimalToReal(junkID);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

        }

        static void UserCopyingPlant()
        {
            int junkID;

            while (true)
            {
                Console.Write("Which Plant ID do you want to copy to the real database? ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out junkID))
                {
                    DatabaseHelper.CopyJunkPlantToReal(junkID);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

        }



        static void ExitPrgm()
        {
            //Hehe
            Console.WriteLine("\nAre you sure you want to exit this program? Charlie will miss you\n" +
                "1. Yes\n" +
                "2. [or other number] No\n");

            int result = 0;
            string input = Console.ReadLine();
            int.TryParse(input, out result);

            if (int.TryParse(input, out result) && result == 1)
            {
                Console.WriteLine("EXITING PROGRAM");
                Environment.Exit(0);
            }

        }


    }

}