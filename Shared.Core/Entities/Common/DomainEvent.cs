using MediatR;

namespace Shared.Core.Entities.Common;

public record DomainEvent(Guid Id) : INotification;