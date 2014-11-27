using CodeGeneration.Domain.Models;
using CodeGeneration.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Infrastructure.RepositoryImplementations
{
    public class EmailRepository : IEmailRepository
    {
        public void SaveEmailRecord(EmailModel emailModel)
        {
            using (var db = new CodeGenerationEntities())
            {
                EmailsSent emailSent = new EmailsSent();
                emailSent.body = emailModel.Body;
                emailSent.email = emailModel.EmailString;
                emailSent.sent_on = emailModel.SentOn;
                emailSent.subject = emailModel.Subject;

                db.EmailsSents.Add(emailSent);
                db.SaveChanges();
            }
        }
    }
}
