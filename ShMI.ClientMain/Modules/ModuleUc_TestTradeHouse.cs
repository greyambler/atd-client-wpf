using System;
using System.Windows.Controls;

namespace ShMI.ClientMain.Modules
{

    public class ModuleUc_TestTradeHouse : ModuleMainWindow
    {
        public ModuleUc_TestTradeHouse( ModuleMainWindow ModuleMain, UserControl uc )
        {
            this.ModuleMain = ModuleMain;
            this.ModuleMain.SetWidthListButton(uc);
        }
        public ModuleMainWindow ModuleMain { get; set; }

        public override void AddItem()
        {
            Console.WriteLine("ModuleUcTestTradeHouse public override void AddItem()");
            //_ = MessageBox.Show("ModuleUcTestTradeHouse");
        }

    }
}
