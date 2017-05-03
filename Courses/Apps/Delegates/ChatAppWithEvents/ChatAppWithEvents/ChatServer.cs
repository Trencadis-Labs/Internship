using System;
using System.Collections.Generic;

namespace ChatAppWithEvents
{
  public partial class ChatServer : IChatServer
  {
    private readonly Dictionary<string, ChatClientWrapper> participants;

    public ChatServer()
    {
      this.participants = new Dictionary<string, ChatClientWrapper>();
    }
  }
}
