using CodeGeneration.Domain.Models;
using CodeGeneration.Domain.RepositoryInterfaces;
using CodeGeneration.Domain.Services;
using CodeGeneration.Interfaces;
using CodeGeneration.Models.CodeGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeGeneration.Controllers
{
    public class CodeGenerationController : Controller
    {
        private ICodeGenerationRepository _cgRepo;
        private IMembership _membership;
        public CodeGenerationController(ICodeGenerationRepository cgRepo, IMembership membership)
        {
            this._cgRepo = cgRepo;
            this._membership = membership;
        }

        public ActionResult Index()
        {
            CGService cgService = new CGService(_cgRepo);
            List<CodeGenerationModel> codeGenerationModels = cgService.UserCGModels(_membership.GetUserId()).ToList();
            return View(codeGenerationModels);
        }

        public ActionResult Create()
        {
            CodeGenerationVM cgVM = new CodeGenerationVM();
            cgVM.NewCG();
            Guid newId = this.Save(cgVM.CGModel.SerializeJsonNet());
            return RedirectToAction("Edit", new { id = newId });
        }

        public ActionResult Edit(Guid id)
        {
            CodeGenerationVM cgVM = new CodeGenerationVM(id);
            ViewBag.CGTitle = cgVM.CGModel.CGName;
            return View("Create", cgVM);
        }

        [HttpPost]
        [ValidateInput(false)]
        public Guid Save(string componentModelString)
        {
            CodeGenerationModel cgM = componentModelString.DeserializeJsonNet<CodeGenerationModel>();
            cgM.Active = true;
            CGService cgService = new CGService(_cgRepo);
            Guid cgId = cgM.CodeGenerationID;

            if (!cgService.Exists(cgM.CodeGenerationID))
            {
                cgId = cgService.SaveCG(cgM, _membership.GetUserId());
            }
            else
            {
                cgService.SaveEdits(cgM);
            }

            return cgId;
        }

        [HttpPost]
        public bool Delete(Guid id)
        {
            return this._cgRepo.Delete(id);
        }

        public ActionResult Details(Guid id)
        {
            CodeGenerationVM cgVM = new CodeGenerationVM(id);
            return View(cgVM);
        }
    }
}
