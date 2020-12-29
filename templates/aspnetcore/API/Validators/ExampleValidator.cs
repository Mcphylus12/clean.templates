using API.ViewModels;
using FluentValidation;

namespace API.Validators
{
    internal class ExampleValidator : AbstractValidator<ExampleViewModel>
    {
        public ExampleValidator()
        {
            RuleFor(x => x.Name).Length(0, 100).Contains(" ");
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Age).InclusiveBetween(18, 60);
        }
    }
}