using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Interfaces
{
    public interface IUrlBuilder
    {
        string CreateFullyQualifiedUrl(string action, string controller, object properties, string httpType);
    }
}
