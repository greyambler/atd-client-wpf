using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcTestEdit.xaml
    /// </summary>
    public partial class Uc_TestEdit : UserControl
    {
        public Uc_TestEdit(ModuleUc_Test ModuleMain, TestTable EditItem)
        {
            InitializeComponent();
            Module = ModuleMain;
            Module.CurrentItem = EditItem;
        }

        public ModuleUc_Test Module
        {
            get => DataContext as ModuleUc_Test;
            set => DataContext = value;
        }
    }
}
