namespace CleanArchitecture.Application.UseCases.DeleteUser;

public sealed record DeleteUserResponse
{
    public Guid Id { get; init; }
    public string? Email { get; init; }
    public string? Name { get; init; }
};