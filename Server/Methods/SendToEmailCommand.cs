using DBLayer;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using Models;

namespace Server.Methods
{
    public  static class SendToEmailCommand
    {
        private static readonly int _port;
        private static readonly string _server;
        private static readonly string _login;
        private static readonly string _passw;

        private static readonly SmtpClient _client;

        static SendToEmailCommand()
        {
            var response = new Response();
            _port = 2525;
            _server = "sandbox.smtp.mailtrap.io";
            _login = "c7fdcac9c5edc6";
            _passw = "9f493e869ee4cd";

            _client = new SmtpClient(_server, _port)
            {
                Credentials = new NetworkCredential(_login, _passw),
                EnableSsl = true
            };
        }
        public static void Send(FlyCompanyDBInstance _db, NetworkStream _ns, Request _r)
        {
            try
            {
                byte[] response = System.Text.Encoding.ASCII.GetBytes("Email sent successfully");
                _ns.Write(response, 0, response.Length);
            }
            catch (Exception ex)
            {
                byte[] errorResponse = System.Text.Encoding.ASCII.GetBytes($"Error: {ex.Message}");
                _ns.Write(errorResponse, 0, errorResponse.Length);
            }
        }
        public static string PrepareEmailBody(Client client, Ticket ticket)
        {
            return $"Dear {client.FirstName},\n\n" +
                   $"Here are your ticket details:\n" +
                   $"Flight: {ticket.Number}\n" +
                   $"Date: {ticket.SaleTime}\n" +
                   "Thank you for choosing our airline.";
        }

        public static void SendEmail(string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress("my_adress@my_domain.com");
            var toAddress = new MailAddress(toEmail);

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                _client.Send(message);
            }
        }
    }
}