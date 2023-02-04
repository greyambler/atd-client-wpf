using ShMI.ClientMain.Modules;
using System.Windows;
using System.Windows.Input;

namespace ShMI.ClientMain
{
    /// <summary>
    /// Логика взаимодействия для WindDialog.xaml
    /// </summary>
    public partial class WindDialog : Window
    {
        public WindDialog()
        {
            InitializeComponent();
        }
        public ModuleWindDialog Module
        {
            get => LayoutRoot.DataContext as ModuleWindDialog;
            set => LayoutRoot.DataContext = value;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Normal;
        }

        public enum DialogType
        {
            Message = 0,
            Error = 1,
            Question = 2,
        }


        public WindDialog(DialogType dialogType, string textMessage = "здесь будет сообщение", double _FontSize = 14)
        {
            InitializeComponent();
            txt_dialog.FontSize = _FontSize;
            txt_dialog.Text = textMessage;
            if (dialogType == DialogType.Message)
            {
                Module.IconTypeWindow = Module.InitIconTypeWindow(PathImage: "Icons/Spell.ico");
            }
            if (dialogType == DialogType.Error)
            {
                Module.IconTypeWindow = Module.InitIconTypeWindow(PathImage: "Icons/Stop 2.ico");
            }
            if (dialogType == DialogType.Question)
            {
                btn_cancel.Visibility = Visibility.Collapsed;
                btn_yes.Visibility = Visibility.Visible;
                btn_no.Visibility = Visibility.Visible;
                Module.IconTypeWindow = Module.InitIconTypeWindow(PathImage: "Icons/Question.ico");
            }
        }


        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {

            //if (Pass.Visibility == Visibility.Visible)
            //{
            //    DialogResult = true;
            //    PassWord = Pass.Password;
            //}
            Close();

        }
        private void Pass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Btn_cancel_Click(this, null);
            }
        }
        private void Btn_no_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void Btn_yes_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

    }
}
