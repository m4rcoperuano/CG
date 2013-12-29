using CodeGeneration.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Domain.RepositoryInterfaces
{
    public interface ICodeGenerationRepository
    {
        Guid SaveCodeGeneration(CodeGenerationModel cgModel);
        bool Exists(Guid id);
        bool SaveEdits(CodeGenerationModel cgModel);
        IEnumerable<CodeGenerationModel> GetCGModelsFromUser(int userId);
        IEnumerable<CodeGenerationModel> GetAllCGModels();
        CodeGenerationModel GetCGModel(Guid id);
        bool Delete(Guid id);
    }
}
