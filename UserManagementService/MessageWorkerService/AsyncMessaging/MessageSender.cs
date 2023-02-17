using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MessageWorkerService.AsyncMessaging;

public class MessageSender<T> : IMessageSender<T> where T : class
{
    public async Task<bool> SendMessage(T message, string queueName)
    {
        var success = true;
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using (var connection = factory.CreateConnection("UserManagement_Connecton"))
        {
            //Create Channel
            using (var channel = connection.CreateModel())
            {
                //Declare Queue
                channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                //publish Message
                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                try
                {
                    channel.BasicPublish(exchange: "",
                        routingKey: queueName,
                        basicProperties: null,
                        body: body);
                }catch
                {
                    success = false;
                }

                return success;
                Console.WriteLine($"Sent message : {JsonSerializer.Serialize(message)}");
            }
        }
    }
}
