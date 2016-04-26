using System;
using System.Text;
using RabbitMQ.Client;

namespace Producer_EmitLog_Proj
{
    ///                             <!--PUBLISH/SUBSCRIBE-->
    /// <summary>
    /// Producer never sends any messages directly to a queue.
    /// The producer can only send messages to an exchange.
    /// On one side it(exchange) receives messages from producers and the other side it pushes them to queues.
    /// There are a few exchange types available: direct, topic, headers and fanout.
    /// To view all exchanges use -> $ sudo rabbitmqctl list_exchanges.
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
                    channel.ExchangeDeclare("logs", "fanout");

                    var message = GetMessage(args);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "logs", routingKey: "", basicProperties: null, body: body);
                    Console.WriteLine("[*] Sent {0}", message);
                }
            }
            Console.WriteLine("Press [enter] to exit");
            Console.ReadLine();
        }

        private static string GetMessage(string[] args)
        {
            return args.Length > 0
                ? string.Join(" ", args)
                : "info: Hello World!";
        }
    }
}
