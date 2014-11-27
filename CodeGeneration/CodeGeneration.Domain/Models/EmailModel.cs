using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Domain.Models
{
    public class EmailModel
    {
        public int EmailId { get; set; }
        public string EmailString { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentOn { get; set; }
    }
}
