using FluentValidation;

namespace API.Validators
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, string> Contains<T>(this IRuleBuilder<T, string> ruleBuilder, string needle)
        {
            return ruleBuilder.Must(sstring => sstring.Contains(needle)).WithMessage($"String must contain: {needle}");
        }
    }
}
