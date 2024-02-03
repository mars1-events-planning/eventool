using Eventool.Application.Utility;
using Eventool.Domain.Organizers;
using MediatR;
using OneOf.Types;

namespace Eventool.Application.Commands;

public class CheckCredentialsCommandHandler(IOrganizerRepository organizerRepository, IHasher hasher)
    : IRequestHandler<CheckCredentialsCommand, CredentialsValidationResult>
{
    public async Task<CredentialsValidationResult> Handle(
        CheckCredentialsCommand request,
        CancellationToken cancellationToken)
    {
        var result = await organizerRepository.GetByEmail(request.Email, cancellationToken);
        return result.Match<CredentialsValidationResult>(
            organizer => IsPasswordValidFor(organizer, request.Password)
                ? new Success<Organizer>(organizer)
                : new WrongCredentials(),
            notFound => new WrongCredentials()
        );
    }

    private bool IsPasswordValidFor(Organizer organizer, string password)
    {
        var (hashedPassword, salt) = hasher.Hash(password, organizer.Password.Salt);
        return new Password(hashedPassword, salt) == organizer.Password;
    }
}