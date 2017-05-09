using System;

namespace ChatAppWithEvents
{
  public interface IChatMessageEmitter
  {
    event EventHandler<ChatMessage> OnChatEvent;
  }
}
