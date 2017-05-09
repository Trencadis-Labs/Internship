namespace ChatAppWithEvents
{
  public interface IChatMessageHandler
  {
    void HandleChatMessage(object sender, ChatMessage e);

    void HandleErrorMessage(object sender, ChatMessage e);
  }
}
