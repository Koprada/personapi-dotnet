using RabbitMQ.Client;
using RabbitMQ.Consumer;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using System.Text;
using System.Data.SqlClient;

namespace RabbitMQ.Consumer
{
    static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var consumer = new EventingBasicConsumer(channel);
            
            string connectionString = "{{connection}}";

            consumer.Received += (sender, e) =>
            {

                Console.WriteLine("Recibiendo datos");
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);

                Country country = JsonConvert.DeserializeObject<Country>(message);
                string countryName = " ";
                string capital = " ";
                int population = 0;
                string officialLanguage = " ";

                if (country != null)
                {
                    countryName = country.CountryName;
                    capital = country.Capital;
                    population = country.Population;
                    officialLanguage = country.OfficialLanguage;
                }
                

                using (SqlConnection dbConnection = new SqlConnection(connectionString))
                {
                    try 
                    {
                        dbConnection.Open();
                        string sqlInsertCountry = "INSERT INTO Country (Id, CountryName, Capital, Population, OfficialLanguage) VALUES (@Valor1, @Valor2, @Valor3, @Valor4, @Valor5)";
                        using (SqlCommand command = new SqlCommand(sqlInsertCountry, dbConnection))
                        {
                            command.Parameters.AddWithValue("@Valor1", Guid.NewGuid());
                            command.Parameters.AddWithValue("@Valor2", countryName);
                            command.Parameters.AddWithValue("@Valor3", capital);
                            command.Parameters.AddWithValue("@Valor4", population);
                            command.Parameters.AddWithValue("@Valor5", officialLanguage);
                            int rowsAffected = command.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Registro Country insertado correctamente.");
                            }
                            else
                            {
                                Console.WriteLine("No se pudo insertar el registro Country.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                    finally 
                    {
                        dbConnection.Close();
                    }
                }

            };

            channel.BasicConsume("demo-queue", true, consumer);
            Console.ReadLine();
        }
    }

    public class Country
    {
        public string? CountryName { get; set; }
        public string? Capital { get; set; }
        public int Population { get; set; }
        public string? OfficialLanguage { get; set; }
    }
}
