using System;
using System.Collections.Generic;

namespace ChatAppWithEvents
{
  public class ChatServer : IChatServer
  {
    private enum ConnectionRegisterStatus
    {
      RegistrationFailed = 0,

      AlreadyRegistered,

      RegisteredSuccesfully
    }

    private enum DisconnetUnregisterStatus
    {
      UnregistrationFailed = 0,

      ClientWasNotConnected,

      UnregisteredSuccesfully
    }

    private readonly Dictionary<string, IChatParticipant> participants;

    public ChatServer()
    {
      this.participants = new Dictionary<string, IChatParticipant>();
    }

    public void HandleConnectRequest(IChatParticipant client)
    {
      var registrationStatus = this.OnConnect_RegisterClient(client);
      if (registrationStatus != ConnectionRegisterStatus.RegisteredSuccesfully)
      {
        return;
      }

      this.OnConnect_AttachToEventSource(client);

      this.OnConnect_NotifyGroupAboutNewConnection(client);
    }

    public void HandleDisconnectRequest(IChatParticipant client)
    {
      var unregisterStatus = this.OnDisconnect_UnregisterClient(client);
      if (unregisterStatus != DisconnetUnregisterStatus.UnregisteredSuccesfully)
      {
        return;
      }

      this.OnDisconnect_DetachFromEventSource(client);

      this.OnDisconnect_NotifyGroupAboutEndConnection(client);
    }

    #region Logic to execute on new connection requests

    private ConnectionRegisterStatus OnConnect_RegisterClient(IChatParticipant client)
    {
      if (client == null)
      {
        // we don't accept null clients
        return ConnectionRegisterStatus.RegistrationFailed;
      }

      if (this.participants.ContainsKey(client.Id))
      {
        // client already connected
        return ConnectionRegisterStatus.AlreadyRegistered;
      }

      this.participants.Add(client.Id, client);

      return ConnectionRegisterStatus.RegisteredSuccesfully;
    }

    private void OnConnect_AttachToEventSource(IChatParticipant client)
    {
      IChatMessageEmitter emitter = client as IChatMessageEmitter;
      if (emitter == null)
      {
        throw new ArgumentException($"Class '{client.GetType()}' must also implement inteface '{typeof(IChatMessageEmitter)}'");
      }

      emitter.OnChatEvent += this.Client_OnChatEvent;
    }

    private void OnConnect_NotifyGroupAboutNewConnection(IChatParticipant client)
    {
      Console.WriteLine($"User '{client.Id}' has joined the group chat");
    }

    #endregion

    #region  Logic to execute on disconnect requests

    private DisconnetUnregisterStatus OnDisconnect_UnregisterClient(IChatParticipant client)
    {
      if (client == null)
      {
        // we don't accept null clients
        return DisconnetUnregisterStatus.UnregistrationFailed;
      }

      if (!this.participants.ContainsKey(client.Id))
      {
        // client wasn't connected
        return DisconnetUnregisterStatus.ClientWasNotConnected;
      }

      this.participants.Remove(client.Id);

      return DisconnetUnregisterStatus.UnregisteredSuccesfully;
    }

    private void OnDisconnect_DetachFromEventSource(IChatParticipant client)
    {
      IChatMessageEmitter emitter = client as IChatMessageEmitter;
      if (emitter == null)
      {
        throw new ArgumentException($"Class '{client.GetType()}' must also implement inteface '{typeof(IChatMessageEmitter)}'");
      }

      emitter.OnChatEvent -= this.Client_OnChatEvent;
    }

    private void OnDisconnect_NotifyGroupAboutEndConnection(IChatParticipant client)
    {
      Console.WriteLine($"User '{client.Id}' has left the group chat");
    }

    #endregion

    #region Logic for message routing

    private void Client_OnChatEvent(object sender, ChatMessage e)
    {
      if (e == null)
      {
        return;
      }

      switch (e.Type)
      {
        case ChatMessageType.PeerToPeer:
          ForwardPeerToPeerMessage(sender, e);
          break;

        case ChatMessageType.Broadcast:
        default:
          ForwardBroadcastMessage(sender, e);
          break;
      }
    }

    private void ForwardPeerToPeerMessage(object sender, ChatMessage msg)
    {
      IChatParticipant client;
      if (this.participants.TryGetValue(msg.DestinatarId, out client))
      {
        // client was found in list
        ChatParticipant_InvokeHandleChatMessage(sender, client, msg);
      }
      else
      {
        client = sender as IChatParticipant;
        if (client != null)
        {
          ChatParticipant_InvokeHandleErrorMessage(
            client,
            ChatMessage.PeerToPeer(
              $"Destinatar '{msg.DestinatarId}' doesn't exist",
              msg.DestinatarId));
        }
      }
    }

    private void ForwardBroadcastMessage(object sender, ChatMessage msg)
    {
      foreach (var client in this.participants.Values)
      {
        ChatParticipant_InvokeHandleChatMessage(sender, client, msg);
      }
    }

    private void ChatParticipant_InvokeHandleChatMessage(object sender, IChatParticipant toParticipant, ChatMessage msg)
    {
      IChatMessageHandler clientHandling = toParticipant as IChatMessageHandler;
      if (clientHandling != null)
      {
        clientHandling.HandleChatMessage(sender, msg);
      }
      else
      {
        throw new ArgumentException($"Class '{toParticipant.GetType()}' must also implement inteface '{typeof(IChatMessageHandler)}'");
      }
    }

    private void ChatParticipant_InvokeHandleErrorMessage(IChatParticipant toParticipant, ChatMessage msg)
    {
      IChatMessageHandler clientHandling = toParticipant as IChatMessageHandler;
      if (clientHandling != null)
      {
        clientHandling.HandleErrorMessage(this, msg);
      }
      else
      {
        throw new ArgumentException($"Class '{toParticipant.GetType()}' must also implement inteface '{typeof(IChatMessageHandler)}'");
      }
    }

    #endregion
  }
}
