using CodeGeneration.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Domain.RepositoryInterfaces
{
    public interface IEmailRepository
    {
        void SaveEmailRecord(EmailModel emailModel); 
    }
}
