using System;
using System.Text;
using RabbitMQ.Client;

namespace ProducerWorkQueuesProj
{
    public class Producer
    {
        public void Run(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost"};
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "task_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                    
                    var message = GetMessage(args);
                    var body = Encoding.UTF8.GetBytes(message);
                    var property = channel.CreateBasicProperties();
                    property.Persistent = true;

                    channel.BasicPublish(exchange: "", routingKey: "task_queue", basicProperties: property, body: body);
                    Console.WriteLine("[*] Sent {0}", message);
                }
            }
            Console.WriteLine("Press [enter] to exit");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return (args.Length > 0) ? string.Join(" ", args) : "Hello World!";
        }
    }
}
