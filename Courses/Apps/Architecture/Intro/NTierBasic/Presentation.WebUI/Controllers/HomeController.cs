using BusinessLogic;
using DataAccess.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Extensions;
using Models.Paging;
using Models.Sorting;
using System;

namespace Presentation.WebUI.Controllers
{
  public class HomeController : Controller
  {
    private readonly IPersonRepository personRepository;

    public HomeController(IPersonRepository personRepository)
    {
      if (personRepository == null)
      {
        throw new ArgumentNullException($"{nameof(personRepository)}");
      }

      this.personRepository = personRepository;
    }

    public IActionResult Index()
    {
      return this.List();
    }

    public IActionResult List(int pageIndex = 0, int pageSize = 20, PersonSortCriteria criteria = PersonSortCriteria.ById, SortDirection sortDirection = SortDirection.Ascending)
    {
      var personBO = new PersonBusinessObject(this.personRepository);

      var data = personBO.GetPersonsPaged(pageIndex, pageSize, criteria, sortDirection);

      if (pageIndex > data.GetLastPageIndex())
      {
        pageIndex = data.GetLastPageIndex();

        data = personBO.GetPersonsPaged(pageIndex, pageSize, criteria, sortDirection);
      }

      return View("Index", data);
    }

    public IActionResult AddPerson()
    {
      return View();
    }

    public IActionResult SaveNewPerson(Person p)
    {
      return RedirectToAction("List");
    }

    public IActionResult Error()
    {
      return View();
    }
  }
}
