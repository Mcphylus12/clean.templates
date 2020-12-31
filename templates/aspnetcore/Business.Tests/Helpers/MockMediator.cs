using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Tests.Helpers
{
    public class MockMediator : IMediator
    {
        private object response;
        private Dictionary<Type, object> typedResponses = new Dictionary<Type, object>();
        private List<object> notifications = new List<object>();

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            notifications.Add(notification);
            return Task.CompletedTask;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) 
            where TNotification : INotification
        {
            return this.Publish((object)notification, cancellationToken);
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult((TResponse)typedResponses[request.GetType()]);
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(response);
        }

        public void Returns(object response)
        {
            this.response = response;
        }

        public void Returns<TRequest, TResponse>(TResponse response)
            where TRequest : IRequest<TResponse>
        {
            typedResponses.Add(typeof(TRequest), response);
        }

        public IEnumerable<object> Notifications()
        {
            return notifications;
        }

        public IEnumerable<TNotification> Notifications<TNotification>()
        {
            return notifications.OfType<TNotification>();
        }
    }
}
