using ShMI.ClientMain.Controls;
using ShMI.ClientMain.Interface;
using ShMI.ClientMain.Modules;
using System;
using System.Windows;
using System.Windows.Input;

namespace ShMI.ClientMain
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public ModuleMainWindow ModuleMain
        {
            get => DataContext as ModuleMainWindow;
            set => DataContext = value;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool isAdmin =
#if _DEBUG
                true;
#else
                false;
#endif

            ModuleMain = new ModuleMainWindow(this, workGrid,
                new ResourceDictionary() { Source = new Uri("pack://application:,,,./Themes/ThemeMain.xaml") },
                isAdmin, Dispatcher);

            ModuleMain.SetWidthListButton(new UcStart(ModuleMain));
        }

        private Key Key_Down = Key.None;
        private string code = "";

        private void Main_Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift)
            {
                Key_Down = Key.LeftShift;
            }
        }
        private void Main_Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (Key_Down == Key.LeftShift)
            {
                if (e.Key != Key.LeftShift)
                {
                    code = string.Format("{0} {1}", code, e.Key);
                }
            }
            if (e.Key == Key.LeftShift)
            {
                Key_Down = Key.None;

                if (code == " S H M I")
                {
                    ModuleMain.IsAdmin = !ModuleMain.IsAdmin;

                    //Console.WriteLine($"ModuleMain.IsAdmin = {ModuleMain.IsAdmin}");

                    if ((workGrid.Children[0] as System.Windows.Controls.UserControl).Name == "ucStart")
                    {
                        UcListMainButton uc = (workGrid.Children[0] as IUcChildren)?.UcListButton as UcListMainButton;
                        (uc.DataContext as ModuleUcStart).IsAdmin = ModuleMain.IsAdmin;
                        uc?.IntinListButton();

                    }
                }
                code = "";
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _ = ModuleMain.Do(s => s.StopTime());
        }
    }
}
