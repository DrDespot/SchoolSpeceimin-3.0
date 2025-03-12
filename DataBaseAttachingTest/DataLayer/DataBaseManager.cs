using System.Data.SQLite;
//using Microsoft.Data.Sqlite;
using System.IO;
using System.Text;
using DataBaseAttachingTest.Models;  

public class DatabaseHelper
{
    // Relative paths to WORKING DIRECTORY [in this case, bin/Debug/net8.0], NOT this C# file.
    // Side Note: Wont Work anymore if this is turned into an .exe
    private static string dbFullPath = @"..\..\..\Files\LibraryManagementSystem.db";

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
                string createBooksTableQuery = @"
                    CREATE TABLE IF NOT EXISTS books (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        title TEXT NOT NULL,
                        author TEXT NOT NULL,
                        genre TEXT NOT NULL
                    );";

                string createUsersTableQuery = @"
                    CREATE TABLE IF NOT EXISTS users (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL,
                        age INTEGER NOT NULL,
                        email TEXT UNIQUE NOT NULL,
                        password TEXT NOT NULL,
                        user_type TEXT NOT NULL
                    );";

                // Add the defined tables.
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = createBooksTableQuery;
                    command.ExecuteNonQuery();

                    command.CommandText = createUsersTableQuery;
                    command.ExecuteNonQuery();
                }
            }
        }

    }

    public static void AddSampleUsers()
    {
        // Note: Temp solution. U should check if the value already has been added and then choose to not add it again. 
        if (!File.Exists(dbFullPath))
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string[] userNames = {
            "Alan Turing",
            "Linus Torvalds",
            "Steve Jobs",
            "Edsger Dijkstra",
            "Bill Gates"
            };
                int[] userAges = {
            41,
            55,
            56,
            72,
            69
            };
                string[] userEmails = {
            "alan.turing@example.com",
            "linus.torvalds@example.com",
            "steve.jobs@example.com",
            "edsger.dijkstra@example.com",
            "bill.gates@example.com"
            };
                string[] userPasswords = {
            "password1",
            "password2",
            "password3",
            "password4",
            "password5"
            };
                string[] userTypes = {
            "RegularUser",
            "PremiumUser",
            "RegularUser",
            "RegularUser",
            "PremiumUser"
            };

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    for (int i = 0; i < userNames.Length; i++)
                    {
                        //The @'s are placeholders for the real values.
                        command.CommandText =
                            @"INSERT INTO users (name, age, email, password, user_type)
                        VALUES (@name, @age, @email, @password, @user_type);";

                        // Imporrtant to safely add parameters this way to avoid 
                        // SQL injections.
                        command.Parameters.AddWithValue("@name", userNames[i]);
                        command.Parameters.AddWithValue("@email", userEmails[i]);
                        command.Parameters.AddWithValue("@age", userAges[i]);
                        command.Parameters.AddWithValue("@password", userPasswords[i]);
                        command.Parameters.AddWithValue("@user_type", userTypes[i]);
                        command.ExecuteNonQuery();

                        //Cleaning up parameters for the next iteration of the loop.
                        command.Parameters.Clear();
                    }
                }
            }
        }
    }
            

    public static void AddSampleBooks()
    {
        if (!File.Exists(dbFullPath))
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string[] bookTitles = {
            "All My Sons",
            "Oliver Twist",
            "Das Parfum",
            };
                string[] bookAuthors = {
            "Arthur Miller",
            "Charles Dickens",
            "Patrick Süskind"
            };
                string[] bookGenres = {
            "Tragedy",
            "Roman",
            "Horror"
            };

                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    for (int i = 0; i < bookTitles.Length; i++)
                    {
                        //The @'s are placeholders for the real values.
                        command.CommandText =
                            @"INSERT INTO books (title, author, genre)
                        VALUES (@title, @author, @genre);";

                        // Imporrtant to safely add parameters this way to avoid 
                        // SQL injections.
                        command.Parameters.AddWithValue("@title", bookTitles[i]);
                        command.Parameters.AddWithValue("@author", bookAuthors[i]);
                        command.Parameters.AddWithValue("@genre", bookGenres[i]);

                        command.ExecuteNonQuery();

                        //Cleaning up parameters for the next iteration of the loop.
                        command.Parameters.Clear();
                    }
                }
            }
        }
           
    }


    public List<Book> GetAllBooks()
    {
        using (StreamWriter sw = new StreamWriter("test.csv"))
        {
            sw.WriteLine("a,b,c");
        }

        var Books = new List<Book>();

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string selectQuery = @"
            SELECT * FROM books;";
            using var command = new SQLiteCommand(selectQuery, connection);


            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                int idValue = reader.GetInt32(0);
                string id = idValue.ToString();

                string title = reader.GetString(1);
                string author = reader.GetString(2);
                string genre = reader.GetString(3);

                Books.Add(new Book
                (
                    idValue,
                    title,
                    author,
                    genre
                ));
            }


            foreach (var val in Books)
            {
                Console.WriteLine($"ID: {val.Id},\tTitle: {val.Title},\tAuthor: {val.Author},\tGenre: {val.Genre}");

            }


            return Books;
        }
        
    }
}
