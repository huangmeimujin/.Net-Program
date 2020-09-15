using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.MessageConsumer.MsgConsumer
{
    public class MsgConsumer
    {
        public static void Show()
        {
            var factory = new ConnectionFactory();
            factory.HostName = "localhost";//RabbitMQ服务在本地运行
            factory.UserName = "guest";//用户名
            factory.Password = "guest";//密码 
            using (var connect=factory.CreateConnection())
            {
                using (var channel=connect.CreateModel())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    try
                    {
                        //申明一个队列
                        channel.QueueDeclare(queue: "OnlyProducerMessage", durable: true, exclusive: false, autoDelete: false, arguments: null);
                        //申明一个交换机
                        channel.ExchangeDeclare(exchange: "OnlyProducerMessageExChange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                        channel.QueueBind(queue: "OnlyProducerMessage", exchange: "OnlyProducerMessageExChange", routingKey: string.Empty, arguments: null);


                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (sender, e) => {
                            var body = e.Body;
                            var message = Encoding.UTF8.GetString(body.ToArray());
                            Console.WriteLine($"消费者01 接受消息: {message}");
                        };
                        channel.BasicConsume(queue: "OnlyProducerMessage",
                                            autoAck: true,
                                            consumer: consumer);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message);
                    }
                   

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
