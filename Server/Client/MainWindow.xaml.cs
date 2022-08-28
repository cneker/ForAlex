using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int _port = 35768;
        private static string _ip = "127.0.0.1";
        private static Socket? _socket;
        private static Button[] _buttons = new Button[20];
        private static Thread? _thread;
        private static string? _status;
        private static int _allNumber = 20;

        public MainWindow()
        {
            InitializeComponent();

            CreateButtons();
        }
        //инициализация "палок"
        private void CreateButtons()
        {
            int x = 40;
            for (int i = 0; i < 10; i++)
            {
                Button bnt = new Button()
                {
                    Margin = new Thickness(x * i, 50, 740 - x * i, 220),
                    Content = "#",
                    Width = 40,
                    Height = 90
                };

                grid.Children.Add(bnt);
                _buttons[i] = bnt;
            }
            for (int i = 10; i < 20; i++)
            {
                Button bnt = new Button()
                {
                    Margin = new Thickness(x * (i - 10), 140, 740 - x * (i - 10), 130),
                    Content = "#",
                    Width = 40,
                    Height = 90
                };

                grid.Children.Add(bnt);
                _buttons[i] = bnt;
            }
            ChooseButton.IsEnabled = false;
            foreach (var button in _buttons)
            {
                button.IsEnabled = false;
            }
        }
        //действие кнопки connect и соединение с сервером
        private void Connect(object sender, RoutedEventArgs e)
        {
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(ipPoint);
            StringBuilder builder = new StringBuilder();

            var button = sender as Button;
            button.IsEnabled = false;

            //поток смысл в методе, описанном ниже
            _thread = new Thread(() =>
            {
                while (true)
                {
                    //распределение: кто ходит первый
                    if (_socket.Available <= 0) continue;
                    GetResponseFromServer(out builder);
                    Debug.WriteLine($"Get data: {builder}");
                    switch (builder.ToString())
                    {
                        case "go":
                            _status = "go";
                            ChooseButton.Dispatcher.Invoke(() => ChooseButton.IsEnabled = true);
                            break;
                        case "wait":
                            _status = "wait";
                            break;
                    }

                    Status.Dispatcher.Invoke(() => Status.Text = _status);
                    break;
                }
                ListenServer();
            });
            _thread.Start();
        }

        //этот метод фоном проверяет переменную _state: если go, то скипает,
        //если wait, то ожидает сообщения от сервера.
        //нажатие на кнопку Choose меняет состояние на wait, а получение сообщения на go
        private void ListenServer()
        {
            while (true)
            {
                if (!_status.Equals("wait")) continue;

                if (_socket.Available > 0)
                {
                    GetResponseFromServer(out StringBuilder response);
                    Debug.WriteLine($"Get data: {response}");
                    _allNumber = int.Parse(response.ToString());
                    //проверяем, вытянул ли последнюю (последние) "палки" другой игрок (если да, то ты вин)
                    if (_allNumber == 0)
                    {
                        Status.Dispatcher.Invoke(() => Status.Text = "You win");
                        try
                        {
                            _thread.Interrupt();
                        }
                        finally
                        {
                            _socket.Shutdown(SocketShutdown.Both);
                        }
                        break;
                    }
                    HiddenButtons();

                    _status = "go";
                    ChooseButton.Dispatcher.Invoke(() => ChooseButton.IsEnabled = true);
                    Status.Dispatcher.Invoke(() => Status.Text = _status);
                }
            }
        }
        //ответ от сервера (количество оставшихся "палок" после хода другого игрока)
        private void GetResponseFromServer(out StringBuilder builder)
        {
            builder = new StringBuilder();
            var data = new byte[256];

            var bytes = _socket.Receive(data, data.Length, 0);
            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
        }
        //действие нажатия на кнопку выбора количества "палок"
        private void Choose(object sender, RoutedEventArgs e)
        {
            var result = IsInputValid(Number.Text, out int number);
            if (!result)
                return;

            _allNumber -= number;

            HiddenButtons();
            SendToAll();
            //проверяем, вытянул ли последнюю (последние) "палки" ходивший игрок (если да, то он слил катку)
            if (_allNumber == 0)
            {
                Status.Text = "You lose";
                try
                {
                    _thread.Interrupt();
                }
                finally
                {
                    _socket.Shutdown(SocketShutdown.Both);
                }
                return;
            }
            _status = "wait";
            Status.Text = _status;
            ChooseButton.IsEnabled = false;
        }
        //отправляем количество оставшихся "палок" на сервер, а сервер другому чухану
        private void SendToAll()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(_allNumber);
            Debug.WriteLine($"Send data: {builder}");

            byte[] data = Encoding.Unicode.GetBytes(builder.ToString());
            _socket.Send(data);
        }
        //проверяем на корректность ввода: больше 0, меньше 3 и чтобы не было больше количества оставшихся "палок"
        private bool IsInputValid(string input, out int number)
        {
            if(int.TryParse(input, out number))
            {
                if(number > 0 && number < 4 && _allNumber - number >= 0)
                {
                    Error.Text = "";
                    return true;
                }
            }
            Error.Text = "Input error";
            return false;
        }
        //прячем "вытянутые палки"
        private void HiddenButtons()
        {
            for (int i = 0; i < 20 - _allNumber; i++)
            {
                _buttons[i].Dispatcher.Invoke(() => _buttons[i].Visibility = Visibility.Hidden);
            }
        }
    }
}
