using AutoMapper;
using CodeGeneration.Domain.Models;
using CodeGeneration.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneration.Infrastructure.RepositoryImplementations
{
    public class CodeGenerationRepository : ICodeGenerationRepository
    {
        public CodeGenerationRepository()
        {
            Mapper.CreateMap<CodeGenerationModel, CodeGenerator>();
            Mapper.CreateMap<CodeGenerator, CodeGenerationModel>();
        }

        public Guid SaveCodeGeneration(CodeGenerationModel cgModel)
        {
            using (var db = new CodeGenerationEntities())
            {
                CodeGenerator dbCodeGenerator = Mapper.Map<CodeGenerator>(cgModel);
                db.CodeGenerators.Add(dbCodeGenerator);
                db.SaveChanges();

                return dbCodeGenerator.CodeGenerationID;
            }
        }

        public bool Exists(Guid id)
        {
            using (var db = new CodeGenerationEntities())
            {
                var cgItem = db.CodeGenerators.SingleOrDefault(x => x.CodeGenerationID == id);
                return cgItem != null;
            }
        }

        public bool SaveEdits(CodeGenerationModel cgModel)
        {
            using (var db = new CodeGenerationEntities())
            {
                var cgDataItem = db.CodeGenerators.SingleOrDefault(x => x.CodeGenerationID == cgModel.CodeGenerationID);
                cgDataItem.CGName = cgModel.CGName;
                cgDataItem.HTMLThatGoesInModal = cgModel.HTMLThatGoesInModal;
                cgDataItem.ScriptThatExecutesOnModalLoad = cgModel.ScriptThatExecutesOnModalLoad;
                cgDataItem.ScriptThatExecutesUponButtonPress = cgModel.ScriptThatExecutesUponButtonPress;
                cgDataItem.UpdatedAt = cgModel.UpdatedAt;
                cgDataItem.CGDescription = cgModel.CGDescription;
                cgDataItem.Active = cgModel.Active;
                cgDataItem.Published = cgModel.Published;

                db.SaveChanges();
                return true;
            }
        }

        public IEnumerable<CodeGenerationModel> GetCGModelsFromUser(int userId)
        {
            using (var db = new CodeGenerationEntities())
            {
                var dbCGs = db.CodeGenerators.Where(x => x.CGCreatedBy == userId);
                foreach (var cg in dbCGs)
                {
                    CodeGenerationModel cgModel = Mapper.Map<CodeGenerationModel>(cg);
                    yield return cgModel;
                }
            }
        }

        public IEnumerable<CodeGenerationModel> GetAllCGModels()
        {
            using (var db = new CodeGenerationEntities())
            {
                var dbCGs = db.CodeGenerators;
                foreach (var cg in dbCGs)
                {
                    CodeGenerationModel cgModel = Mapper.Map<CodeGenerationModel>(cg);
                    yield return cgModel;
                }
            }
        }

        public CodeGenerationModel GetCGModel(Guid id)
        {
            using (var db = new CodeGenerationEntities())
            {
                var dbCG = db.CodeGenerators.SingleOrDefault(x => x.CodeGenerationID == id);
                CodeGenerationModel cgModel = Mapper.Map<CodeGenerationModel>(dbCG);
                return cgModel;
            }
        }


        public bool Delete(Guid id)
        {
            using (var db = new CodeGenerationEntities())
            {
                var dbCG = db.CodeGenerators.SingleOrDefault(x => x.CodeGenerationID == id);
                db.CodeGenerators.Remove(dbCG);
                db.SaveChanges();
                return true;
            }
        }
    }
}
