using ClinicBooking.Application.Common.Models;
using MediatR;

namespace ClinicBooking.Application.Features.Appointments.Commands;

public record CancelAppointmentCommand(Guid Id, string Reason) : IRequest<Result<bool>>;

public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        
        return Result<bool>.Success(true);
    }
}