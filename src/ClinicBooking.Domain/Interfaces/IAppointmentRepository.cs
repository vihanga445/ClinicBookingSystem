using ClinicBooking.Domain.Entities;

namespace ClinicBooking.Domain.Interfaces;

public interface IAppointmentRepository
{
    Task<bool> HasConflictAsync(Guid doctorId, DateTime date, TimeSpan startTime);
    Task AddAsync(Appointment appointment);
    Task<Appointment?> GetByIdAsync(Guid id);
    Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId);
}