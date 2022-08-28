using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {
        private static int _port = 35768;
        private static List<Socket> _clients = new List<Socket>();

        private static int _turn;

        static void Main(string[] args)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Any, _port);
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(ipPoint);
            listenSocket.Listen(2);
            //ждем пока подключатся
            Console.WriteLine("Server was started...");
            while (true)
            {
                Socket handler = listenSocket.Accept();
                //добавляем чухана в список игроков
                if (_clients.Count < 2)
                {
                    Console.WriteLine("New connection!");

                    _clients.Add(handler);
                }
                //как только собираются два - выбираем, кто ходит первый, и дальше ждем сообщения от игроков
                if (_clients.Count == 2)
                {
                    Console.WriteLine("Game started!!!");

                    ChooseTheFirst();

                    ListenForMessage();
                }
            }
        }
        //выбираем того, кто ходит первый
        static void ChooseTheFirst()
        {
            Random rand = new Random();
            _turn = rand.Next(0, 2);
            Console.WriteLine($"First: {_turn}");
            var data = Encoding.Unicode.GetBytes("go");
            _clients[_turn].Send(data);
            data = Encoding.Unicode.GetBytes("wait");
            _clients[1 ^ _turn].Send(data);
        }
        //отправляем количество оставшихся "палок" игрокам
        static void SendMessageToClients(string message, Socket sender)
        {
            var data = Encoding.Unicode.GetBytes(message);
            var receivers = _clients.Where(c => c != sender).ToList();

            foreach (var receiver in receivers)
            {
                Console.WriteLine($"Send data: {message}");
                receiver.Send(data);
            }
        }
        //получаем количество оставшихся палок от только что походившего игрока
        static void GetMessage(Socket client, out StringBuilder message)
        {
            var data = new byte[256];
            message = new StringBuilder();
            var bytes = client.Receive(data, data.Length, 0);
            message.Append(Encoding.Unicode.GetString(data, 0, bytes));
        }
        //ожидаем получения сообщения от игрока
        private static void ListenForMessage()
        {
            while (true)
            {
                foreach (var client in _clients)
                {
                    if (client.Available > 0)
                    {
                        GetMessage(client, out StringBuilder builder);
                        Console.WriteLine($"Get data: {builder}");

                        SendMessageToClients(builder.ToString(), client);
                        builder.Clear();
                    }
                }
            }
        }
    }
}