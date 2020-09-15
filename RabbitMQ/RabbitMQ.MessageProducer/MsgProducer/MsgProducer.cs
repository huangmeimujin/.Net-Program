using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ.MessageProducer.MessageProducer
{
    public class MsgProducer
    {
        public static void Show()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";//RabbitMQ服务在本地运行
            factory.UserName = "guest";//用户名
            factory.Password = "guest";//密码 
            using (IConnection connection =factory.CreateConnection())
            {
                //创建一个信道
                using (IModel channel = connection.CreateModel())
                {
                    //申明一个队列
                    channel.QueueDeclare(queue: "OnlyProducerMessage", durable: true, exclusive: false, autoDelete: false, arguments: null);

                    //申明一个交换机
                    channel.ExchangeDeclare(exchange: "OnlyProducerMessageExChange", type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);

                    channel.QueueBind(queue: "OnlyProducerMessage", exchange: "OnlyProducerMessageExChange", routingKey: string.Empty, arguments: null);


                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("生产者ProducerDemo已准备就绪~~~");
                    int i = 1;
                    {
                        while (true)
                        {
                            IBasicProperties basicProperties = channel.CreateBasicProperties();
                            basicProperties.Persistent = true;
                            //basicProperties.DeliveryMode = 2;
                            string message = $"消息{i}";
                            byte[] body = Encoding.UTF8.GetBytes(message);
                            channel.BasicPublish(exchange: "OnlyProducerMessageExChange",
                                            routingKey: string.Empty,
                                            basicProperties: basicProperties,
                                            body: body);
                            Console.WriteLine($"消息消息消息消息消息消息消息消息消息消息消息消息消息消息：{message} 已发送~");
                            i++;
                            //Thread.Sleep(10);
                        }
                    }
                }

            }

        }
    }
}
