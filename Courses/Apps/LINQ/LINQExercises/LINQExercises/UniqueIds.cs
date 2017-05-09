using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExercises
{
  public static class UniqueIds
  {
    private static readonly Dictionary<Type, Dictionary<int, bool>> uniqueIDs = new Dictionary<Type, Dictionary<int, bool>>();

    public static bool IsUniqueId<T>(int id)
    {
      Type targetType = typeof(T);

      return UniqueIds.IsUniqueId(id, targetType);
    }

    public static bool IsUniqueId(int id, Type type)
    {
      if (type == null)
      {
        throw new ArgumentNullException(nameof(type));
      }

      if (!uniqueIDs.ContainsKey(type))
      {
        return true;
      }

      return !uniqueIDs[type].ContainsKey(id);
    }

    public static void UseUniqueId<T>(int id)
    {
      Type targetType = typeof(T);

      if (!UniqueIds.IsUniqueId<T>(id))
      {
        throw new ArgumentException($"The identifier value '{id}' used for type '{targetType}' is not unique");
      }

      if (!uniqueIDs.ContainsKey(targetType))
      {
        uniqueIDs.Add(targetType, new Dictionary<int, bool>());
      }

      uniqueIDs[targetType].Add(id, true);
    }

    public static void UseUniqueId(int id, Type type)
    {
      if (type == null)
      {
        throw new ArgumentNullException(nameof(type));
      }

      if (!UniqueIds.IsUniqueId(id, type))
      {
        throw new ArgumentException($"The identifier value '{id}' used for type '{type}' is not unique");
      }

      if (!uniqueIDs.ContainsKey(type))
      {
        uniqueIDs.Add(type, new Dictionary<int, bool>());
      }

      uniqueIDs[type].Add(id, true);
    }

    public static int GenerateUniqueId<T>()
    {
      return UniqueIds.GenerateUniqueId(typeof(T));
    }

    public static int GenerateUniqueId(Type type)
    {
      if (type == null)
      {
        throw new ArgumentNullException(nameof(type));
      }

      if (!uniqueIDs.ContainsKey(type))
      {
        uniqueIDs.Add(type, new Dictionary<int, bool>());
      }

      int nextId = uniqueIDs[type].Keys.Count > 0 
                    ?
                    uniqueIDs[type].Keys.Max() + 1
                    :
                    1;

      UniqueIds.UseUniqueId(nextId, type);

      return nextId;
    }
  }
}
