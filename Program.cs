using CsvHelper;
using System.Globalization;

class Program
{
    static void Main()
    {
        var path = "C:\\Users\\kofinasa\\Desktop\\Academic_ID_650437_20231213114227.csv";
        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        // read CSV file
        var records = csv.GetRecords<dynamic>();

        // Connect to your SQL Server database (modify connection string accordingly)
        var connectionString = "Data Source=DM-07-PC\\TEST24;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        // Create a SQL command to insert data into the table (modify table name and column names accordingly)
        var tableName = "AcademicIDs";
        var sqlCommand = $"INSERT INTO {tableName} " +
            $"(Column1, " +
            $"Column2, " +
            $"Column3) " +
            $"VALUES " +
            $"(@Column1," +
            $"@Column2, " +
            $"@Column3)";

        foreach (var record in records)
        {
            // Create a SQL command with parameters
            using var command = new SqlCommand(sqlCommand, connection);
            command.Parameters.AddWithValue("@Column1", record.Column1); // Replace with the actual column name in your CSV
            command.Parameters.AddWithValue("@Column2", record.Column2); // Replace with the actual column name in your CSV
            command.Parameters.AddWithValue("@Column3", record.Column3); // Replace with the actual column name in your CSV

            // Execute the SQL command to insert the record
            command.ExecuteNonQuery();
        }
    }
}
