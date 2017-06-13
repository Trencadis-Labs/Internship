using DataAccess.Abstractions;
using Models;
using Models.Paging;
using Models.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using Models.CRUD;

namespace DataAccess.InMemory
{
  public class InMemoryPersonRepository : IPersonRepository
  {
    private static readonly List<Person> personsCollection = new List<Person>();

    public InMemoryPersonRepository()
      : this(InMemoryPersonRepository.DefaultDataSetInitialization)
    {

    }

    public InMemoryPersonRepository(Func<IEnumerable<Person>> dataSetInitialization)
    {
      if (InMemoryPersonRepository.personsCollection.Count == 0)
      {
        if (dataSetInitialization != null)
        {
          var initialSet = dataSetInitialization();

          if (initialSet != null)
          {
            InMemoryPersonRepository.personsCollection.AddRange(initialSet);
          }
        }
      }
    }

    public SortedPagedCollection<Person, PersonSortCriteria> GetPersonsPaged(int pageIndex, int pageSize, PersonSortCriteria sortCriteria, SortDirection sortDirection)
    {
      IEnumerable<Person> query = InMemoryPersonRepository.personsCollection;

      switch (sortCriteria)
      {
        case PersonSortCriteria.ById:
        default:
          switch (sortDirection)
          {
            case SortDirection.Ascending:
            default:
              query = query.OrderBy(p => p.Id);
              break;

            case SortDirection.Descending:
              query = query.OrderByDescending(p => p.Id);
              break;
          }
          break;

        case PersonSortCriteria.ByFirstName:
          switch (sortDirection)
          {
            case SortDirection.Ascending:
            default:
              query = query.OrderBy(p => p.FirstName);
              break;

            case SortDirection.Descending:
              query = query.OrderByDescending(p => p.FirstName);
              break;
          }
          break;

        case PersonSortCriteria.ByLastName:
          switch (sortDirection)
          {
            case SortDirection.Ascending:
            default:
              query = query.OrderBy(p => p.LastName);
              break;

            case SortDirection.Descending:
              query = query.OrderByDescending(p => p.LastName);
              break;
          }
          break;

        case PersonSortCriteria.ByBirthDate:
          switch (sortDirection)
          {
            case SortDirection.Ascending:
            default:
              query = query.OrderBy(p => p.DateOfBirth);
              break;

            case SortDirection.Descending:
              query = query.OrderByDescending(p => p.DateOfBirth);
              break;
          }
          break;
      }

      int totalRecordsCount = query.Count();

      var data = query.Skip(pageIndex * pageSize).Take(pageSize);

      return new SortedPagedCollection<Person, PersonSortCriteria>(data, pageIndex, pageSize, totalRecordsCount, sortCriteria, sortDirection);
    }

    public Person Create(CreatePersonDTO createModel)
    {
      int nextID = InMemoryPersonRepository.personsCollection
                    .Max(p => p.Id) + 1;

      var newPerson = new Person
      {
        Id = nextID,
        FirstName = createModel.FirstName,
        LastName = createModel.LastName,
        DateOfBirth = createModel.DateOfBirth
      };

      InMemoryPersonRepository.personsCollection.Add(newPerson);

      return newPerson;
    }

    private static IEnumerable<Person> DefaultDataSetInitialization()
    {
      List<Person> persons = new List<Person>();
      var randomGen = new Random();

      for (int i = 0; i < 100; i++)
      {
        var person = new Person()
        {
          Id = i + 1,
          FirstName = "Generated",
          LastName = "Person " + (i + 1),
          DateOfBirth = new DateTime(
                          year: 1970 + randomGen.Next(0, 40),
                          month: randomGen.Next(1, 12),
                          day: randomGen.Next(1, 28))
        };

        persons.Add(person);
      }

      return persons;
    }
  }
}
