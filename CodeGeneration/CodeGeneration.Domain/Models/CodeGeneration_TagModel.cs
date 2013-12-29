using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Domain.Models
{
    public class CodeGeneration_TagModel
    {
        public int CodeGeneration_Tag_ID { get; set; }
        public string TagName { get; set; }
        public int CodeGeneration_Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
