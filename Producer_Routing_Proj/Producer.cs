using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;

namespace Producer_Routing_Proj
{
    /// <summary>
    /// We will use a direct exchange instead.
    /// The routing algorithm behind a direct exchange is simple - a message goes to the queues whose binding key exactly matches the routing key of the message.
    /// </summary>
    public class Producer
    {
        public void Run(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("direct_logs", "direct");

                    var severity = GetSeverity(args);
                    var message = GetMessage(args);
                    var body = Encoding.UTF8.GetBytes(message);
                    // routing key is severity
                    channel.BasicPublish(exchange: "direct_logs", routingKey: severity, basicProperties: null, body: body);
                    Console.WriteLine("[*] Sent '{0}':'{1}'", severity, message);
                }
            }
            Console.WriteLine("Press [enter] to exit");
            Console.ReadLine();
        }

        private static string GetSeverity(string[] args)
        {
            return args.Length > 0 ? args[0] : "info";
        }

        private static string GetMessage(string[] args)
        {
            return args.Length > 1
                          ? string.Join(" ", args.Skip(1).ToArray())
                          : "Hello World!";
        }
    }
}
