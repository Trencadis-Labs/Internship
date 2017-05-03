namespace ChatAppWithEvents
{
  public interface IChatParticipant
  {
    string Id { get; }

    void ConnectTo(IChatServer server);

    void Disconnect(IChatServer server);

    void Chat(ChatMessage message);
  }
}
