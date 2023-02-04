using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcCodesGoodsEdit.xaml
    /// </summary>
    public partial class UcCodesGoodsEdit : UserControl
    {
        //public UcCodesGoodsEdit(ModuleMainWindow ModuleMain, ReCodesTable EditItem)
        public UcCodesGoodsEdit(ModuleUcCodesGoods ModuleMain, ReCodesTable EditItem)
        {
            InitializeComponent();
            Module = ModuleMain;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcCodesGoods Module
        {
            get => mainGrid.DataContext as ModuleUcCodesGoods;
            set => mainGrid.DataContext = value;
        }
    }
}
