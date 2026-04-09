using ClinicBooking.Application.Common.Models;
using MediatR;

namespace ClinicBooking.Application.Features.Appointments.Queries;

public record GetAppointmentByIdQuery(Guid Id) : IRequest<Result<object>>;