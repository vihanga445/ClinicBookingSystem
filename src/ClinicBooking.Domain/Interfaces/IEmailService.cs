using ClinicBooking.Domain.Entities;

namespace ClinicBooking.Domain.Interfaces;

public interface IEmailService
{
    Task SendBookingConfirmationAsync(Appointment appointment);
}