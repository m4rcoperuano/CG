using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Domain.Models
{
    public class LanguagesModel
    {
        public Guid CG_Id { get; set; }
        public string CGName { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
