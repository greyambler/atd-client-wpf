using ShMI.ClientMain.Modules;
using System.Windows;
using System.Windows.Controls;

namespace ShMI.ClientMain.Interface
{
    public interface IMainWindow
    {
        Window ShellWindow { get; }
        Grid WorkGrid { get; }
        ResourceDictionary ResourcesDict { get; }
        void SetWidthListButton(UserControl _UserControl = null, bool visual = true);
        bool IsAdmin { get; set; }
        System.Windows.Threading.Dispatcher DispatcherShell { get; set; }
    }

    public interface IListButtonsService
    {
        void AddItem();
        void EditItem();
        void DeleteItem();
        void SaveItem();
        void CancelItem();
        void UtilItem();
        void Cancel();
    }

    public interface IUcChildren
    {
        UserControl UcListButton { get; }
    }

}
