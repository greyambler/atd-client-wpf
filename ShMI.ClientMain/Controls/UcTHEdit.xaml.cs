using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcTHEdit.xaml
    /// </summary>
    public partial class UcTHEdit : UserControl
    {
        public UcTHEdit(ModuleUcTH ModuleMain, NCassa EditItem)
        {
            InitializeComponent();
            Module = ModuleMain;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcTH Module
        {
            get => DataContext as ModuleUcTH;
            set => DataContext = value;
        }
    }
}
