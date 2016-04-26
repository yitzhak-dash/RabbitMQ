using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Web.Script.Serialization;


namespace ProducerProj
{
    public class Producer
    {
        public void Run()
        {
            var factory = new ConnectionFactory { HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var message = "Hello from another side";

                    var body = Encoding.UTF8.GetBytes(new JavaScriptSerializer().Serialize(new { message = message }));

                    channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                    Console.WriteLine("[*] Sent {0}", message);
                }
            }
            Console.WriteLine("Press [enter] to exit");
            Console.ReadLine();
        }
    }
}
