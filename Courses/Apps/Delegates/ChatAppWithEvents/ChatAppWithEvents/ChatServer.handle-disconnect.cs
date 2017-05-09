using System;

namespace ChatAppWithEvents
{
  public partial class ChatServer : IChatServer
  {
    public void HandleDisconnectRequest<T>(T client)
      where T : IChatParticipant, IChatMessageEmitter, IChatMessageHandler
    {
      if (client == null)
      {
        // we don't accept null clients
        return;
      }

      if (!this.participants.ContainsKey(client.Id))
      {
        // client wasn't connected
        return;
      }

      client.OnChatEvent -= this.Client_OnChatEvent;

      this.participants.Remove(client.Id);

      this.NotifyGroupOnDisconnect(client);
    }

    #region  Logic to execute on disconnect requests

    protected virtual void NotifyGroupOnDisconnect(IChatParticipant client)
    {
      Console.WriteLine($"User '{client.Id}' has left the group chat");
    }

    #endregion
  }
}
