using RabbitMQ.Client.Events;

namespace MessageWorkerService.AsyncMessaging;

public interface IMessageReceiver
{
    void ConsumerReceived(object sender, BasicDeliverEventArgs e, ref string message);
    Task<string> ReceiveMessage(string queueName);
}
