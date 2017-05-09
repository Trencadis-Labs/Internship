namespace ArrayHelper
{
  public class DeleteResult<T>
  {
    public DeleteResult(T[] resultArray, T[] deletedElements)
    {
      this.ResultArray = resultArray ?? new T[0];
      this.DeletedElements = deletedElements ?? new T[0];
    }

    public T[] ResultArray { get; private set; }

    public T[] DeletedElements { get; private set; }
  }
}
