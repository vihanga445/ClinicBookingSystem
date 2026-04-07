using ClinicBooking.Domain.Entities;
using ClinicBooking.Domain.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace ClinicBooking.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendBookingConfirmationAsync(Appointment appointment)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Clinic System", _config["Email:From"]));
        message.To.Add(new MailboxAddress(appointment.Patient.FirstName, appointment.Patient.Email));
        message.Subject = "Appointment Confirmed";

        message.Body = new TextPart("html")
        {
            Text = $"<h2>Your appointment is confirmed!</h2>" +
                   $"<p>Date: {appointment.AppointmentDate:dd MMM yyyy}</p>" +
                   $"<p>Time: {appointment.StartTime}</p>" +
                   $"<p>Doctor: Dr. {appointment.Doctor.LastName}</p>"
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_config["Email:Host"], int.Parse(_config["Email:Port"]!), false);
        await client.AuthenticateAsync(_config["Email:Username"], _config["Email:Password"]);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}