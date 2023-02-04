using ShMI.ClientMain.Interface;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcTestTradeHouse.xaml
    /// </summary>
    public partial class UcTestTradeHouse : UserControl
    {
        public UcTestTradeHouse(ModuleMainWindow ModuleMain)
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitButtons();
        }

        public void InitButtons()
        {
            //bool isShow = Module.IsExist_Servise();

            //(UcListButton as UcListButtons).Btn_Visibility(UcListButtons.BtnType.btn_Add);

            //(UcListButton as UcListButtons).Btn_Visibility(UcListButtons.BtnType.btn_Edit);

            ucListButton.Btn_Visibility(UcListButtons.BtnType.btn_Util, btnVisib: System.Windows.Visibility.Collapsed);

            //(UcListButton as UcListButtons).Btn_Visibility(UcListButtons.BtnType.btn_Util, "Опросить");

        }


    }
}
