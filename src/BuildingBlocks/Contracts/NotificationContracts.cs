namespace ModularMonolith.BuildingBlocks.Contracts;

public record Recipient(Guid UserId, string? Email);

    public enum NotificationType
    {
        UnKnown = 0,
        Promotion,
        Topup,
        Order,
        Transactional,
        ChangePassword
    }
    [Flags]
    public enum ChannelType
    {
        InApp = 0,
        Email,
        Web,
    }
    public enum NotificationPriority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }
