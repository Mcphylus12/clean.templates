using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    public class ExampleBusinessFunction : IRequestHandler<ExampleParam, ExampleResult>
    {
        private readonly IRepository<ExampleBusinessModel> repository;

        public ExampleBusinessFunction(IRepository<ExampleBusinessModel> repository)
        {
            this.repository = repository;
        }

        public async Task<ExampleResult> Handle(ExampleParam request, CancellationToken cancellationToken)
        {
            var model = await repository.GetByIdAsync(request.Id);

            model.Age += request.AgeToAdd;

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
