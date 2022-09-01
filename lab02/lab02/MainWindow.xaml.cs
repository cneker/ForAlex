using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace lab02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //эта херня нужна для паузы
        static ManualResetEvent _suspend = new ManualResetEvent(true);
        //а эта херня для остановки таска
        static CancellationTokenSource _cancelTokenSource;
        CancellationToken _token;

        Task _task;
        bool _isPaused;
        bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        //действие кнопки старт
        void Start(Object sender, RoutedEventArgs e)
        {
            _cancelTokenSource = new CancellationTokenSource();
            _token = _cancelTokenSource.Token;
            int n = GetN();
            _task = new Task(() => Calculate(n), _token);
            _task.Start();
            IsRunning = true;
            _isPaused = false;
        }
        //действие кнопки пауза
        void Pause(Object sender, RoutedEventArgs e)
        {
            if(_isPaused)
                _suspend.Set();
            else
                _suspend.Reset();
            _isPaused = !_isPaused;
        }
        //действие кнопки стоп
        void Stop(Object sender, RoutedEventArgs e)
        {
            _cancelTokenSource.Cancel();
            if (_isPaused)
                _suspend.Set();
            IsRunning = false;
        }
        //алгоритм подсчета
        void Calculate(int n)
        {
            double sum = 0;
            for (int k = 0; k <= n; k++)
            {
                //для паузы
                _suspend.WaitOne(Timeout.Infinite);
                //если нажата кнопка стоп
                if (_token.IsCancellationRequested)
                    return;
                var temp = 1 / Math.Pow(2, k);
                //выводим на экран значение каждые 0.5 секунды
                TextVlock.Dispatcher.Invoke(() => TextVlock.Text = temp.ToString());
                sum += temp;
                Thread.Sleep(500);
            }
            TextVlock.Dispatcher.Invoke(() => TextVlock.Text = sum.ToString());
            IsRunning = false;
        }
        //получаем число, введенное пользователем
        private int GetN() => int.Parse(TextBoxN.Text);

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
