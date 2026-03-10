using BuildingBlocks.Core.CQRS;
using MediatR;

namespace ModularMonolith.Notification.Features;

public class SendEmailMessagesCommand() : ICommand
{
    public int SentMessagesCount { get; set; }
};


public class SendEmailMessagesCommandHandler : ICommandHandler<SendEmailMessagesCommand>
{
    public Task<Unit> Handle(SendEmailMessagesCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}