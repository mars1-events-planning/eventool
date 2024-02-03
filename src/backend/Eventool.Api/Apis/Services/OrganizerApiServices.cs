using MediatR;

namespace Eventool.Api.Apis.Services;

public record OrganizerApiServices(
    ILogger<OrganizerApiServices> Logger,
    IMediator Mediator);