using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsumerWorkQueuesProj
{
    public class Consumer
    {
        public void Run()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, args) =>
                {
                    var body = args.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("[*] Recieve {0}", message);

                    int dots = message.Split('.').Length - 1;
                    Thread.Sleep(dots * 1000);

                    Console.WriteLine(" [x] Done");

                    channel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: "task_queue", noAck: false, consumer: consumer);

                Console.WriteLine("Press [enter] to exit");
                Console.ReadLine();
            }
        }
    }
}
