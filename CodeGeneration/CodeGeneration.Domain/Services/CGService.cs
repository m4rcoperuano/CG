using CodeGeneration.Domain.Models;
using CodeGeneration.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Domain.Services
{
    public class CGService
    {
        private string DEFAULT_MODAL_HTML;
        private string DEFAULT_SCRIPT;
        private ICodeGenerationRepository _cgRepository;

        public CGService(ICodeGenerationRepository cgRepo)
        {
            this.setProperties();
            this._cgRepository = cgRepo;
        }
        private void setProperties()
        {
            this.DEFAULT_MODAL_HTML = @"<modal>

    <div class=""form-group"">
        <label class=""col-sm-4"">Class Name: </label>
        <div class=""col-sm-8"">
            <input type=""text"" id=""class-name"" class=""form-control"" />
        </div>
    </div>

</modal>";

            this.DEFAULT_SCRIPT = @"";
        }

        public CodeGenerationModel GetNewCGModel()
        {
            CodeGenerationModel cgModel = new CodeGenerationModel();
            cgModel.HTMLThatGoesInModal = this.DEFAULT_MODAL_HTML;
            cgModel.ScriptThatExecutesUponButtonPress = this.DEFAULT_SCRIPT;
            cgModel.ScriptThatExecutesOnModalLoad = this.DEFAULT_SCRIPT;
            cgModel.CGDescription = "";

            return cgModel;
        }
        public Guid SaveCG(CodeGenerationModel codeGenerationModel, int userId)
        {
            codeGenerationModel.CodeGenerationID = Guid.NewGuid();
            codeGenerationModel.UpdatedAt = DateTime.Now;
            codeGenerationModel.CreatedAt = DateTime.Now;
            codeGenerationModel.CGCreatedBy = userId;
            codeGenerationModel.Active = true;

            if (String.IsNullOrWhiteSpace(codeGenerationModel.CGName))
            {
                codeGenerationModel.CGName = "[Untitled]";
            }

            if (codeGenerationModel.Tags != null)
            {
                if (codeGenerationModel.Tags.Count() > 0)
                {
                    CodeGeneration_TagModel tagModel = new CodeGeneration_TagModel();
                    tagModel.TagName = "Uncategorized";
                    tagModel.CreatedAt = DateTime.Now;
                    tagModel.UpdatedAt = DateTime.Now;
                    codeGenerationModel.Tags = new List<CodeGeneration_TagModel>();
                    ((List<CodeGeneration_TagModel>)codeGenerationModel.Tags).Add(tagModel);
                }
            }
            else
            {
                CodeGeneration_TagModel tagModel = new CodeGeneration_TagModel();
                tagModel.TagName = "Uncategorized";
                tagModel.CreatedAt = DateTime.Now;
                tagModel.UpdatedAt = DateTime.Now;
                codeGenerationModel.Tags = new List<CodeGeneration_TagModel>();
                ((List<CodeGeneration_TagModel>)codeGenerationModel.Tags).Add(tagModel);
            }

            return _cgRepository.SaveCodeGeneration(codeGenerationModel);
        }
        public bool Exists(Guid cgId)
        {
            return _cgRepository.Exists(cgId);
        }
        public bool SaveEdits(CodeGenerationModel cgModel)
        {
            if (String.IsNullOrEmpty(cgModel.CGName))
            {
                cgModel.CGName = "[Untitled]";
            }
            cgModel.UpdatedAt = DateTime.Now;
            return _cgRepository.SaveEdits(cgModel);
        }
        public IEnumerable<CodeGenerationModel> UserCGModels(int userId)
        {
            return _cgRepository.GetCGModelsFromUser(userId).Where(x=>x.Active);
        }
        public CodeGenerationModel GetCGModel(Guid id)
        {
            var cgModel = this._cgRepository.GetCGModel(id);
            return cgModel;
        }
        public bool DeleteCGModel(Guid id)
        {
            return this._cgRepository.Delete(id);
        }
    }
}
