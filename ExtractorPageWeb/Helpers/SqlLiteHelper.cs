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
                TipoSeletor TEXT,
                Seletor TEXT,
                Resultado TEXT,
                Data TEXT
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

        public void InsertData(string tipoSeletor, string seletor, string resultado, string data)
        {
            string insertQuery = "INSERT INTO Selectors (TipoSeletor, Seletor, Resultado, Data) VALUES (@TipoSeletor, @Seletor, @Resultado, @Data);";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@TipoSeletor", tipoSeletor);
                    cmd.Parameters.AddWithValue("@Seletor", seletor);
                    cmd.Parameters.AddWithValue("@Resultado", resultado);
                    cmd.Parameters.AddWithValue("@Data", data);
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
                            TipoSeletor = reader["TipoSeletor"].ToString(),
                            Seletor = reader["Seletor"].ToString(),
                            Resultado = reader["Resultado"].ToString(),
                            Data = reader["Data"].ToString()
                        });
                    }
                }
            }

            return selectors;
        }

        public void UpdateSelector(int id, string tipoSeletor, string seletor, string resultado, string data)
        {
            string updateQuery = "UPDATE Selectors SET TipoSeletor = @TipoSeletor, Seletor = @Seletor, Resultado = @Resultado, Data = @Data  WHERE Id = @Id;";

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var cmd = new SQLiteCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Data", data);
                    cmd.Parameters.AddWithValue("@TipoSeletor", tipoSeletor);
                    cmd.Parameters.AddWithValue("@Seletor", seletor);
                    cmd.Parameters.AddWithValue("@Resultado", resultado);
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
        public string TipoSeletor { get; set; }
        public string Seletor { get; set; }
        public string Resultado { get; set; }
        public string Data { get; set; }
    }
}

