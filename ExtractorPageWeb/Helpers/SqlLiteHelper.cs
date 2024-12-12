using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Controls.Primitives;

namespace ExtractorPageWeb.Helpers
{
    public class SQLiteHelper
    {
        private readonly string _connectionString;

        public SQLiteHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateTable()
        {
            string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Selectors (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                TypeSelector TEXT,
                SelectorName TEXT,
                Url TEXT,
                Result TEXT,
                Date TEXT
            );"
            ;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void InsertData(string tipoSeletor, string seletor, string resultado, string data, string url)
        {
            string insertQuery = "INSERT INTO Selectors (TypeSelector, SelectorName, Url, Result, Date) VALUES (@TypeSelector, @SelectorName, @Url, @Result, @Date);";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@TypeSelector", tipoSeletor);
                    cmd.Parameters.AddWithValue("@SelectorName", seletor);
                    cmd.Parameters.AddWithValue("@Result", resultado);
                    cmd.Parameters.AddWithValue("@Url", url);
                    cmd.Parameters.AddWithValue("@Date", data);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Selector> GetAllSelectors()
        {
            string selectQuery = "SELECT * FROM Selectors;";
            var selectors = new List<Selector>();

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(selectQuery, connection))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        selectors.Add(new Selector
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            TypeSelector = reader["TypeSelector"].ToString(),
                            SelectorName = reader["SelectorName"].ToString(),
                            Url = reader["Url"].ToString(),
                            Result = reader["Result"].ToString(),
                            Date = reader["Date"].ToString()
                        });
                    }
                }
            }

            return selectors;
        }

        public void UpdateSelector(int id, string tipoSeletor, string seletor, string resultado, string data, string url)
        {
            string updateQuery = "UPDATE Selectors SET TypeSelector = @TypeSelector, SelectorName = @SelectorName, Url = @Url, Result = @Result, Date = @Date  WHERE Id = @Id;";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Date", data);
                    cmd.Parameters.AddWithValue("@TypeSelector", tipoSeletor);
                    cmd.Parameters.AddWithValue("@Url", url);
                    cmd.Parameters.AddWithValue("@SelectorName", seletor);
                    cmd.Parameters.AddWithValue("@Result", resultado);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSelector(int id)
        {
            string deleteQuery = "DELETE FROM Selectors WHERE Id = @Id;";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
    public class Selector
    {
        public int Id { get; set; }
        public string TypeSelector { get; set; }
        public string SelectorName { get; set; }
        public string Url { get; set; }
        public string Result { get; set; }
        public string Date { get; set; }
    }
}

