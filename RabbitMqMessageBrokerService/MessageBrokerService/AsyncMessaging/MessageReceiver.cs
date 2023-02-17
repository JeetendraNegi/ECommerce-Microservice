using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessageBrokerService.AsyncMessaging;

public class MessageReceiver : IMessageReceiver
{
    public delegate string MyDelegate();
    public async Task<string> ReceiveMessage(string queueName)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var message = "";
        using (var connection = factory.CreateConnection("UserManagement_Connecton"))
        {

            //create channel
            using (var channel = connection.CreateModel())
            {
                //declare queue
                channel.QueueDeclare(
                    queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                //consume message
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += delegate (object sender, BasicDeliverEventArgs e) { ConsumerReceived(sender, e, ref message); };

                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                Thread.Sleep(3000);
            }
        }
        return message;
    }

    public void ConsumerReceived(object sender, BasicDeliverEventArgs e, ref string message)
    {
        var body = e.Body.ToArray();
        message = Encoding.UTF8.GetString(body);
        
        Console.WriteLine($"Recieved message {message}");
    }
}
