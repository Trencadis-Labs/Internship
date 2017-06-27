using BusinessLogic;
using BusinessLogic.Abstractions;
using DataAccess.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.CRUD;
using Presentation.WebUI.FileUploads;
using Presentation.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.WebUI.Controllers
{
  public class AddPersonController : Controller
  {
    private readonly IPersonRepository personRepo;
    private readonly IPersonImageFileNameGenerator fileNameGenerator;
    private readonly IFileManager fileManager;


    public AddPersonController(
      IPersonRepository personRepo,
      IPersonImageFileNameGenerator fileNameGenerator,
      IFileManager fileManager)
    {
      this.personRepo = personRepo ?? throw new ArgumentNullException();
      this.fileNameGenerator = fileNameGenerator ?? throw new ArgumentNullException(nameof(fileNameGenerator));
      this.fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> Save(CreatePerson p)
    {
      var personBO = new PersonBusinessObject(this.personRepo, this.fileNameGenerator, this.fileManager);

      personBO.Create(new CreatePersonDTO()
      {
        FirstName = p.FirstName,
        LastName = p.LastName,
        DateOfBirth = p.DateOfBirth,
        ImageData = await p.Image.GetRawBytesAsync(),
        ImageFileName = p.Image.FileName
      });

      return RedirectToAction("List", "Home");
    }
  }
}