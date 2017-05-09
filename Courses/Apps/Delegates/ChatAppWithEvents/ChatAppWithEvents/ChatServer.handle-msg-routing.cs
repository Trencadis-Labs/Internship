namespace ChatAppWithEvents
{
  public partial class ChatServer : IChatServer
  {
    private void Client_OnChatEvent(object sender, ChatMessage e)
    {
      if (e == null)
      {
        return;
      }

      switch (e.Type)
      {
        case ChatMessageType.PeerToPeer:
          RoutePeerToPeerMessage(sender, e);
          break;

        case ChatMessageType.Broadcast:
        default:
          RouteBroadcastMessage(sender, e);
          break;
      }
    }

    private void RoutePeerToPeerMessage(object sender, ChatMessage msg)
    {
      ChatClientWrapper client;
      if (this.participants.TryGetValue(msg.DestinatarId, out client))
      {
        // destinatar client was found in list
        client.AsHandler().HandleChatMessage(sender, msg);
      }
      else
      {
        // destinatar was not found in list
        // we try to contact emitter back and signall the issue
        client = ChatClientWrapper.TryWrap(sender);

        // try to use it's handler implementation
        var errorHandler = client.AsHandler();

        if (errorHandler != null)
        {
          errorHandler.HandleErrorMessage(
            this,
            ChatMessage.PeerToPeer(
              $"Destinatar '{msg.DestinatarId}' doesn't exist",
              msg.DestinatarId));
        }
      }
    }

    private void RouteBroadcastMessage(object sender, ChatMessage msg)
    {
      foreach (var client in this.participants.Values)
      {
        client.AsHandler().HandleChatMessage(sender, msg);
      }
    }
  }
}
