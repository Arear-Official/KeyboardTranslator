using Microsoft.Win32;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Configuration;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System;

namespace RuToEnToRu
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        const string _Name = "Keyboard rewriter";

        private bool autorun;

        public bool Autorun { get { return autorun; } set { autorun = value; OnPropertyChanged(nameof(autorun)); ConfigurationManager.AppSettings.Set("Autorun", autorun.ToString()); } }

        public event PropertyChangedEventHandler PropertyChanged;
        private string txt;
        public string Txt { get { return txt; } set { txt = value; OnPropertyChanged(nameof(Txt)); } }

        private bool autooff;
        public bool Autooff { get { return autooff; } set { autooff = value; OnPropertyChanged(nameof(autooff)); ConfigurationManager.AppSettings.Set("Autooff", autooff.ToString()); } }

        private Notify _trey;

        public MainWindow()
        {
            Txt = "";
            Autorun = bool.Parse(ConfigurationManager.AppSettings.Get("Autorun"));
            Autooff = bool.Parse(ConfigurationManager.AppSettings.Get("Autooff"));
            InitializeComponent();
            this.DataContext = this;
            _trey = new Notify();
            if (autorun)
            {
                this.Hide();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            var hotKeyHost = new HotKeyHost((HwndSource)PresentationSource.FromVisual(this));
            hotKeyHost.AddHotKey(new CustomHotKey(Key.C, ModifierKeys.Alt, TranslateKey));
        }

        private void TranslateKey(Key e, ModifierKeys d)
        {
            if (!Autooff) this.Show();
            Txt = Translator.TranslateText(System.Windows.Clipboard.GetText());
            System.Windows.Clipboard.SetText(Txt);
        }

        private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string text = Translator.TranslateText(TextBox.Text);

                Txt = text;

            }
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            SetAutorun();
        }

        private void SetAutorun()
        {
            string ExePath = Assembly.GetExecutingAssembly().Location;

            RegistryKey registry = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");

            try
            {
                if (Autorun)
                {
                    registry.SetValue(_Name, ExePath);
                }
                else
                {
                    registry.DeleteValue(ExePath);
                }
                registry.Flush();
                registry.Close();
            }
            catch { }
        }


        private void TranslateKey()
        {
            if (!Autooff) this.Show();
            Txt = Translator.TranslateText(System.Windows.Clipboard.GetText());
            System.Windows.Clipboard.SetText(Txt);
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161, 2, 0);
        }

        private void pnlControlBar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _trey.SetVisible(true);
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
    
