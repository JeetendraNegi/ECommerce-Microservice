namespace MessageWorkerService;

using MessageBrokerService.AsyncMessaging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IMessageReceiver _messageService;
    private readonly IConfiguration _configuration;
    private static HttpClient client = new HttpClient();
    private readonly string APIUrl = "";
    public Worker(ILogger<Worker> logger,
        IMessageReceiver messageService,
        IConfiguration configuration)
    {
        _logger = logger;
        _messageService = messageService;
        _configuration = configuration;
        APIUrl = _configuration.GetValue<string>("OrchestratorUrl");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", await GetToken());
        while (!stoppingToken.IsCancellationRequested)
        {
            // Call the MessageReceiver for Update user Address
            var addressQueue = _configuration.GetValue<string>("RabbitMqQueueName:AddressQueue");
            var addressDetail = await _messageService.ReceiveMessage(addressQueue);
            if (!string.IsNullOrEmpty(addressDetail) && addressDetail.Length > 0)
            {
                try
                {
                    var response = await client.PostAsJsonAsync($"{APIUrl}u/Users/UpdateUser/address", addressDetail);
                    _logger.LogInformation(response.Content.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Exception in UpdataAddress : {ex.Message}");
                }
            }

            // Call the MessageREceiver for Update Card Detail
            var cardQueue = _configuration.GetValue<string>("RabbitMqQueueName:CardQueue");
            var cardDetail = await _messageService.ReceiveMessage(cardQueue);
            if (!string.IsNullOrEmpty(cardDetail) && cardDetail.Length > 0)
            {
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

    private async Task<string> GetToken()
    {
        //get token to update the Address
        LoginDto loginDto = new LoginDto();
        loginDto.UserID = _configuration.GetValue<string>("UserID");
        loginDto.Password = _configuration.GetValue<string>("Password");

        //var content = new FormUrlEncodedContent(pairs);
        var tokenResponse = await client.PostAsJsonAsync($"{APIUrl}l/login", loginDto);
        var token = "";
        if (tokenResponse.StatusCode == System.Net.HttpStatusCode.OK)
        {
            token = JsonSerializer.Deserialize<LoginResponse>(tokenResponse.Content.ReadAsStringAsync().Result).token;
        }
        return token;
    }
}

public class LoginDto
{
    public string UserID { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string token { get; set; } = string.Empty;
    public string expiration { get; set; } = string.Empty;
}