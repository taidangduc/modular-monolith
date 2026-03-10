namespace ModularMonolith.Notification.Domain.Entities;

public class EmailMessage
{
    public Guid Id { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public int AttemptCount { get; set; }
    public int MaxAttemptCount { get; set; }
    public DateTimeOffset? SentDateTime { get; set; }

    public EmailMessage Create(string to, string subject, string body, string attemptCount, string maxAttemptCount)
    {
        return new EmailMessage
        {
            To = to,
            Subject = subject,
            Body = body,
            AttemptCount = int.Parse(attemptCount),
            MaxAttemptCount = int.Parse(maxAttemptCount),
        };
    }
}
