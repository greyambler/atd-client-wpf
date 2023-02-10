using ShMI.BaseMain;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ShMI.ClientMain.Modules
{
    public class ModuleUcStart : ModuleMainWindow
    {
        public ModuleUcStart( Window ShellWindow, Grid WorkGrid, ResourceDictionary ResourcesDict, bool IsAdmin, Dispatcher DispatcherCore )
            : base(ShellWindow, WorkGrid, ResourcesDict, IsAdmin, DispatcherCore)
        {
        }

        public ModuleMainWindow ModuleMain { get; set; }
    }
}
