using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsumerProj
{
    public class Consumer
    {
        public void Run()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, args) =>
                    {
                        var body = args.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("[*] Recieve {0}", message);
                    };
                    channel.BasicConsume(queue: "hello", noAck: true, consumer: consumer);

                    Console.WriteLine("Press [enter] to exit");
                    Console.ReadLine();

                }
            }
        }
    }
}
