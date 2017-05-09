using System;

namespace ChatAppWithEvents
{
  public partial class ChatServer : IChatServer
  {
    private class ChatClientWrapper
    {
      private readonly object clientReference;

      private ChatClientWrapper(object client, bool enforceConstraints)
      {
        this.clientReference = null;

        if (client == null)
        {
          if (enforceConstraints)
          {
            throw new ArgumentNullException(nameof(client));
          }
        }
        else
        {
          bool isChatParticipant = client is IChatParticipant;
          bool isChatMsgEmitter = client is IChatMessageEmitter;
          bool isChatMsgHandler = client is IChatMessageHandler;

          if (isChatParticipant && isChatMsgEmitter && isChatMsgHandler)
          {
            this.clientReference = client;
          }
          else
          {
            if (enforceConstraints)
            {
              if (!isChatParticipant)
              {
                throw new ArgumentException($"Parameter '{nameof(client)}' of type '{client.GetType()}' must also implement interface '{typeof(IChatParticipant)}'");
              }

              if (!isChatMsgEmitter)
              {
                throw new ArgumentException($"Parameter '{nameof(client)}' of type '{client.GetType()}' must also implement interface '{typeof(IChatMessageEmitter)}'");
              }

              if (!isChatMsgHandler)
              {
                throw new ArgumentException($"Parameter '{nameof(client)}' of type '{client.GetType()}' must also implement interface '{typeof(IChatMessageHandler)}'");
              }
            }
          }
        }
      }

      public IChatParticipant AsParticipant()
      {
        if (this.clientReference == null)
        {
          return null;
        }

        return this.clientReference as IChatParticipant;
      }

      public IChatMessageEmitter AsEmitter()
      {
        if (this.clientReference == null)
        {
          return null;
        }

        return this.clientReference as IChatMessageEmitter;
      }

      public IChatMessageHandler AsHandler()
      {
        if (this.clientReference == null)
        {
          return null;
        }

        return this.clientReference as IChatMessageHandler;
      }

      public static ChatClientWrapper Wrap<T>(T client)
        where T : IChatParticipant, IChatMessageEmitter, IChatMessageHandler
      {
        return new ChatClientWrapper(client, true);
      }

      public static ChatClientWrapper TryWrap(object client)
      {
        return new ChatClientWrapper(client, false);
      }
    }
  }
}
