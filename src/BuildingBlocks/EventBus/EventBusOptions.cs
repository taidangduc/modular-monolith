namespace ModularMonolith.BuildingBlocks.EventBus;

public class EventBusOptions
{
    public string Transport { get; set; }
    public RabbitMQOptions RabbitMQ { get; set; }

    public bool UsedRabbitMQ()
    {
        return Transport == "RabbitMQ";
    }

    public bool UsedInMemory()
    {
        return Transport == "InMemory";
    }
}

public class RabbitMQOptions
{
    public string HostName { get; set; }
    public string ExchangeName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public ushort? Port { get; set; }
}