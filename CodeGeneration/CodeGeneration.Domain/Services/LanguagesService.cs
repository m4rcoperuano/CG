using CodeGeneration.Domain.Models;
using CodeGeneration.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Domain.Services
{
    public class LanguagesService
    {
        private ICodeGenerationRepository CGRepo;
        public LanguagesService(ICodeGenerationRepository repo)
        {
            this.CGRepo = repo;
        }

        public List<LanguagesModel> GetAllLPublishedLanguages()
        {
            List<CodeGenerationModel> cgModels = this.CGRepo.GetAllCGModels().Where(x=>x.Published).ToList();
            List<LanguagesModel> allLanguages = new List<LanguagesModel>();
            foreach (var cgM in cgModels)
            {
                LanguagesModel lm = new LanguagesModel();
                lm.CG_Id = cgM.CodeGenerationID;
                lm.CGName = cgM.CGName;
                lm.Description = cgM.CGDescription;
                lm.LastUpdated = cgM.UpdatedAt;
                allLanguages.Add(lm);
            }

            return allLanguages;
        }
    }
}
