using DataAccess.Abstractions;
using Models;
using Models.CRUD;
using Models.Extensions;
using Models.IO;
using Models.Paging;
using Models.Sorting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace DataAccess.Xml
{
  public class XmlPersonRepository : IPersonRepository
  {
    private readonly string xmlPath;
    private readonly XDocument xmlDocument;

    public XmlPersonRepository(GlobalSettings settings, IPathServices pathServices = null)
      : this(xmlPath: settings?.RepositoriesConfig?.Xml?.Path, pathServices: pathServices)
    {
    }

    public XmlPersonRepository(string xmlPath, IPathServices pathServices = null)
    {
      if (string.IsNullOrWhiteSpace(xmlPath))
      {
        throw new ArgumentNullException($"{nameof(xmlPath)}");
      }

      if (pathServices != null)
      {
        xmlPath = xmlPath.Replace(PathPlaceholders.CurrentDirectory, pathServices.GetCurrentDirectory());
      }

      if (!File.Exists(xmlPath))
      {
        throw new FileNotFoundException($"XML file cannot be found at path '{xmlPath}'");
      }

      this.xmlPath = xmlPath;
      this.xmlDocument = XDocument.Load(xmlPath);
    }

    public SortedPagedCollection<Person, PersonSortCriteria> GetPersonsPaged(int pageIndex, int pageSize, PersonSortCriteria sortCriteria, SortDirection sortDirection)
    {
      IEnumerable<Person> query = from persElement in this.xmlDocument.Descendants("Person")
                                  let dateOfBirthString = persElement.Element("DateOfBirth")?.Value
                                  let dateOfBirthFormat = persElement.Element("DateOfBirth") != null ?
                                                            persElement.Element("DateOfBirth").Attribute("format")?.Value
                                                            :
                                                            ""
                                  select new Person()
                                  {
                                    Id = int.Parse(persElement.Attribute("id").Value),
                                    FirstName = persElement.Element("FirstName")?.Value,
                                    LastName = persElement.Element("LastName")?.Value,
                                    DateOfBirth = dateOfBirthString.ParseWithFormat(dateOfBirthFormat),
                                    ImageFileName = persElement.Element("ImageFileName")?.Value,
                                  };

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
      var nextID = this.xmlDocument.Descendants("Person")
                    .Select(xmlElem => int.Parse(xmlElem.Attribute("id").Value))
                    .Max() + 1;

      var newPerson = new Person
      {
        Id = nextID,
        FirstName = createModel.FirstName,
        LastName = createModel.LastName,
        DateOfBirth = createModel.DateOfBirth,
        ImageFileName = createModel.ImageFileName
      };

      var personElement = new XElement(
        "Person",
        new XAttribute("id", nextID),
        new XElement("FirstName", newPerson.FirstName),
        new XElement("LastName", newPerson.LastName),
        new XElement(
          "DateOfBirth",
          new XAttribute("format", "dd-MM-yyyy"),
          newPerson.DateOfBirth.ToString("dd-MM-yyyy")),
        new XElement("ImageFileName", newPerson.ImageFileName)
       );

      this.xmlDocument.Root.Add(personElement);

      using (var stream = new FileStream(this.xmlPath, FileMode.Create))
      {
        this.xmlDocument.Save(stream);
      }

      return newPerson;

    }

    public Person GetById(int id)
    {
      var query = from persElement in this.xmlDocument.Descendants("Person")
                  let persId = int.Parse(persElement.Attribute("id").Value)
                  let dateOfBirthString = persElement.Element("DateOfBirth")?.Value
                  let dateOfBirthFormat = persElement.Element("DateOfBirth") != null ?
                                            persElement.Element("DateOfBirth").Attribute("format")?.Value
                                            :
                                            ""
                  where persId == id
                  select new Person()
                  {
                    Id = persId,
                    FirstName = persElement.Element("FirstName")?.Value,
                    LastName = persElement.Element("LastName")?.Value,
                    DateOfBirth = dateOfBirthString.ParseWithFormat(dateOfBirthFormat),
                    ImageFileName = persElement.Element("ImageFileName")?.Value,
                  };

      return query.FirstOrDefault();
    }
  }
}
