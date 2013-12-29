using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Domain.Models
{
    public class CodeGenerationModel
    {
        public Guid CodeGenerationID { get; set; }
        public string CGName { get; set; }
        public string CGDescription { get; set; }
        public int CGCreatedBy { get; set; }
        public string HTMLThatGoesInModal { get; set; }
        public string ScriptThatExecutesUponButtonPress { get; set; }
        public string ScriptThatExecutesOnModalLoad { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public IEnumerable<CodeGeneration_TagModel> Tags { get; set; }
        public bool Active { get; set; }
        public bool Published { get; set; }
    }
}
