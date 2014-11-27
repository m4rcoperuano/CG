using CodeGeneration.Domain.Models;
using CodeGeneration.Domain.RepositoryInterfaces;
using CodeGeneration.Domain.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Domain.Services
{
    public class EmailService : IEmailService
    {
        private string MAIL_SERVER;
        private string FROM_EMAIL;
        private string CREDENTIALS_USERNAME;
        private string CREDENTIALS_PASSWORD;
        private IEmailRepository _emailRepo;

        public EmailService(IEmailRepository emailRepo)
        {
            this.MAIL_SERVER = "";
            this.FROM_EMAIL = "";
            this.CREDENTIALS_USERNAME = "";
            this.CREDENTIALS_PASSWORD = "";
            this._emailRepo = emailRepo;

        }

        public void Send(string email, string subject, string body, DateTime todaysDate)
        {
            String Message = String.Empty;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(this.MAIL_SERVER);

            mail.From = new MailAddress(this.FROM_EMAIL);
            mail.To.Add(email);
            mail.Subject = subject;

            mail.IsBodyHtml = true;

            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(this.CREDENTIALS_USERNAME, this.CREDENTIALS_PASSWORD);

            SmtpServer.Send(mail);

            EmailModel emailModel = new EmailModel();
            emailModel.Body = body;
            emailModel.EmailString = email;
            emailModel.SentOn = todaysDate;
            emailModel.Subject = subject;

            this._emailRepo.SaveEmailRecord(emailModel);
        }


    }
}
