using Eventool.Domain.Events;
using Eventool.Infrastructure.Persistence;
using FluentValidation;
using MediatR;

namespace Eventool.Application.UseCases;

public record EditEventRequest(
    Guid EventId,
    string Title,
    string? Address = null,
    string? Description = null
) : IRequest<Event>;

public class EditEventHandler(
    IValidator<EditEventRequest> validator,
    IUnitOfWork unitOfWork) : IRequestHandler<EditEventRequest, Event>
{
    public async Task<Event> Handle(EditEventRequest request, CancellationToken cancellationToken) =>
        await unitOfWork.ExecuteAndCommitAsync(async repositories =>
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);

            var @event = await repositories
                .EventRepository
                .GetByIdAsync(request.EventId, cancellationToken);

            if (@event is null)
                throw new InvalidOperationException("Событие не найдено")
                {
                    Data = { ["eventId"] = request.EventId }
                };

            if (request.StepId is not null && @event.Steps.FirstOrDefault(x => x.Id == request.StepId) is { } step)
            {
                step.SetTitle(request.Title);
                step.SetDescription(request.Description);
            }
            else
            {
                @event.AddTimelineStep(new TimelineStep(Guid.NewGuid(), request.Title)
                {
                    Description = request.Description
                });
            }

            repositories.EventRepository.Save(@event);

            return @event;
        }, cancellationToken);
}

public class EditEventRequestValidator : AbstractValidator<EditEventRequest>
{
    public EditEventRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .Length(2, 80)
            .WithMessage("Название должно быть от 2 до 80 символов");

        When(x => x.Description is not null and not "", () =>
        {
            RuleFor(x => x.Description)
                .Length(2, 500)
                .WithMessage("Описание должно быть от 2 до 500 символов");
        });
        
        When(x => x.Address is not null and not "", () =>
        {
            RuleFor(x => x.Address)
                .Length(2, 150)
                .WithMessage("Адрес должен быть от 2 до 500 символов");
        });
    }
}