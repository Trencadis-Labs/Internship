using System;

namespace ChatAppWithEvents
{
  public partial class ChatServer : IChatServer
  {
    public void HandleConnectRequest<T>(T client)
      where T : IChatParticipant, IChatMessageEmitter, IChatMessageHandler
    {
      if (client == null)
      {
        // we don't accept null clients
        return;
      }

      if (this.participants.ContainsKey(client.Id))
      {
        // client already connected
        return;
      }

      // add client to the list of connected clients
      this.participants.Add(client.Id, ChatClientWrapper.Wrap(client));

      // attach to client messaging
      client.OnChatEvent += this.Client_OnChatEvent;

      this.NotifyGroupOnNewConnection(client);
    }

    #region Logic to execute on new connection requests

    protected virtual void NotifyGroupOnNewConnection(IChatParticipant client)
    {
      Console.WriteLine($"User '{client.Id}' has joined the group chat");
    }

    #endregion
  }
}
