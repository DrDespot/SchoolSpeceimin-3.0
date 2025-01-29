using OrganismClasses;
using Microsoft.Data.Sqlite;

internal class DAL
{
    //private readonly string _connectionString = @"Data Source=/home/user123/DBExoten/OrganismenDB.db;";
    private readonly string _connectionString = @"Data Source=/C:/Users/marin/Documents/ZUYD/YEAR1/BLOK 2/Software engineering/Huiswerk/OrganismenDB.db";
    public DAL()
    {
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var connection = new SqliteConnection(_connectionString);
    }

    public void addPlant(Plant newPlant)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = @"
            INSERT INTO Organism (id, Name, Type, Origin, ChildClass, HeightInMeters)
            VALUES (@id, @Name, @Type, @Origin, @ChildClass, @HeightInMeters);"
        ;

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@id", newPlant.Id);
        command.Parameters.AddWithValue("@Name", newPlant.Name);
        command.Parameters.AddWithValue("@Type", newPlant.Type);
        command.Parameters.AddWithValue("@Origin", newPlant.Origin);
        command.Parameters.AddWithValue("@ChildClass", newPlant.Type);
        command.Parameters.AddWithValue("@HeightInMeters", newPlant.HeightInMeters);

        command.ExecuteNonQuery();
    }

    public void addAnimal(Animal newAnimal)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = @"
            INSERT INTO Organism (id, Name, Type, Origin, ChildClass, Habitat)
            VALUES (@id, @Name, @Type, @Origin, @ChildClass, @Habitat);"
        ;

        using var command = new SqliteCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@id", newAnimal.Id);
        command.Parameters.AddWithValue("@Name", newAnimal.Name);
        command.Parameters.AddWithValue("@Type", newAnimal.Type);
        command.Parameters.AddWithValue("@Origin", newAnimal.Origin);
        command.Parameters.AddWithValue("@ChildClass", newAnimal.Type);
        command.Parameters.AddWithValue("@Habitat", newAnimal.Habitat);

        command.ExecuteNonQuery();
    }

    public List<Organism> getAllOrganisms()
    {
        List<Organism> organisms = new List<Organism>();
    

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string selectQuery = @"SELECT * FROM Organism;";

        using var command = new SqliteCommand(selectQuery, connection);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            Organism newOrganism = new Organism();
            newOrganism.Id = reader.GetInt32(0);
            newOrganism.Name = reader.GetString(1);

            organisms.Add(newOrganism);
        }
        return organisms;

        command.ExecuteNonQuery();
    }
}