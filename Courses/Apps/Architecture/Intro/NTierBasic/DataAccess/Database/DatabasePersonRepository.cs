using DataAccess.Abstractions;
using Models;
using Models.Core;
using Models.CRUD;
using Models.Paging;
using Models.Sorting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Database
{
  public class DatabasePersonRepository : IPersonRepository
  {
    private readonly string connectionString;

    public DatabasePersonRepository(GlobalSettings settings)
      : this(connectionString: settings?.RepositoriesConfig?.Database?.ConnectionString)
    {
    }

    protected DatabasePersonRepository(string connectionString)
    {
      if (string.IsNullOrWhiteSpace(connectionString))
      {
        throw new ArgumentNullException(nameof(connectionString));
      }

      this.connectionString = connectionString;
    }

    public SortedPagedCollection<Person, PersonSortCriteria> GetPersonsPaged(int pageIndex, int pageSize, PersonSortCriteria sortCriteria, SortDirection sortDirection)
    {
      var sqlSort = new StringBuilder();
      ValueTypeMultiSwitch<PersonSortCriteria, SortDirection>
        .On(sortCriteria, sortDirection)
        .Case(PersonSortCriteria.ById, SortDirection.Ascending, () => sqlSort.Append("Id Asc"))
        .Case(PersonSortCriteria.ById, SortDirection.Descending, () => sqlSort.Append("Id Desc"))
        .Case(PersonSortCriteria.ByFirstName, SortDirection.Ascending, () => sqlSort.Append("FirstName Asc"))
        .Case(PersonSortCriteria.ByFirstName, SortDirection.Descending, () => sqlSort.Append("FirstName Desc"))
        .Case(PersonSortCriteria.ByLastName, SortDirection.Ascending, () => sqlSort.Append("LastName Asc"))
        .Case(PersonSortCriteria.ByLastName, SortDirection.Descending, () => sqlSort.Append("LastName Desc"))
        .Case(PersonSortCriteria.ByBirthDate, SortDirection.Ascending, () => sqlSort.Append("DateOfBirth Asc"))
        .Case(PersonSortCriteria.ByBirthDate, SortDirection.Descending, () => sqlSort.Append("DateOfBirth Desc"))
        .Default(() => sqlSort.Append("Id Asc"))
        .Evaluate();

      var sqlData = new StringBuilder();
      sqlData.Append(@"
        SELECT * FROM
        (
        SELECT
                ROW_NUMBER() OVER (ORDER BY " + sqlSort.ToString() + @") as RowNumber,
                Id,
                FirstName,
                LastName,
                DateOfBirth
        FROM
                Person
        ) as data
        WHERE
                data.RowNumber Between (@PageIndex * @PageSize + 1) and ((@PageIndex + 1) * @PageSize)
      ");

      var sqlCount = new StringBuilder();
      sqlCount.Append(@"
        select COUNT(*) from Person;
      ");

      List<Person> itemsPerPage = new List<Person>();
      int totalRecordsCount = 0;

      using (var sqlConnection = new SqlConnection(this.connectionString))
      {
        sqlConnection.Open();

        using (var sqlCmd = new SqlCommand(sqlData.ToString(), sqlConnection))
        {
          sqlCmd.Parameters.AddWithValue("PageIndex", pageIndex);
          sqlCmd.Parameters.AddWithValue("PageSize", pageSize);

          using (var reader = sqlCmd.ExecuteReader())
          {
            while (reader.Read())
            {
              itemsPerPage.Add(new Person
              {
                Id = (int)reader["Id"],
                FirstName = reader["FirstName"]?.ToString(),
                LastName = reader["LastName"]?.ToString(),
                DateOfBirth = (reader["DateOfBirth"] != DBNull.Value) ? (DateTime)reader["DateOfBirth"] : DateTime.MinValue
              });
            }
          }
        }

        using (var sqlCmd = new SqlCommand(sqlCount.ToString(), sqlConnection))
        {
          totalRecordsCount = (int)sqlCmd.ExecuteScalar();
        }

        sqlConnection.Close();
      }

      return new SortedPagedCollection<Person, PersonSortCriteria>(itemsPerPage, pageIndex, pageSize, totalRecordsCount, sortCriteria, sortDirection);
    }

    public Person Create(CreatePersonDTO createModel)
    {
      var sql = @"
        INSERT INTO Person(FirstName, LastName, DateOfBirth)
        VALUES(@FirstName, @LastName, @DateOfBirth);

        SELECT @@IDENTITY";

      using (var sqlConnection = new SqlConnection(this.connectionString))
      {
        sqlConnection.Open();

        using (var sqlCmd = new SqlCommand(sql, sqlConnection))
        {
          sqlCmd.Parameters.AddWithValue("FirstName", createModel.FirstName);
          sqlCmd.Parameters.AddWithValue("LastName", createModel.LastName);
          sqlCmd.Parameters.AddWithValue("DateOfBirth", createModel.DateOfBirth);

          var nextID = Convert.ToInt32(sqlCmd.ExecuteScalar());

          return new Person()
          {
            Id = nextID,
            FirstName = createModel.FirstName,
            LastName = createModel.LastName,
            DateOfBirth = createModel.DateOfBirth
          };
        }
      }
    }

    public Person GetById(int id)
    {
      var sqlQuery = new StringBuilder();
      sqlQuery.Append(@"
        SELECT
                Id,
                FirstName,
                LastName,
                DateOfBirth
        FROM
                Person
        WHERE
                Id = @Id;");

      Person result = null;

      using (var sqlConnection = new SqlConnection(this.connectionString))
      {
        sqlConnection.Open();

        using (var sqlCmd = new SqlCommand(sqlQuery.ToString(), sqlConnection))
        {
          sqlCmd.Parameters.AddWithValue("Id", id);

          using (var reader = sqlCmd.ExecuteReader())
          {
            if (reader.Read())
            {
              result = new Person
              {
                Id = (int)reader["Id"],
                FirstName = reader["FirstName"]?.ToString(),
                LastName = reader["LastName"]?.ToString(),
                DateOfBirth = (reader["DateOfBirth"] != DBNull.Value) ? (DateTime)reader["DateOfBirth"] : DateTime.MinValue
              };
            }
          }
        }

        sqlConnection.Close();
      }

      return result;
    }
  }
}
