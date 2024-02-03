using System.Net.Mail;
using Eventool.Application.Utility;
using Eventool.Domain.Organizers;
using FluentValidation;
using MediatR;
using OneOf.Types;

namespace Eventool.Application.Commands;

public class OrganizerSignUpCommandHandler(
    IOrganizerRepository organizerRepository,
    IValidator<OrganizerSignUpCommand> validator,
    IHasher hasher) 
    : IRequestHandler<OrganizerSignUpCommand, ValidatableResult<Guid>>
{
    public async Task<ValidatableResult<Guid>> Handle(OrganizerSignUpCommand request, CancellationToken cancellationToken)
    { 
        var result = await validator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return result;
        
        var (hashedPassword, salt) = hasher.Hash(request.Password);
        var organizer = new Organizer(
            Guid.NewGuid(),
            DateTime.UtcNow,
            request.FullName,
            new MailAddress(request.Email),
            new Password(hashedPassword, salt));

        await organizerRepository.CreateAsync(organizer, cancellationToken);

        return new Success<Guid>(organizer.Id);
    }
}