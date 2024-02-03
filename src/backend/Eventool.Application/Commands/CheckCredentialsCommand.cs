using Eventool.Domain.Organizers;
using MediatR;
using OneOf;
using OneOf.Types;

namespace Eventool.Application.Commands;

public record CheckCredentialsCommand(string Email, string Password) : IRequest<CredentialsValidationResult>;


[GenerateOneOf]
public partial class CredentialsValidationResult : OneOfBase<Success<Organizer>, WrongCredentials>
{
}

public record WrongCredentials()
{
    public const string Message = "Почта или пароль указаны неверно.";
}