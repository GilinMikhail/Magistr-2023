using System;
using System.Data.SQLite;

class Program
{
    static SQLiteConnection dbConnection;

    static void Main()
    {
        dbConnection = new SQLiteConnection("Data Source=test01.sqlite3;Version=3;");
        dbConnection.Open();

        // Раскомментируйте нужные функции в зависимости от операций, которые вы хотите выполнить
      //CreateTable();
        // DeleteTable();
      //InsertData(10000);
      //CreateIndex();
        // DeleteIndex();
      
      Select();

        dbConnection.Close();
    }

    static string GetRandomName()
    {
        string alphavit = "abcdefghqijklmnoprstuvwxyz";
        Random random = new Random();
        int len = 2 + random.Next(12);
        string name = "";
        for (int i = 0; i < len; i++)
        {
            name += alphavit[random.Next(alphavit.Length)];
        }
        return name;
    }

    static int GetRandomRate(int a = 160, int b = 240)
    {
        Random random = new Random();
        return a + random.Next(b - a + 1);
    }

    static void CreateTable()
    {
        using (SQLiteCommand cmd = new SQLiteCommand(dbConnection))
        {
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS students (id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, rate INTEGER)";
            cmd.ExecuteNonQuery();
        }
    }

    static void DeleteTable()
    {
        using (SQLiteCommand cmd = new SQLiteCommand(dbConnection))
        {
            cmd.CommandText = "DROP TABLE IF EXISTS students";
            cmd.ExecuteNonQuery();
        }
    }

    static void InsertData(int count = 100000)
    {
        using (SQLiteTransaction transaction = dbConnection.BeginTransaction())
        {
            using (SQLiteCommand cmd = new SQLiteCommand(dbConnection))
            {
                cmd.CommandText = "INSERT INTO students (name, rate) VALUES (@name, @rate)";

                for (int i = 0; i < count; i++)
                {
                    cmd.Parameters.AddWithValue("@name", GetRandomName());
                    cmd.Parameters.AddWithValue("@rate", GetRandomRate());
                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();
                }
            }

            transaction.Commit();
        }
    }

    static void CreateIndex()
    {
        using (SQLiteCommand cmd = new SQLiteCommand(dbConnection))
        {
            cmd.CommandText = "CREATE INDEX IF NOT EXISTS index_name ON students (name)";
            cmd.ExecuteNonQuery();
        }
    }

    static void DeleteIndex()
    {
        using (SQLiteCommand cmd = new SQLiteCommand(dbConnection))
        {
            cmd.CommandText = "DROP INDEX IF EXISTS index_name";
            cmd.ExecuteNonQuery();
        }
    }

    static void Select()
    {
        Console.WriteLine("time");
        using (SQLiteCommand cmd = new SQLiteCommand(dbConnection))
        {
            cmd.CommandText = "SELECT id, name, rate FROM students WHERE name = 'py'";
            using (SQLiteDataReader reader = cmd.ExecuteReader())
            {
                int count = 0;
                while (reader.Read())
                {
                    count++;
                }
                Console.WriteLine(count);
            }
        }
    }
}