using ShMI.ClientMain.Interface;
using ShMI.ClientMain.Modules;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcListButtons.xaml
    /// </summary>
    public partial class UcListButtons : UserControl
    {
        public UcListButtons()
        {
            InitializeComponent();
        }

        public ModuleMainWindow Module
        {
            get => DataContext as ModuleMainWindow;
            set => DataContext = value;
        }

        public enum BtnType
        {
            btn_Add = 1,
            btn_Edit = 2,
            btn_Delete = 3,
            btn_Util = 4,
        }

        public void Btn_Text(BtnType btnType, string ContentTab = null)
        {
            if (ContentTab != null)
            {
                (listButton.Items[(int)btnType] as Button).Content = ContentTab;
                (listButton.Items[(int)btnType] as Button).Tag = ContentTab;
            }
        }

        public void Btn_Visibility(BtnType btnType, Visibility btnVisib = Visibility.Visible)
        {
            (listButton.Items[(int)btnType] as Button).Visibility = btnVisib;
        }

        private void BtnMain_Click(object sender, RoutedEventArgs e)
        {
            Module?.SetWidthListButton(new UcStart(Module));
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Module?.AddItem();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Module?.EditItem();
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Module?.DeleteItem();
        }
        private void BtnUtil_Click(object sender, RoutedEventArgs e)
        {
            Module?.UtilItem();
        }
    }
}
