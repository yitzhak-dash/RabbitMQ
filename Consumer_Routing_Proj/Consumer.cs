using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer_Routing_Proj
{
    public class Consumer
    {
        public void Run(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("direct_logs", "direct");

                // we create a non-durable, exclusive, autodelete queue with a generated name
                var queueName = channel.QueueDeclare().QueueName;

                if (args.Length < 1)
                {
                    Console.Error.WriteLine("Usage: {0} [info] [warning] [error]", Environment.GetCommandLineArgs()[0]);
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                    Environment.ExitCode = 1;
                    return;
                }

                foreach (var severity in args)
                {
                    channel.QueueBind(queue: queueName,
                                      exchange: "direct_logs",
                        // The meaning of a binding key(routingKey) depends on the exchange type. 
                                      routingKey: severity);
                }

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine(" [*] Received '{0}':'{1}'", routingKey, message);
                };
                channel.BasicConsume(queue: queueName, noAck: true, consumer: consumer);

                Console.WriteLine("Press [enter] to exit");
                Console.ReadLine();
            }
        }
    }
}
