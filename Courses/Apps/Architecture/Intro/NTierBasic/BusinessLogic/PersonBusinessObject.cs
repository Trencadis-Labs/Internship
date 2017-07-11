using BusinessLogic.Abstractions;
using DataAccess.Abstractions;
using Models;
using Models.CRUD;
using Models.Paging;
using Models.Sorting;
using System;
using System.IO;

namespace BusinessLogic
{
  public class PersonBusinessObject
  {
    private readonly IPersonRepository personsRepository;
    private readonly IPersonImageFileNameGenerator fileNameGenerator;
    private readonly IFileManager fileManager;

    public PersonBusinessObject(
      IPersonRepository personsRepository,
      IPersonImageFileNameGenerator fileNameGenerator,
      IFileManager fileManager)
    {
      this.personsRepository = personsRepository ?? throw new ArgumentNullException(nameof(personsRepository));
      this.fileNameGenerator = fileNameGenerator ?? throw new ArgumentNullException(nameof(fileNameGenerator));
      this.fileManager = fileManager ?? throw new ArgumentNullException(nameof(fileManager));
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

      if(createModel.ImageData != null)
      {
        var fileName = this.fileNameGenerator.GetImageFileName(newPerson);

        var fileExtension = Path.GetExtension(createModel.ImageFileName);

        this.fileManager.SaveFile(fileName + "." + fileExtension, createModel.ImageData);
      }

      return newPerson;
    }

    public byte[] GetPersonImage(int personID)
    {
      var person = this.personsRepository.GetById(personID);

      var fileName = this.fileNameGenerator.GetImageFileName(person);

      var fileExtension = Path.GetExtension(person.ImageFileName);

      return this.fileManager.ReadFile(fileName + "." + fileExtension);
    }
  }
}
