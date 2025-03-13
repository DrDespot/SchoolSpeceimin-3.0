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

                if (int.TryParse(input, out result) && result <= 9)
                {
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }

            switch (result)
            {
                case 1:
                    Console.WriteLine("VIEWING ALL ORGANISMS");
                    DatabaseHelper.GetAllOrganisms();
                    break;
                case 2:
                    Console.WriteLine("VIEWING ALL ORGANISMS");
                    DatabaseHelper.GetAllAnimals();
                    break;
                case 3:
                    Console.WriteLine("VIEWING ALL PLANTS");
                    DatabaseHelper.GetAllPlants();
                    break;
                case 4:
                    Console.WriteLine("VIEWING ALL NATIVE ORGANISMS");
                    //DatabaseHelper.GetAllNatives();
                    break;
                case 5:
                    Console.WriteLine("VIEWING ALL FOREIGN ORGANISMS");
                    //DatabaseHelper.GetAllForeign();
                    break;
                case 6:
                    Console.WriteLine("ADDING ANIMAL");
                    UserAddingAnimal();
                    break;
                case 7:
                    Console.WriteLine("ADDING PLANT");
                    UserAddingPlant();
                    break;
                case 8:
                    Console.WriteLine("EXPORTING ORGANISMS AS .csv");
                    DatabaseHelper.ExportAsCsv();
                    //DatabaseHelper.AddAnimal("Penguin", "Foreign", "Pinguïns of vetganzen zijn een orde van niet-vliegende zeevogels die alleen voorkomen op het zuidelijk halfrond.", "50.86963445", "6.04903023345004");
                    break;
                case 9:
                    Console.WriteLine("EXITING PROGRAM");
                    //DatabaseHelper.AddAnimal("Penguin", "Foreign", "Pinguïns of vetganzen zijn een orde van niet-vliegende zeevogels die alleen voorkomen op het zuidelijk halfrond.", "50.86963445", "6.04903023345004");
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

            Console.Write("Plant Latitude? ");
            string inputLongitude = Console.ReadLine();

            //DatabaseHelper.AddAnimal("Penguin", "Foreign", "Pinguïns of vetganzen zijn een orde van niet-vliegende zeevogels die alleen voorkomen op het zuidelijk halfrond.", "50.86963445", "6.04903023345004");
            DatabaseHelper.AddPlant(inputName, inputOrigin, inputDescription, inputLatitude, inputLongitude);
        }

    }

}