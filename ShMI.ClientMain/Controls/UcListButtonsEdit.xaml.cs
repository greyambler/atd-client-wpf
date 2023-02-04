using ShMI.ClientMain.Modules;
using System.Windows;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcListButtonsEdit.xaml
    /// </summary>
    public partial class UcListButtonsEdit : UserControl
    {
        public UcListButtonsEdit()
        {
            InitializeComponent();
        }

        public ModuleMainWindow Module
        {
            get => listButton.DataContext as ModuleMainWindow;
            set => listButton.DataContext = value;
        }

        public ModuleMainWindow ModuleMain { get; set; }

        public enum BtnType
        {
            btn_Add = 1,
            btn_Edit = 2,
        }

        public void Btn_Visibility(BtnType btnType, string ContentTab = null, Visibility btnVisib = Visibility.Visible)
        {
            (listButton.Items[(int)btnType] as Button).Visibility = btnVisib;
            if (ContentTab != null)
            {
                (listButton.Items[(int)btnType] as Button).Content = ContentTab;
                (listButton.Items[(int)btnType] as Button).Tag = ContentTab;
            }
        }

        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            Module?.SetWidthListButton(new UcStart(Module));

            //_ = new UcStart(ModuleMain);
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Module?.SaveItem();
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Module?.Cancel();
        }

    }
}
