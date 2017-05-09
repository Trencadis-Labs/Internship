using System;

namespace ChatAppWithEvents
{
  class Program
  {
    static void Main(string[] args)
    {
      IChatServer server = new ChatServer();
      IChatParticipant client1 = new ChatParticipant("Ion (1)");
      IChatParticipant client2 = new ChatParticipant("Ghita (2)");
      IChatParticipant client3 = new ChatParticipant("Maria (3)");

      client1.ConnectTo(server);
      client2.ConnectTo(server);

      client1.Chat(ChatMessage.Broadcast("Salutare tututor!"));
      client2.Chat(ChatMessage.Broadcast("Noa, buna ziua!"));

      client1.Chat(ChatMessage.PeerToPeer(
        "Psst, Ghita, ia auzi, Maria nu e prin zona?",
        "Ghita (2)"));
      client2.Chat(ChatMessage.PeerToPeer(
        "Ba da mah, acuma intra!",
        "Ion (1)"));

      client3.ConnectTo(server);

      client3.Chat(ChatMessage.Broadcast("Salutare baieti!"));
      client2.Chat(ChatMessage.PeerToPeer("Vezi ca o intrebat Ion de tine", "Maria (3)"));

      client3.Chat(ChatMessage.PeerToPeer("Servus Ioane", "Ionica"));
      client3.Chat(ChatMessage.PeerToPeer("Servus Ioane", "Ion (1)"));

      client1.Disconnect(server);

      client3.Chat(ChatMessage.Broadcast("Bai Ioanee.....!?!?!?!"));

      Console.WriteLine(" ----- press any key to close this wonderful chat ---- ");
      Console.ReadKey();
    }
  }
}
