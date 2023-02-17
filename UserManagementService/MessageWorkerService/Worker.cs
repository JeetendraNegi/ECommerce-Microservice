using MessageWorkerService.AsyncMessaging;
using System.Net.Http.Json;

namespace MessageWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMessageReceiver _messageService;
        private readonly IConfiguration _configuration;
        private static HttpClient client = new HttpClient();
        public Worker(ILogger<Worker> logger, 
            IMessageReceiver messageService, 
            IConfiguration configuration)
        {
            _logger = logger;
            _messageService = messageService;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Call the MessageReceiver for Update user Address
                var addressQueue = _configuration.GetValue<string>("RabbitMqQueueName:AddressQueue");
                var addressDetail = await _messageService.ReceiveMessage(addressQueue);
                if (!string.IsNullOrEmpty(addressDetail) && addressDetail.Length > 0)
                {
                    var APIUrl = _configuration.GetValue<string>("OrchestratorUrl");
                    try
                    {
                        var response = await client.PostAsJsonAsync($"{APIUrl}UpdateUser/address", addressDetail);
                        _logger.LogInformation(response.Content.ToString());
                    }catch (Exception ex)
                    {
                        _logger.LogInformation($"Exception in UpdataAddress : {ex.Message}");
                    }
                }

                // Call the MessageREceiver for Update Card Detail
                var cardQueue = _configuration.GetValue<string>("RabbitMqQueueName:CardQueue");
                var cardDetail = await _messageService.ReceiveMessage(cardQueue);
                if (!string.IsNullOrEmpty(cardDetail) && cardDetail.Length > 0)
                {
                    var APIUrl = _configuration.GetValue<string>("OrchestratorUrl");
                    try
                    {
                        var response = await client.PostAsJsonAsync($"{APIUrl}UpdateUser/card", cardDetail);
                        _logger.LogInformation(response.Content.ToString());
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation($"Exception in UpdataAddress : {ex.Message}");
                    }
                }

                _logger.LogInformation($"Worker Service is working : {DateTimeOffset.Now}");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}