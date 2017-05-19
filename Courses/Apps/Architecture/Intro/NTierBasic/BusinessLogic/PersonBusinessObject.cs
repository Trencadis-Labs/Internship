using DataAccess.Abstractions;
using DataAccess.Database;
using DataAccess.InMemory;
using DataAccess.Xml;
using Models;
using Models.Paging;
using Models.Sorting;
using System;

namespace BusinessLogic
{
  public class PersonBusinessObject
  {
    private readonly IPersonRepository personsRepository;

    public PersonBusinessObject()
    {
      // this.personsRepository = new InMemoryPersonRepository();
      // this.personsRepository = new XmlPersonRepository();
      this.personsRepository = new DatabasePersonRepository(@"Server=FLORIN-PC\MSSQL2012;Database=PersonsDB;Trusted_Connection=True;");
    }

    public SortedPagedCollection<Person, PersonSortCriteria> GetPersonsPaged(int pageIndex, int pageSize, PersonSortCriteria sortCriteria, SortDirection sortDirection)
    {
      if (pageIndex < 0)
      {
        throw new ArgumentException($"{nameof(pageIndex)} must be a positive numeric value");
      }

      if (pageSize <= 0)
      {
        throw new ArgumentException($"{nameof(pageSize)} must be a strict-positive numeric value");
      }

      return this.personsRepository.GetPersonsPaged(pageIndex, pageSize, sortCriteria, sortDirection);
    }
  }
}
