using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

namespace RabbitMQ.Producer
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Por favor, proporciona el nombre del archivo JSON como argumento.");
                return;
            }

            string jsonFilePath = args[0];

            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("El archivo JSON especificado no existe.");
                return;
            }

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

            // Leer el contenido del archivo JSON
            string jsonContent = File.ReadAllText(jsonFilePath);

            // Deserializar el JSON en una lista de objetos
            var countries = JsonConvert.DeserializeObject<List<Country>>(jsonContent);

            foreach (var country in countries)
            {
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(country));
                channel.BasicPublish("", "demo-queue", null, body);
                Console.WriteLine($"Mensaje enviado: {JsonConvert.SerializeObject(country)}");
            }
        }
    }

    // Definir la clase para el objeto JSON
    public class Country
    {
        public string? CountryName { get; set; }
        public string? Capital { get; set; }
        public int Population { get; set; }
        public string? OfficialLanguage { get; set; }
    }

}
