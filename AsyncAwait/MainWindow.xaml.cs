using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using PocAsynchrone.Annotations;

namespace PocAsynchrone
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    { 

        private int _progress;

        public int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public async void MethodAsynchrone()
        {
            var t = new Task(Process);
            t.Start();
            await t;

            MessageBox.Show("Process Asynchrone Fini");
            Progress = 0;
        }

        public void MethodSynchrone()
        {
            Process();
            MessageBox.Show("Process Synchrone Fini");
            Progress = 0;
        }

        private void Process()
        {
            for (var i = 0; i <= 100; i++)
            {
                Progress = i;
                Thread.Sleep(50);
            }            
        }
       
        #region event
        private void ButtonBase_OnClickSynchrone(object sender, RoutedEventArgs e)
        {
            MethodSynchrone();
        }

        private void ButtonBase_OnClickAsynchrone(object sender, RoutedEventArgs e)
        {
            MethodAsynchrone();
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
