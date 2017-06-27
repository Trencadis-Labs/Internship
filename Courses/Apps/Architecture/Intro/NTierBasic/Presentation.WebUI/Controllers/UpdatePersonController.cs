using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebUI.Models;
using DataAccess.Abstractions;

namespace Presentation.WebUI.Controllers
{
  public class UpdatePersonController : Controller
  {
    private readonly IPersonRepository personRepo;

    public UpdatePersonController(IPersonRepository personRepo)
    {
      this.personRepo = personRepo ?? throw new ArgumentNullException();
    }

    public IActionResult Index(int id)
    {
      var person = this.personRepo.GetById(id);

      if(person == null)
      {
        return NotFound();
      }

      return View(new UpdatePerson
      {
        Id = person.Id,
        FirstName = person.FirstName,
        LastName = person.LastName,
        DateOfBirth = person.DateOfBirth
      });
    }

    public IActionResult Save(UpdatePerson p)
    {
      return RedirectToAction("List", "Home");
    }
  }
}