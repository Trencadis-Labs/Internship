using BusinessLogic;
using BusinessLogic.Abstractions;
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
    private readonly IPersonImageFileNameGenerator fileNameGenerator;
    private readonly IFileManager fileManager;

    public HomeController(
      IPersonRepository personRepository,
      IPersonImageFileNameGenerator fileNameGenerator,
      IFileManager fileManager
)
    {
      this.personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
      this.fileNameGenerator = fileNameGenerator ?? throw new ArgumentNullException(nameof(fileNameGenerator));
      this.fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
    }

    public IActionResult Index()
    {
      return this.List();
    }

    public IActionResult List(int pageIndex = 0, int pageSize = 20, PersonSortCriteria criteria = PersonSortCriteria.ById, SortDirection sortDirection = SortDirection.Ascending)
    {
      var personBO = new PersonBusinessObject(this.personRepository, this.fileNameGenerator, this.fileManager);

      var data = personBO.GetPersonsPaged(pageIndex, pageSize, criteria, sortDirection);

      if (pageIndex > data.GetLastPageIndex())
      {
        pageIndex = data.GetLastPageIndex();

        data = personBO.GetPersonsPaged(pageIndex, pageSize, criteria, sortDirection);
      }

      return View("Index", data);
    }

    public IActionResult Error()
    {
      return View();
    }
  }
}
