using System;
using System.Linq;

namespace LINQExercises
{
  public partial class DataSet
  {
    private void InitializeUniversities(params University[] universities)
    {
      if (universities == null)
      {
        return;
      }

      this.universities.AddRange(universities);
    }

    private int GetUniversityId(Predicate<University> universityPredicate)
    {
      if (universityPredicate == null)
      {
        throw new ArgumentNullException(nameof(universityPredicate));
      }

      var id = this.universities.First(u => universityPredicate(u)).Id;

      return id;
    }

    private int GetUniversityIdByName(string universityName)
    {
      return this.GetUniversityId(univ => string.Equals(univ.Name, universityName, StringComparison.OrdinalIgnoreCase));
    }
  }
}
