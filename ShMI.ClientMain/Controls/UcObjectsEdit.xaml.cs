using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcObjectsEdit.xaml
    /// </summary>
    public partial class UcObjectsEdit : UserControl
    {
        public UcObjectsEdit(ModuleUcObjects ModuleMain, NObject EditItem)
        {
            InitializeComponent();
            Module = ModuleMain;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcObjects Module
        {
            get => DataContext as ModuleUcObjects;
            set => DataContext = value;
        }
    }
}
