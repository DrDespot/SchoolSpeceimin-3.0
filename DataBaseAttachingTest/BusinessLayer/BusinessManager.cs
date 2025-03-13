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
                    DatabaseHelper.AddAnimal("Penguin", "Foreign", "Pinguïns of vetganzen zijn een orde van niet-vliegende zeevogels die alleen voorkomen op het zuidelijk halfrond.", "50.86963445", "6.04903023345004");
                    break;
                case 7:
                    Console.WriteLine("ADDING PLANT");
                    //DatabaseHelper.AddAnimal("Penguin", "Foreign", "Pinguïns of vetganzen zijn een orde van niet-vliegende zeevogels die alleen voorkomen op het zuidelijk halfrond.", "50.86963445", "6.04903023345004");
                    break;
                case 8:
                    Console.WriteLine("EXPORTING ORGANISMS AS .csv");
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

    }

}