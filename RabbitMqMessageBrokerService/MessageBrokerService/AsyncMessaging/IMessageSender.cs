
namespace MessageBrokerService.AsyncMessaging;

public interface IMessageSender<T> where T : class
{
    Task<bool> SendMessage(T message, string queueName);
}
