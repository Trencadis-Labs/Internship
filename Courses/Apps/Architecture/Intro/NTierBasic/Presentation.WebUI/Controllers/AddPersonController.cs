using BusinessLogic;
using DataAccess.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models.CRUD;
using Presentation.WebUI.Models;
using System;

namespace Presentation.WebUI.Controllers
{
  public class AddPersonController : Controller
  {
    private readonly IPersonRepository personRepo;

    public AddPersonController(IPersonRepository personRepo)
    {
      this.personRepo = personRepo ?? throw new ArgumentNullException();
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Save(CreatePerson p)
    {
      var personBO = new PersonBusinessObject(this.personRepo);

      personBO.Create(new CreatePersonDTO()
      {
        FirstName = p.FirstName,
        LastName = p.LastName,
        DateOfBirth = p.DateOfBirth
      });

      return RedirectToAction("List", "Home");
    }
  }
}