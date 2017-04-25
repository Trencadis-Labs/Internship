using System.Diagnostics;

namespace PlayingWithIEnumerable
{
  public static class DebuggingTool
  {
    [Conditional("DEBUG")]
    public static void DebugMsg(string message)
    {
      if (Debugger.IsAttached)
      {
        Debug.WriteLine(message);
      }
    }
  }
}
