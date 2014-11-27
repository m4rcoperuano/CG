using CodeGeneration.Domain.Models;
using CodeGeneration.Domain.RepositoryInterfaces;
using CodeGeneration.Domain.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Domain.FauxServices
{
    public class FauxEmailService : IEmailService
    {
        private IEmailRepository _emailRepo;

        public FauxEmailService(IEmailRepository emailRepo)
        {
            this._emailRepo = emailRepo;
        }

        public void Send(string email, string subject, string body, DateTime todaysDate)
        {
            EmailModel emailModel = new EmailModel();
            emailModel.Body = body;
            emailModel.EmailString = email;
            emailModel.SentOn = todaysDate;
            emailModel.Subject = subject;

            this._emailRepo.SaveEmailRecord(emailModel);
        }
    }
}
