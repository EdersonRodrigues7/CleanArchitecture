using FluentValidation;

namespace CleanArchitecture.Application.UseCases.CreateUser;

public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.Email).NotEmpty().MaximumLength(50).EmailAddress();
        RuleFor(u => u.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
    }
}