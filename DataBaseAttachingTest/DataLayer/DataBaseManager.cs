using System.Data.SQLite;
//using Microsoft.Data.Sqlite;
using System.IO;
using System.Numerics;
using System.Text;
using System.Xml.Linq;
using DataBaseAttachingTest.Models;

public class DatabaseHelper
{
    // Relative paths to     NOT this C# file.
    // Yes I know calling it full path sounds like its an absolute path.
    // Side Note: Wont Work anymore if this is turned into an .exe
    private static string dbFullPath = @$"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Files{Path.DirectorySeparatorChar}JunkOrganismSystem.db";

    private static string RealdbFullPath = @$"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}Files{Path.DirectorySeparatorChar}RealOrganismSystem.db";

    // Conveys where DB source is. Very important.
    private static string connectionString = $"Data Source={dbFullPath};Version=3;";
    private static string connectionStringReal = $"Data Source={RealdbFullPath};Version=3;";

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


    public static void InitializeRealDatabase()
    {
        if (!File.Exists(RealdbFullPath))
        {
            SQLiteConnection.CreateFile(RealdbFullPath);

            using (SQLiteConnection connection = new SQLiteConnection(connectionStringReal))
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
            "Vliegende flapdrol",
            "Kamhagedis"
            };
            string[] animalOrigins= {
            "Native",
            "Foreign",
            "Native",
            "Foreign",
            "Native"
            };
            string[] animalDescriptions = {
            "Het edelhert (Cervus elaphus) is een evenhoevig zoogdier uit de familie der hertachtigen.",
            "De hermelijn (Mustela erminea) is een klein zoogdier uit de familie van de marterachtigen (Mustelidae).",
            "De otters (Lutrinae) vormen een onderfamilie van in het water levende roofdieren uit de familie van de marterachtigen (Mustelidae).",
            "De vliegende flapdrol[1] (Draco) zijn een geslacht van hagedissen die behoren tot de agamen (Agamidae). ",
            "Hagedis met zieke hanekam"
            };
                string[] animalLatitudes = {
            "53.2190652",
            "50.997843",
            "51.304945",
            "53.342908",
            "52.089428"
            };
            string[] animalLongitudes = {
            "6.5680077",
            "5.445803",
            "3.886283",
            "3.038247",
            "3.4928466"
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

    public static void AddPlant(string plantName, string plantOrigin, string plantDescription, string plantLatitude, string plantLongitude)
    {

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            using (SQLiteCommand command = new SQLiteCommand(connection))
            {

                //The @'s are placeholders for the real values.
                command.CommandText =
                    @"INSERT INTO plants (name, origin, description, latitude, longitude)
                VALUES (@name, @origin, @description, @latitude, @longitude);";

                // Imporrtant to safely add parameters this way to avoid 
                // SQL injections.
                command.Parameters.AddWithValue("@name", plantName);
                command.Parameters.AddWithValue("@origin", plantOrigin);
                command.Parameters.AddWithValue("@description", plantDescription);
                command.Parameters.AddWithValue("@latitude", plantLatitude);
                command.Parameters.AddWithValue("@longitude", plantLongitude);


                command.ExecuteNonQuery();

                //Cleaning up parameters.
                command.Parameters.Clear();

            }
        }


    }


    public static void GetAllOrganisms()
    {
        // Get the absolute path from the relative path
        string absolutePath = Path.GetFullPath(dbFullPath);

        // Print the absolute path
        Console.WriteLine("Absolute path: " + absolutePath);

        GetAllAnimals();
        GetAllPlants();
        
    }

    public static List<Animal> GetAllAnimals()
    {

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

    public static void GetAllNatives()
    {
        GetAllAnimalNatives();
        GetAllPlantNatives();

    }

    public static void GetAllForeigns()
    {
        GetAllAnimalForeigns();
        GetAllPlantForeigns();
    }



    public static List<Animal> GetAllAnimalNatives()
    {

        var Animals = new List<Animal>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string selectQuery = @"
            SELECT * FROM animals
            WHERE origin LIKE 'Native';";
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



    public static List<Plant> GetAllPlantNatives()
    {

        var Plants = new List<Plant>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string selectQuery = @"
            SELECT * FROM plants
            WHERE origin LIKE 'Native';";
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

    public static List<Animal> GetAllAnimalForeigns()
    {

        var Animals = new List<Animal>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string selectQuery = @"
            SELECT * FROM animals
            WHERE origin LIKE 'Foreign';";
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



    public static List<Plant> GetAllPlantForeigns()
    {

        var Plants = new List<Plant>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string selectQuery = @"
            SELECT * FROM plants
            WHERE origin LIKE 'Foreign';";
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

    //ONLY ADDS CSV
    public static void ExportAnimalsAsCsv()
    {
        string csvPath = @"..\..\..\Files\Animals.csv";


        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            using (SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM animals", connection))
            using (SQLiteDataReader reader = sqlCmd.ExecuteReader())


            using (StreamWriter sw = new StreamWriter(csvPath))
            {
                object[] output = new object[reader.FieldCount];

                // Write headers
                for (int i = 0; i < reader.FieldCount; i++)
                    output[i] = reader.GetName(i);

                sw.WriteLine(string.Join(",", output));

                // Write data rows
                while (reader.Read())
                {
                    reader.GetValues(output);
                    sw.WriteLine(string.Join(",", output));
                }
            }
        }
        Console.WriteLine("Succesfully exported Animals.csv!");
    }

    public static void ExportPlantsAsCsv()
    {
        string csvPath = @"..\..\..\Files\Plants.csv";


        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            using (SQLiteCommand sqlCmd = new SQLiteCommand("SELECT * FROM plants", connection))
            using (SQLiteDataReader reader = sqlCmd.ExecuteReader())


            using (StreamWriter sw = new StreamWriter(csvPath))
            {
                object[] output = new object[reader.FieldCount];

                // Write headers
                for (int i = 0; i < reader.FieldCount; i++)
                    output[i] = reader.GetName(i);

                sw.WriteLine(string.Join(",", output));

                // Write data rows
                while (reader.Read())
                {
                    reader.GetValues(output);
                    sw.WriteLine(string.Join(",", output));
                }
            }
        }
        Console.WriteLine("Succesfully exported Plants.csv!");
    }


    public static void CopyJunkAnimalToReal(int junkID)
    {

        string sourceDbPath = connectionString;
        string destinationDbPath = connectionStringReal;

        using (SQLiteConnection sourceConnection = new SQLiteConnection(sourceDbPath))
        using (SQLiteConnection destinationConnection = new SQLiteConnection(destinationDbPath))
        {
            sourceConnection.Open();
            destinationConnection.Open();

            string selectQuery = "SELECT * FROM animals WHERE id = @id;";

            using (SQLiteCommand selectCmd = new SQLiteCommand(selectQuery, sourceConnection))
            {
                selectCmd.Parameters.AddWithValue("@id", junkID);

                using (SQLiteDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        string origin = reader.GetString(reader.GetOrdinal("origin"));
                        string description = reader.GetString(reader.GetOrdinal("description"));
                        string latitude = reader.GetString(reader.GetOrdinal("latitude"));
                        string longitude = reader.GetString(reader.GetOrdinal("longitude"));
                        string date = reader.GetString(reader.GetOrdinal("date"));
                        string time = reader.GetString(reader.GetOrdinal("time"));

                        string insertQuery = @"
                        INSERT INTO animals (name, origin, description, latitude, longitude, date, time) 
                        VALUES (@name, @origin, @description, @latitude, @longitude, @date, @time)";

                        using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, destinationConnection))
                        {
                            insertCmd.Parameters.AddWithValue("@name", name);
                            insertCmd.Parameters.AddWithValue("@origin", origin);
                            insertCmd.Parameters.AddWithValue("@description", description);
                            insertCmd.Parameters.AddWithValue("@latitude", latitude);
                            insertCmd.Parameters.AddWithValue("@longitude", longitude);
                            insertCmd.Parameters.AddWithValue("@date", date);
                            insertCmd.Parameters.AddWithValue("@time", time);

                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        Console.WriteLine("Copied Animal from junk DB to real DB.");
    }

    public static void CopyJunkPlantToReal(int junkID)
    {
        string sourceDbPath = connectionString;
        string destinationDbPath = connectionStringReal;

        using (SQLiteConnection sourceConnection = new SQLiteConnection(sourceDbPath))
        using (SQLiteConnection destinationConnection = new SQLiteConnection(destinationDbPath))
        {
            sourceConnection.Open();
            destinationConnection.Open();

            string selectQuery = "SELECT * FROM plants WHERE id = @id;";

            using (SQLiteCommand selectCmd = new SQLiteCommand(selectQuery, sourceConnection))
            {
                // Add parameter for selecting by ID
                selectCmd.Parameters.AddWithValue("@id", junkID);

                using (SQLiteDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        string origin = reader.GetString(reader.GetOrdinal("origin"));
                        string description = reader.GetString(reader.GetOrdinal("description"));
                        string latitude = reader.GetString(reader.GetOrdinal("latitude"));
                        string longitude = reader.GetString(reader.GetOrdinal("longitude"));
                        string date = reader.GetString(reader.GetOrdinal("date"));
                        string time = reader.GetString(reader.GetOrdinal("time"));

                        string insertQuery = @"
                        INSERT INTO plants (name, origin, description, latitude, longitude, date, time) 
                        VALUES (@name, @origin, @description, @latitude, @longitude, @date, @time)";

                        using (SQLiteCommand insertCmd = new SQLiteCommand(insertQuery, destinationConnection))
                        {
                            insertCmd.Parameters.AddWithValue("@name", name);
                            insertCmd.Parameters.AddWithValue("@origin", origin);
                            insertCmd.Parameters.AddWithValue("@description", description);
                            insertCmd.Parameters.AddWithValue("@latitude", latitude);
                            insertCmd.Parameters.AddWithValue("@longitude", longitude);
                            insertCmd.Parameters.AddWithValue("@date", date);
                            insertCmd.Parameters.AddWithValue("@time", time);

                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        Console.WriteLine("Copied plant from junk DB to real DB.");
    }


    public static void GetAllRealOrganisms()
    {
        GetAllRealAnimals();
        GetAllRealPlants();

    }

    public static List<Animal> GetAllRealAnimals()
    {

        var Animals = new List<Animal>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionStringReal))
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


    public static List<Plant> GetAllRealPlants()
    {
        /*
        using (StreamWriter sw = new StreamWriter("test.csv"))
        {
            sw.WriteLine("a,b,c");
        }
        */

        var Plants = new List<Plant>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionStringReal))
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
