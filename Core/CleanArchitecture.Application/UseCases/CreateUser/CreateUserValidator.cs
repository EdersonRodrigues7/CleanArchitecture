using FluentValidation;

namespace CleanArchitecture.Application.UseCases.CreateUser;

public sealed class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.Email).NotEmpty().MaximumLength(50).EmailAddress().WithMessage("Invalid Email Address");
        RuleFor(u => u.Name).NotEmpty().MinimumLength(3).MaximumLength(50).WithMessage("The Name must be between 3 and 50 characters");
    }
}