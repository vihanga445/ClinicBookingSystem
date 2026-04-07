using ClinicBooking.Domain.Entities;
using ClinicBooking.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ClinicBooking.Infrastructure.Persistence.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationDbContext _context;

    public AppointmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> HasConflictAsync(Guid doctorId, DateTime date, TimeSpan startTime)
    {
        return await _context.Appointments
            .AnyAsync(a => a.DoctorId == doctorId &&
                           a.AppointmentDate.Date == date.Date &&
                           a.StartTime == startTime &&
                           a.Status != Domain.Enums.AppointmentStatus.Cancelled);
    }

    public async Task AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task<Appointment?> GetByIdAsync(Guid id)
    {
        return await _context.Appointments
            .Include(a => a.Doctor)
            .Include(a => a.Patient)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.Appointments
            .Include(a => a.Doctor)
                .ThenInclude(d => d.Specialty)
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.AppointmentDate)
            .ToListAsync();
    }
}