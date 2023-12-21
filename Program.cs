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
        var tableName = "AcademicIDs";
        var connectionString = "Data Source=DM-07-PC\\TEST24;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        connectionString = "Data Source=DM-07-PC\\TEST24;Integrated Security=True;Connect Timeout=30;Encrypt=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        connectionString = "Data Source=DM-07-PC\\TEST24;Integrated Security=True;Connect Timeout=30;Encrypt=False;Multi Subnet Failover=False";
        connectionString = "Data Source=DM-07-PC\\TEST24;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        List<AcademicId> listOfIds = new List<AcademicId>();
        using (StreamReader reader1 = new StreamReader(path))
        {
            while (!reader1.EndOfStream)
            {
                string line = reader1.ReadLine();
                string[] splitedValues = line.Split(';');

                AcademicId academicId = new AcademicId
                {
                    ChipSerialNumber = GetValueOrDefault(splitedValues, 0),
                    Pk1Rsa3072 = GetValueOrDefault(splitedValues, 1),
                    FpSha256Pk1Rsa3072 = GetValueOrDefault(splitedValues, 2),
                    Pk2Rsa3072 = GetValueOrDefault(splitedValues, 3),
                    FpSha256Pk2Rsa3072 = GetValueOrDefault(splitedValues, 4),
                    Pk1Rsa4096 = GetValueOrDefault(splitedValues, 5),
                    FpSha256Pk1Rsa4096 = GetValueOrDefault(splitedValues, 6),
                    Pk1EcdsaSecp256r1 = GetValueOrDefault(splitedValues, 7),
                    FpSha256Pk1EcdsaSecp256r1 = GetValueOrDefault(splitedValues, 8),
                    Pk2EcdsaSecp256r1 = GetValueOrDefault(splitedValues, 9),
                    FpSha256Pk2EcdsaSecp256r1 = GetValueOrDefault(splitedValues, 10),
                    Pk3EcdsaSecp256r1 = GetValueOrDefault(splitedValues, 11),
                    FpSha256Pk3EcdsaSecp256r1 = GetValueOrDefault(splitedValues, 12),
                    Pk1EcdsaSecp384r1 = GetValueOrDefault(splitedValues, 13),
                    FpSha256Pk1EcdsaSecp384r1 = GetValueOrDefault(splitedValues, 14),
                    Pin = GetValueOrDefault(splitedValues, 15),
                    Puk = GetValueOrDefault(splitedValues, 16),
                };

                listOfIds.Add(academicId);
            }
        }

        try
        {
            List<AcademicId> existingIds = new List<AcademicId>();
            List<AcademicId> nonExistingIds = new List<AcademicId>();

            using (SqlConnection connection1 = new SqlConnection(connectionString))
            {
                connection1.Open();


                foreach (var academicId in listOfIds)
                {
                    var chipSerialNumber = academicId.ChipSerialNumber;
                    var query = $"SELECT COUNT(1) FROM {tableName} WHERE ChipSerialNumber = @ChipSerialNumber";

                    using (SqlCommand command = new SqlCommand(query, connection1))
                    {
                        command.Parameters.AddWithValue("@ChipSerialNumber", chipSerialNumber);
                        var count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            existingIds.Add(academicId);
                        }
                        else
                        {
                            nonExistingIds.Add(academicId);
                        }
                    }
                }
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var academicId in nonExistingIds)
                {
                    var sqlCommand = $"INSERT INTO {tableName} " +
                                      "VALUES (@ChipSerialNumber, @Pk1Rsa3072, @FpSha256Pk1Rsa3072, @Pk2Rsa3072, @FpSha256Pk2Rsa3072, " +
                                      "@Pk1Rsa4096, @FpSha256Pk1Rsa4096, @Pk1EcdsaSecp256r1, @FpSha256Pk1EcdsaSecp256r1, @Pk2EcdsaSecp256r1, @FpSha256Pk2EcdsaSecp256r1, " +
                                      "@Pk3EcdsaSecp256r1, @FpSha256Pk3EcdsaSecp256r1, @Pk1EcdsaSecp384r1, @FpSha256Pk1EcdsaSecp384r1, @Pin, @Puk)";

                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
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
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static string GetValueOrDefault(string[] values, int index)
    {
        return index >= 0 && index < values.Length && values[index].Length > 0
            ? values[index]
            : null;
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
