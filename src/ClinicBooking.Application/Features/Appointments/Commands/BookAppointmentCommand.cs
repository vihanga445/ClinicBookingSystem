using ClinicBooking.Application.Common.Models;
using ClinicBooking.Domain.Entities;
using ClinicBooking.Domain.Interfaces;
using MediatR;

namespace ClinicBooking.Application.Features.Appointments.Commands;

public record BookAppointmentCommand(
    Guid DoctorId,
    Guid PatientId,
    DateTime AppointmentDate,
    TimeSpan StartTime) : IRequest<Result<Guid>>;

public class BookAppointmentCommandHandler
    : IRequestHandler<BookAppointmentCommand, Result<Guid>>
{
    private readonly IAppointmentRepository _repo;
    private readonly IEmailService _emailService;

    public BookAppointmentCommandHandler(
        IAppointmentRepository repo,
        IEmailService emailService)
    {
        _repo = repo;
        _emailService = emailService;
    }

    public async Task<Result<Guid>> Handle(
        BookAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var conflict = await _repo.HasConflictAsync(
            request.DoctorId,
            request.AppointmentDate,
            request.StartTime);

        if (conflict)
            return Result<Guid>.Failure("This time slot is already booked.");

        var appointment = new Appointment
        {
            DoctorId = request.DoctorId,
            PatientId = request.PatientId,
            AppointmentDate = request.AppointmentDate,
            StartTime = request.StartTime,
            EndTime = request.StartTime.Add(TimeSpan.FromMinutes(30))
        };

        await _repo.AddAsync(appointment);
        await _emailService.SendBookingConfirmationAsync(appointment);

        return Result<Guid>.Success(appointment.Id);
    }
}