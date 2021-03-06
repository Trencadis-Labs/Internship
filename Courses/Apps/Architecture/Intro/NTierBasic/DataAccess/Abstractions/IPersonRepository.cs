﻿using Models;
using Models.CRUD;
using Models.Paging;
using Models.Sorting;

namespace DataAccess.Abstractions
{
  public interface IPersonRepository
  {
    SortedPagedCollection<Person, PersonSortCriteria> GetPersonsPaged(int pageIndex, int pageSize, PersonSortCriteria sortCriteria, SortDirection sortDirection);

    Person Create(CreatePersonDTO createModel);

    Person GetById(int id);
  }
}
