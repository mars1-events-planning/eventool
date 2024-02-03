using Eventool.Application.Utility;
using Eventool.Domain.Organizers;
using FluentValidation;
using MediatR;

namespace Eventool.Application.Commands;

public record OrganizerSignUpCommand(
    string FullName,
    string Email,
    string Password) : IRequest<ValidatableResult<Guid>>;
    
public class OrganizerSignUpCommandValidator : AbstractValidator<OrganizerSignUpCommand>
{
    private readonly IOrganizerRepository _organizerRepository;

    public OrganizerSignUpCommandValidator(IOrganizerRepository organizerRepository)
    {
        _organizerRepository = organizerRepository;

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Полное имя не может быть пустым.")
            .Matches(@"^[а-яА-ЯёЁa-zA-Z\s]+$").WithMessage("Полное имя может содержать только буквы и пробелы.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Электронная почта не может быть пустой.")
            .EmailAddress().WithMessage("Неверный формат электронной почты.")
            .MustAsync(BeUniqueEmail).WithMessage("Электронная почта уже используется.");


        RuleFor(x => x.Password)
            .Matches(@"^(?=.*[a-zа-я])(?=.*[A-ZА-Я])(?=.*\d)(?=.*[\W_]).{8,}$")
            .WithMessage("Пароль должен содержать минимум одну строчную букву, одну заглавную букву, одну цифру, один спецсимвол и быть длиной не менее 8 символов.")
            .NotEmpty().WithMessage("Пароль не может быть пустым.")
            .MinimumLength(6).WithMessage("Пароль должен содержать не менее 8 символов.");
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        => await _organizerRepository.IsEmailUniqueAsync(email, cancellationToken);
}