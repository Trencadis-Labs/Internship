using DataAccess.Abstractions;
using Models;
using Models.CRUD;
using Models.Paging;
using Models.Sorting;
using System;

namespace BusinessLogic
{
  public class PersonBusinessObject
  {
    private readonly IPersonRepository personsRepository;

    public PersonBusinessObject(IPersonRepository personsRepository)
    {
      if (personsRepository == null)
      {
        throw new ArgumentNullException($"{nameof(personsRepository)}");
      }

      this.personsRepository = personsRepository;
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

    public Person Create(CreatePersonDTO createModel)
    {
      if(createModel == null)
      {
        throw new ArgumentNullException(nameof(createModel));
      }

      var newPerson = this.personsRepository.Create(createModel);

      return newPerson;
    }
  }
}
