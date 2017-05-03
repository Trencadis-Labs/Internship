namespace ChatAppWithEvents
{
  public interface IChatServer
  {
    void HandleConnectRequest<T>(T client)
      where T: IChatParticipant, IChatMessageEmitter, IChatMessageHandler;

    void HandleDisconnectRequest<T>(T client)
      where T: IChatParticipant, IChatMessageEmitter, IChatMessageHandler;
  }
}
