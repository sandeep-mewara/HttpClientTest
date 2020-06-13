using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HttpClientPerfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GetTimeTaken(100, lblResult100all, lblResult100single, btn100, lblPtage100);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetTimeTaken(500, lblResult500all, lblResult500single, btn500, lblPtage500);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            GetTimeTaken(1000, lblResult1000all, lblResult1000single, btn1000, lblPtage1000);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            GetTimeTaken(2000, lblResult2000all, lblResult2000single, btn2000, lblPtage2000);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            GetTimeTaken(5000, lblResult5000all, lblResult5000single, btn5000, lblPtage5000);
        }

        private async void GetTimeTaken(int connectionCount, Label lblResultAll, Label lblResultSingle, Button button, Label lblPtage)
        {
            IsOperationInProgress = true;

            var watch = new System.Diagnostics.Stopwatch();
            var timeAll = 1.0;
            var timeOne = 1.0;

            lblResultAll.Content = "";
            lblResultSingle.Content = "";
            lblPtage.Content = "";
            button.IsEnabled = false;

            watch.Reset();
            watch.Start();
            await Task.Run(() => TestHttpClientWithStaticInstance(connectionCount));
            watch.Stop();
            lblResultSingle.Content = $"{watch.ElapsedMilliseconds / 1000} seconds";
            timeOne = watch.ElapsedMilliseconds / 1000;

            watch.Reset();
            watch.Start();
            await Task.Run(() => TestHttpClientWithUsing(connectionCount)); 
            watch.Stop();
            lblResultAll.Content = $"{watch.ElapsedMilliseconds/1000} seconds";
            timeAll = watch.ElapsedMilliseconds / 1000;

            button.IsEnabled = true;

            var pTage = ((timeAll - timeOne) * 100) / timeAll;
            lblPtage.Content = $"{pTage:n2} %";

            IsOperationInProgress = false;
        }

        private static void TestHttpClientWithUsing(int noOfConnections)
        {
            try
            {
                for (var i = 0; i < noOfConnections; i++)
                {
                    using (var httpClient = new HttpClient())
                    {
                        var result = httpClient.GetAsync(new Uri("http://www.bing.com/")).Result;
                    }
                }
            }
            catch (Exception exception)
            {
                
            }
        }

        private static readonly HttpClient _httpClient = new HttpClient();
        private static void TestHttpClientWithStaticInstance(int noOfConnections)
        {
            try
            {
                for (var i = 0; i < noOfConnections; i++)
                {
                    var result = _httpClient.GetAsync(new Uri("http://www.bing.com/")).Result;
                }
            }
            catch (Exception exception)
            {
               
            }
        }

        private bool _isOperationInProgress = false;

        /// <summary>
        /// String property used in binding examples.
        /// </summary>
        public bool IsOperationInProgress
        {
            get { return _isOperationInProgress; }
            set
            {
                if (_isOperationInProgress != value)
                {
                    _isOperationInProgress = value;
                    NotifyPropertyChanged("IsOperationInProgress");
                }
            }
        }


        #region INotifyPropertyChanged Members

        //C#5: protected void NotifyPropertyChanged( [CallerMemberName] string propertyName = "")
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
