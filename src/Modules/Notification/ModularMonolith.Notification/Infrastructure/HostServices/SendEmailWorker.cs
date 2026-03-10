using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModularMonolith.Notification.Features;

namespace ModularMonolith.Notification.Infrastructure.HostServices;

public class SendEmailWorker : BackgroundService
{
    private readonly ILogger<SendEmailWorker> _logger;
    private readonly IMediator _mediator;

    public SendEmailWorker(ILogger<SendEmailWorker> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogDebug("SendEmailService is starting.");  
        await ProcessAsync(stoppingToken);
    }

    private async Task ProcessAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogDebug($"SendEmail task doing background work.");

            var sendEmailsCommand = new SendEmailMessagesCommand();

            await _mediator.Send(sendEmailsCommand, cancellationToken);

            if (sendEmailsCommand.SentMessagesCount == 0)
            {
                await Task.Delay(10000, cancellationToken);
            }
        }

        _logger.LogDebug($"SendEmail background task is stopping.");
    }
}
