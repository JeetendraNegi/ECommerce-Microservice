using MessageWorkerService;
using MessageWorkerService.AsyncMessaging;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        // register the service
        services.AddSingleton<IMessageReceiver, MessageReceiver>();
    })
    .Build();

await host.RunAsync();


