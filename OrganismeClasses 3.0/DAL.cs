using OrganismClasses;
using Microsoft.Data.Sqlite;

internal class DAL
{
    private readonly string _connectionString = @"Data Source=/home/user123/DBExoten/OrganismenDB.db;";
    //private readonly string _connectionString = @"Data Source=/C:/Users/marin/Documents/ZUYD/YEAR1/BLOK 2/Software engineering/Huiswerk/OrganismenDB.db";
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
}