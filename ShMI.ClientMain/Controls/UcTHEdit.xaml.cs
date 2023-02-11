using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Linq;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcTHEdit.xaml
    /// </summary>
    public partial class UcTHEdit : UserControl
    {
        public UcTHEdit( ModuleUcTH ModuleMain, NCassa EditItem, bool isEdit )
        {
            InitializeComponent();
            Module = ModuleMain;
            Module.CurrentItem = EditItem;
            if (isEdit)
            {
                Module.CurrentNObject = Module.ListNObject.FirstOrDefault(s => s.Id == EditItem.NObjectId);
                cb_NObect.IsEnabled = !isEdit;
            }
        }

        public ModuleUcTH Module
        {
            get => DataContext as ModuleUcTH;
            set => DataContext = value;
        }
    }
}
