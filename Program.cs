using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using CsvHelper;

class Program
{
    static void Main()
    {
        var path = "C:\\Users\\kofinasa\\Desktop\\Academic_ID_650437_20231213114227.csv";

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Read();
        csv.ReadHeader();


        var connectionString = "Data Source=DM-07-PC\\TEST24;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        connectionString = "Data Source=DM-07-PC\\TEST24;Integrated Security=True;Connect Timeout=30;Encrypt=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        connectionString = "Data Source=DM-07-PC\\TEST24;Integrated Security=True;Connect Timeout=30;Encrypt=False;Multi Subnet Failover=False";
        connectionString = "Data Source=DM-07-PC\\TEST24;Integrated Security=True;Connect Timeout=30;Encrypt=False;";



        try
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();

            var tableName = "AcademicIDs";
            var sqlCommand = $"INSERT INTO {tableName} VALUES (@ChipSerialNumber, @Pk1Rsa3072, @FpSha256Pk1Rsa3072, @Pk2Rsa3072, @FpSha256Pk2Rsa3072, " +
                              "@Pk1Rsa4096, @FpSha256Pk1Rsa4096, @Pk1EcdsaSecp256r1, @FpSha256Pk1EcdsaSecp256r1, @Pk2EcdsaSecp256r1, @FpSha256Pk2EcdsaSecp256r1, " +
                              "@Pk3EcdsaSecp256r1, @FpSha256Pk3EcdsaSecp256r1, @Pk1EcdsaSecp384r1, @FpSha256Pk1EcdsaSecp384r1, @Pin, @Puk)";

            var listOfAcademicIds = new List<AcademicId>();

            while (csv.Read())
            {
                var firstline = csv.GetField<string>(0);                
                var linesSplited = firstline.Split(';');

                foreach (var line in linesSplited)
                {
                    Console.WriteLine($"Line: {line}");
                    // Process each line as needed
                }

                Console.WriteLine();  // Separate lines for clarity
                


                var academicId = new AcademicId
                {
                    ChipSerialNumber = csv.GetField<string>(0),
                    Pk1Rsa3072 = csv.GetField<string>(1),
                    FpSha256Pk1Rsa3072 = csv.GetField<string>(2),
                    Pk2Rsa3072 = csv.GetField<string>(3),
                    FpSha256Pk2Rsa3072 = csv.GetField<string>(4),
                    Pk1Rsa4096 = csv.GetField<string>(5),
                    FpSha256Pk1Rsa4096 = csv.GetField<string>(6),
                    Pk1EcdsaSecp256r1 = csv.GetField<string>(7),
                    FpSha256Pk1EcdsaSecp256r1 = csv.GetField<string>(8),
                    Pk2EcdsaSecp256r1 = csv.GetField<string>(9),
                    FpSha256Pk2EcdsaSecp256r1 = csv.GetField<string>(10),
                    Pk3EcdsaSecp256r1 = csv.GetField<string>(11),
                    FpSha256Pk3EcdsaSecp256r1 = csv.GetField<string>(12),
                    Pk1EcdsaSecp384r1 = csv.GetField<string>(13),
                    FpSha256Pk1EcdsaSecp384r1 = csv.GetField<string>(14),
                    Pin = csv.GetField<string>(15),
                    Puk = csv.GetField<string>(16)
                };

                using var command = new SqlCommand(sqlCommand, connection);
                command.Parameters.AddWithValue("@ChipSerialNumber", academicId.ChipSerialNumber);
                command.Parameters.AddWithValue("@Pk1Rsa3072", academicId.Pk1Rsa3072);
                command.Parameters.AddWithValue("@FpSha256Pk1Rsa3072", academicId.FpSha256Pk1Rsa3072);
                command.Parameters.AddWithValue("@Pk2Rsa3072", academicId.Pk2Rsa3072);
                command.Parameters.AddWithValue("@FpSha256Pk2Rsa3072", academicId.FpSha256Pk2Rsa3072);
                command.Parameters.AddWithValue("@Pk1Rsa4096", academicId.Pk1Rsa4096);
                command.Parameters.AddWithValue("@FpSha256Pk1Rsa4096", academicId.FpSha256Pk1Rsa4096);
                command.Parameters.AddWithValue("@Pk1EcdsaSecp256r1", academicId.Pk1EcdsaSecp256r1);
                command.Parameters.AddWithValue("@FpSha256Pk1EcdsaSecp256r1", academicId.FpSha256Pk1EcdsaSecp256r1);
                command.Parameters.AddWithValue("@Pk2EcdsaSecp256r1", academicId.Pk2EcdsaSecp256r1);
                command.Parameters.AddWithValue("@FpSha256Pk2EcdsaSecp256r1", academicId.FpSha256Pk2EcdsaSecp256r1);
                command.Parameters.AddWithValue("@Pk3EcdsaSecp256r1", academicId.Pk3EcdsaSecp256r1);
                command.Parameters.AddWithValue("@FpSha256Pk3EcdsaSecp256r1", academicId.FpSha256Pk3EcdsaSecp256r1);
                command.Parameters.AddWithValue("@Pk1EcdsaSecp384r1", academicId.Pk1EcdsaSecp384r1);
                command.Parameters.AddWithValue("@FpSha256Pk1EcdsaSecp384r1", academicId.FpSha256Pk1EcdsaSecp384r1);
                command.Parameters.AddWithValue("@Pin", academicId.Pin);
                command.Parameters.AddWithValue("@Puk", academicId.Puk);

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public class AcademicId
    {
        public string ChipSerialNumber { get; set; } = null!;
        public string? Pk1Rsa3072 { get; set; }
        public string? FpSha256Pk1Rsa3072 { get; set; }
        public string? Pk2Rsa3072 { get; set; }
        public string? FpSha256Pk2Rsa3072 { get; set; }
        public string? Pk1Rsa4096 { get; set; }
        public string? FpSha256Pk1Rsa4096 { get; set; }
        public string? Pk1EcdsaSecp256r1 { get; set; }
        public string? FpSha256Pk1EcdsaSecp256r1 { get; set; }
        public string? Pk2EcdsaSecp256r1 { get; set; }
        public string? FpSha256Pk2EcdsaSecp256r1 { get; set; }
        public string? Pk3EcdsaSecp256r1 { get; set; }
        public string? FpSha256Pk3EcdsaSecp256r1 { get; set; }
        public string? Pk1EcdsaSecp384r1 { get; set; }
        public string? FpSha256Pk1EcdsaSecp384r1 { get; set; }
        public string? Pin { get; set; }
        public string? Puk { get; set; }
    }
}
