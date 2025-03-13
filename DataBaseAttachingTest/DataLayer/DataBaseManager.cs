using System.Data.SQLite;
//using Microsoft.Data.Sqlite;
using System.IO;
using System.Numerics;
using System.Text;
using System.Xml.Linq;
using DataBaseAttachingTest.Models;

public class DatabaseHelper
{


    // Relative paths to WORKING DIRECTORY [in this case, bin/Debug/net8.0], NOT this C# file.
    // Side Note: Wont Work anymore if this is turned into an .exe
    private static string dbFullPath = @"..\..\..\Files\JunkOrganismSystem.db";

    // Conveys where DB source is. Very important.
    private static string connectionString = $"Data Source={dbFullPath};Version=3;";

    // Note you still have to add the DB via Tools>SQLite/ SQL Server Compact Box.
    public static void InitializeDatabase()
    {
        if (!File.Exists(dbFullPath))
        {
            SQLiteConnection.CreateFile(dbFullPath);

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();


                // Common Cmds: Queries[read], Update[Change row[] of data], Insert [Add new row of data] 

                // Define tables for your data
                string createAnimalsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS animals (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        origin TEXT NOT NULL,
                        description TEXT NOT NULL,
                        latitude TEXT NOT NULL,
                        longitude TEXT NOT NULL,
                        date TEXT NOT NULL DEFAULT (CURRENT_DATE),
                        time TEXT NOT NULL DEFAULT (CURRENT_TIME)

                    );";

                string createPlantsTableQuery = @"
                    CREATE TABLE IF NOT EXISTS plants (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        origin TEXT NOT NULL,
                        description TEXT NOT NULL,
                        latitude TEXT NOT NULL,
                        longitude TEXT NOT NULL,
                        date TEXT NOT NULL DEFAULT (CURRENT_DATE),
                        time TEXT NOT NULL DEFAULT (CURRENT_TIME)
                    );";
                

                // Add the defined tables.
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = createAnimalsTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createPlantsTableQuery;
                    command.ExecuteNonQuery();
                    
                }
            }
        }

    }



    public static void AddSampleAnimals()
    {

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string checkQuery = "SELECT COUNT(*) FROM animals;";
            using (SQLiteCommand checkCommand = new SQLiteCommand(checkQuery, connection))
            {
                long count = (long)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    Console.WriteLine("Animal data already exists. Skipping sample insertion.");
                    return; // Exit the function if data exists
                }
            }

            string[] animalNames = {
            "Edelhert",
            "Hermelijn",
            "Otter",
            };
            string[] animalOrigins= {
            "Native",
            "Foreign",
            "Native",
            };
            string[] animalDescriptions = {
            "Het edelhert (Cervus elaphus) is een evenhoevig zoogdier uit de familie der hertachtigen.",
            "De hermelijn (Mustela erminea) is een klein zoogdier uit de familie van de marterachtigen (Mustelidae).",
            "De otters (Lutrinae) vormen een onderfamilie van in het water levende roofdieren uit de familie van de marterachtigen (Mustelidae)."
            };
                string[] animalLatitudes = {
            "53.2190652",
            "50.997843",
            "51.304945"
            };
            string[] animalLongitudes = {
            "6.5680077",
            "5.445803",
            "3.886283"
            };


            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                for (int i = 0; i < animalNames.Length; i++)
                {
                    //The @'s are placeholders for the real values.
                    command.CommandText =
                        @"INSERT INTO animals (name, origin, description, latitude, longitude)
                    VALUES (@name, @origin, @description, @latitude, @longitude);";

                    // Imporrtant to safely add parameters this way to avoid 
                    // SQL injections.
                    command.Parameters.AddWithValue("@name", animalNames[i]);
                    command.Parameters.AddWithValue("@origin", animalOrigins[i]);
                    command.Parameters.AddWithValue("@description", animalDescriptions[i]);
                    command.Parameters.AddWithValue("@latitude", animalLatitudes[i]);
                    command.Parameters.AddWithValue("@longitude", animalLongitudes[i]);
                   

                    command.ExecuteNonQuery();

                    //Cleaning up parameters for the next iteration of the loop.
                    command.Parameters.Clear();
                }
            }
        }


    }


    public static void AddAnimal(string animalName, string animalOrigin, string animalDescription, string animalLatitude, string animalLongitude)
    {

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                
                //The @'s are placeholders for the real values.
                command.CommandText =
                    @"INSERT INTO animals (name, origin, description, latitude, longitude)
                VALUES (@name, @origin, @description, @latitude, @longitude);";

                // Imporrtant to safely add parameters this way to avoid 
                // SQL injections.
                command.Parameters.AddWithValue("@name", animalName);
                command.Parameters.AddWithValue("@origin", animalOrigin);
                command.Parameters.AddWithValue("@description", animalDescription);
                command.Parameters.AddWithValue("@latitude", animalLatitude);
                command.Parameters.AddWithValue("@longitude", animalLongitude);


                command.ExecuteNonQuery();

                //Cleaning up parameters.
                command.Parameters.Clear();
                
            }
        }


    }


    
    public static void AddSamplePlants()
    {
        // Note: Temp solution. U should check if the value already has been added and then choose to not add it again. 
       

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string checkQuery = "SELECT COUNT(*) FROM plants;";
            using (SQLiteCommand checkCommand = new SQLiteCommand(checkQuery, connection))
            {
                long count = (long)checkCommand.ExecuteScalar();
                if (count > 0)
                {
                    Console.WriteLine("Plant data already exists. Skipping sample insertion.");
                    return; // Exit the function if data exists
                }
            }

            string[] plantNames = {
            "Palmtree",
            "Eikenhoutenkiemplantje",
            "Mimosa Pudica",
            };
            string[] plantOrigins = {
            "Foreign",
            "Native",
            "Foreign",
            };
            string[] plantDescriptions = {
            "De palmenfamilie (Palmae of Arecaceae: beide zijn toegestaan) is de enige familie in de orde Arecales.",
            "Een kiemplant is een item dat kan worden gegroeid in bomen.",
            "Mimosa pudica (also called sensitive plant, sleepy plant,[citation needed] action plant, humble plant, touch-me-not, touch-and-die, or shameplant)[3][2] is a creeping annual or perennial flowering plant of the pea/legume family Fabaceae.."
            };
            string[] plantLatitudes = {
            "53.2190652",
            "50.997843",
            "51.304945"
            };
            string[] plantLongitudes = {
            "6.5680077",
            "5.445803",
            "3.886283"
            };

            using (SQLiteCommand command = new SQLiteCommand(connection))
            {
                for (int i = 0; i < plantNames.Length; i++)
                {
                    //The @'s are placeholders for the real values.
                    command.CommandText =
                        @"INSERT INTO plants (name, origin, description, latitude, longitude)
                    VALUES (@name, @origin, @description, @latitude, @longitude);";

                    // Imporrtant to safely add parameters this way to avoid 
                    // SQL injections.
                    command.Parameters.AddWithValue("@name", plantNames[i]);
                    command.Parameters.AddWithValue("@origin", plantOrigins[i]);
                    command.Parameters.AddWithValue("@description", plantDescriptions[i]);
                    command.Parameters.AddWithValue("@latitude", plantLatitudes[i]);
                    command.Parameters.AddWithValue("@longitude", plantLongitudes[i]);


                    command.ExecuteNonQuery();

                    //Cleaning up parameters for the next iteration of the loop.
                    command.Parameters.Clear();
                }
            }
        }
        
    }
    


    public static void GetAllOrganisms()
    {
        GetAllAnimals();
        GetAllPlants();
        
    }

    public static List<Animal> GetAllAnimals()
    {
        /*
        using (StreamWriter sw = new StreamWriter("test.csv"))
        {
            sw.WriteLine("a,b,c");
        }
        */

        var Animals = new List<Animal>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string selectQuery = @"
            SELECT * FROM animals;";
            using var command = new SQLiteCommand(selectQuery, connection);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int idValue = reader.GetInt32(0);
                string id = idValue.ToString();

                string name = reader.GetString(1);
                string origin = reader.GetString(2);
                string description = reader.GetString(3);
                string latitude = reader.GetString(4);
                string longitude = reader.GetString(5);
                string date = reader.GetString(6);
                string time = reader.GetString(7);

                Animals.Add(new Animal
                (
                    idValue,
                    name,
                    origin,
                    description,
                    latitude,
                    longitude,
                    date,
                    time
                ));
            }


            foreach (var val in Animals)
            {
                Console.WriteLine($"ID: {val.Id}\nName: {val.Name}\nOrigin: {val.Origin}\nDescription: {val.Description}\nLatitude: {val.Latitude}\nLongitude: {val.Longitude}\nDate [YYYY-MM-DD]: {val.Date}\nTime [UTC]: {val.Time}\n");

            }

          


            return Animals;
        }
        
    }


    public static List<Plant> GetAllPlants()
    {
        /*
        using (StreamWriter sw = new StreamWriter("test.csv"))
        {
            sw.WriteLine("a,b,c");
        }
        */

        var Plants = new List<Plant>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string selectQuery = @"
            SELECT * FROM plants;";
            using var command = new SQLiteCommand(selectQuery, connection);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int idValue = reader.GetInt32(0);
                string id = idValue.ToString();

                string name = reader.GetString(1);
                string origin = reader.GetString(2);
                string description = reader.GetString(3);
                string latitude = reader.GetString(4);
                string longitude = reader.GetString(5);
                string date = reader.GetString(6);
                string time = reader.GetString(7);

                Plants.Add(new Plant
                (
                    idValue,
                    name,
                    origin,
                    description,
                    latitude,
                    longitude,
                    date,
                    time
                ));
            }


            foreach (var val in Plants)
            {
                Console.WriteLine($"ID: {val.Id}\nName: {val.Name}\nOrigin: {val.Origin}\nDescription: {val.Description}\nLatitude: {val.Latitude}\nLongitude: {val.Longitude}\nDate [YYYY-MM-DD]: {val.Date}\nTime [UTC]: {val.Time}\n");

            }


            return Plants;

        }

    }

}
