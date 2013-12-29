using CodeGeneration.Domain.Models;
using CodeGeneration.Domain.RepositoryInterfaces;
using CodeGeneration.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace CodeGeneration.Models.CodeGeneration
{
    public class CodeGenerationVM
    {
        public CodeGenerationModel CGModel { get; set; }
        public string CGTagsStr { get; set; }
        private ICodeGenerationRepository _cgRepo;

        //ctors
        public CodeGenerationVM()
        {
            this.setProperties();
        }

        public CodeGenerationVM(Guid id)
        {
            this.setProperties();
            this.CGModel = this._cgRepo.GetCGModel(id);
        }

        //set properties in any ctor
        private void setProperties()
        {
            this._cgRepo = Bootstrapper.UnityContainer.Resolve<ICodeGenerationRepository>();
        }

        public void NewCG()
        {
            CGService cgService = new CGService(_cgRepo);
            this.CGModel = cgService.GetNewCGModel();
        }
    }
}