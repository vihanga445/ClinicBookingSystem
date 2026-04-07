using FluentValidation;

namespace ClinicBooking.Application.Features.Appointments.Commands;

public class BookAppointmentValidator : AbstractValidator<BookAppointmentCommand>
{
    public BookAppointmentValidator()
    {
        RuleFor(x => x.DoctorId).NotEmpty().WithMessage("Doctor is required.");
        RuleFor(x => x.PatientId).NotEmpty().WithMessage("Patient is required.");
        RuleFor(x => x.AppointmentDate)
            .GreaterThan(DateTime.Today)
            .WithMessage("Appointment must be in the future.");
        RuleFor(x => x.StartTime).NotEmpty().WithMessage("Start time is required.");
    }
}