using Canina.Domain.Entities;

namespace Canina.Application.Features.Citas.ChangeStatus;

public record ChangeStatusCommand(Guid Id, EstatusCita nuevo_estatus);