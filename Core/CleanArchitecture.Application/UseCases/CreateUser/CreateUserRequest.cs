using MediatR;

namespace CleanArchitecture.Application.UseCases.CreateUser;

public record CreateUserRequest(string Email, string Name) : IRequest<CreateUserResponse>;