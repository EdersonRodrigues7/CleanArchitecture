namespace CleanArchitecture.Application.UseCases.CreateUser;

public sealed record CreateUserResponse
{
    public Guid Id { get; init; }
    public string? Email { get; init; }
    public string? Name { get; init; }
}