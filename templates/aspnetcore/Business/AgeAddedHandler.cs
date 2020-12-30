using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    class AgeAddedHandler : INotificationHandler<AgeAddedNotification>
    {
        private readonly ILogger<AgeAddedHandler> logger;

        public AgeAddedHandler(ILogger<AgeAddedHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(AgeAddedNotification notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Age added to {id}", notification.Id);

            return Task.CompletedTask;
        }
    }
}
