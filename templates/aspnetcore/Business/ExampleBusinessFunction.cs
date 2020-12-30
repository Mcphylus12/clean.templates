using Business.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    public class ExampleBusinessFunction : IRequestHandler<ExampleParam, ExampleResult>
    {
        private readonly IRepository<ExampleBusinessModel> repository;
        private readonly IPublisher publisher;
        private readonly ILogger<ExampleBusinessFunction> logger;

        public ExampleBusinessFunction(IRepository<ExampleBusinessModel> repository, IPublisher publisher, ILogger<ExampleBusinessFunction> logger)
        {
            this.repository = repository;
            this.publisher = publisher;
            this.logger = logger;
        }

        public async Task<ExampleResult> Handle(ExampleParam request, CancellationToken cancellationToken)
        {
            using (logger.TimeOperation("Adding age for id {id}", request.Id))
            {
                var model = await repository.GetByIdAsync(request.Id);

                model.Age += request.AgeToAdd;

                await repository.SaveChangesAsync();
            }

            await publisher.Publish(new AgeAddedNotification(request.Id));

            return new ExampleResult(true);
        }
    }

    public class ExampleParam : IRequest<ExampleResult>
    {
        public ExampleParam(int id, int ageToAdd)
        {
            Id = id;
            AgeToAdd = ageToAdd;
        }

        public int Id { get; }
        public int AgeToAdd { get; }
    }

    public class ExampleResult
    {
        public ExampleResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}
