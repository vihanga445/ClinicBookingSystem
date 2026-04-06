namespace ClinicBooking.Domain.Entities;

public class Doctor : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public decimal ConsultationFee { get; set; }
    public int SlotDurationMinutes { get; set; } = 30;
    public bool IsApproved { get; set; } = false;
    public Guid SpecialtyId { get; set; }
    public Specialty Specialty { get; set; } = null!;
    public ICollection<Availability> Availabilities { get; set; } = new List<Availability>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
