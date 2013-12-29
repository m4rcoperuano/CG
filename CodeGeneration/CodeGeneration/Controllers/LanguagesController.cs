using CodeGeneration.Domain.RepositoryInterfaces;
using CodeGeneration.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodeGeneration.Controllers
{
    public class LanguagesController : Controller
    {
        private ICodeGenerationRepository cgRepo;
        public LanguagesController(ICodeGenerationRepository repo)
        {
            this.cgRepo = repo;
        }
        public ActionResult Index()
        {
            LanguagesService languageService = new LanguagesService(this.cgRepo);
            var languages = languageService.GetAllLPublishedLanguages();
            return View(languages);
        }

    }
}
