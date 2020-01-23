using FluentValidation;

namespace Application.User.Validators
{
    public static class ValidatorExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .NotEmpty()
                .MinimumLength(6).WithMessage("Pasword must be at least 6 characters")
                .Matches("[A-Z]").WithMessage("Password must containt 1 uppercase")
                .Matches("[a-z]").WithMessage("Password must containt at least 1 lowercase")
                .Matches("[0-9]").WithMessage("Password must contain one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");

            return options;                
        }
    }
}