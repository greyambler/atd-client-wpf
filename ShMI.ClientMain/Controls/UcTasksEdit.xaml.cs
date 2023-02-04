using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Windows.Controls;
using System.Windows;
using System.Linq;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcTasksEdit.xaml
    /// </summary>
    public partial class UcTasksEdit : UserControl
    {
        public UcTasksEdit(ModuleUcTasks ModuleMain, Task_Device EditItem)
        {
            InitializeComponent();
            Module = ModuleMain;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcTasks Module
        {
            get => mainGrid.DataContext as ModuleUcTasks;
            set => mainGrid.DataContext = value;
        }

        private void UcTanks_Loaded(object sender, RoutedEventArgs e)
        {
            /////cbTypeTask.SelectedItem )
            ControllersVisible();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ControllersVisible();
            InitDelTable();
            #region old
            //if (sender is ComboBox _ComboBox)
            //{
            //    switch (_ComboBox.SelectedItem)
            //    {
            //        case "level":
            //        case "Struna":
            //            {
            //                g_Date_LastLine_Struna.Visibility = Visibility.Visible;
            //                g_Date_LastLine_Cassa.Visibility = Visibility.Collapsed;
            //                tb_CountDaySave.Visibility = Visibility.Visible;
            //                grig_PingCass.Visibility = Visibility.Collapsed;
            //                grig_DisableTables.Visibility = Visibility.Collapsed;
            //                g_Date_History_Windows.Visibility = Visibility.Collapsed;
            //                //uc_TimeSpan_Ping.Show_Grid_Sec = false;
            //                break;
            //            }
            //        case "cassa":
            //        case "Cassa":
            //            {
            //                g_Date_LastLine_Struna.Visibility = Visibility.Collapsed;
            //                g_Date_LastLine_Cassa.Visibility = Visibility.Visible;
            //                tb_CountDaySave.Visibility = Visibility.Visible;
            //                grig_PingCass.Visibility = Visibility.Collapsed;
            //                grig_DisableTables.Visibility = Visibility.Collapsed;
            //                g_Date_History_Windows.Visibility = Visibility.Collapsed;
            //                break;
            //            }
            //        case "zip":
            //            {
            //                g_Date_LastLine_Struna.Visibility = Visibility.Visible;
            //                g_Date_LastLine_Cassa.Visibility = Visibility.Visible;
            //                tb_CountDaySave.Visibility = Visibility.Collapsed;
            //                grig_PingCass.Visibility = Visibility.Collapsed;
            //                grig_DisableTables.Visibility = Visibility.Collapsed;
            //                g_Date_History_Windows.Visibility = Visibility.Visible;
            //                break;
            //            }
            //        case "auditarch":
            //            {
            //                g_Date_LastLine_Struna.Visibility = Visibility.Visible;
            //                g_Date_LastLine_Cassa.Visibility = Visibility.Collapsed;
            //                grig_PingCass.Visibility = Visibility.Collapsed;
            //                grig_DisableTables.Visibility = Visibility.Collapsed;
            //                g_Date_History_Windows.Visibility = Visibility.Collapsed;
            //                tb_CountDaySave.Visibility = Visibility.Visible;
            //                break;
            //            }
            //        case "th":
            //            {
            //                g_Date_LastLine_Struna.Visibility = Visibility.Collapsed;
            //                g_Date_LastLine_Cassa.Visibility = Visibility.Collapsed;
            //                tb_CountDaySave.Visibility = Visibility.Collapsed;
            //                grig_PingCass.Visibility = Visibility.Collapsed;
            //                grig_DisableTables.Visibility = Visibility.Visible;
            //                g_Date_History_Windows.Visibility = Visibility.Collapsed;
            //                break;
            //            }
            //        case "MSG_Water":
            //            {
            //                g_Date_LastLine_Struna.Visibility = Visibility.Collapsed;
            //                g_Date_LastLine_Cassa.Visibility = Visibility.Collapsed;
            //                tb_CountDaySave.Visibility = Visibility.Collapsed;
            //                grid_ZIP.Visibility = Visibility.Collapsed;
            //                grig_PingCass.Visibility = Visibility.Visible;
            //                grig_DisableTables.Visibility = Visibility.Collapsed;
            //                uc_TimeSpan_Task.Grid_M.Visibility = Visibility.Visible;
            //                uc_TimeSpan_Task.TB_M.Visibility = Visibility.Visible;
            //                g_Date_History_Windows.Visibility = Visibility.Collapsed;
            //                break;
            //            }
            //        case "MSG_Fuel":
            //            {
            //                g_Date_LastLine_Struna.Visibility = Visibility.Collapsed;
            //                g_Date_LastLine_Cassa.Visibility = Visibility.Collapsed;
            //                tb_CountDaySave.Visibility = Visibility.Collapsed;
            //                grid_ZIP.Visibility = Visibility.Collapsed;
            //                grig_PingCass.Visibility = Visibility.Visible;
            //                grig_DisableTables.Visibility = Visibility.Collapsed;
            //                uc_TimeSpan_Task.Grid_M.Visibility = Visibility.Visible;
            //                uc_TimeSpan_Task.TB_M.Visibility = Visibility.Visible;
            //                g_Date_History_Windows.Visibility = Visibility.Collapsed;
            //                break;
            //            }
            //        default:
            //            {
            //                g_Date_LastLine_Struna.Visibility = Visibility.Collapsed;
            //                g_Date_LastLine_Cassa.Visibility = Visibility.Collapsed;
            //                tb_CountDaySave.Visibility = Visibility.Visible;
            //                grig_PingCass.Visibility = Visibility.Collapsed;
            //                grig_DisableTables.Visibility = Visibility.Collapsed;
            //                g_Date_History_Windows.Visibility = Visibility.Collapsed;
            //                break;
            //            }
            //    }
            //}
            #endregion old
        }

        private void InitDelTable()
        {
            if (Module.CurrentItem != null && Module.CurrentItem.Status != null && Module.CurrentItem.Status != "")
            {
                string[] Mas = Module.CurrentItem.Status.Split(',');
                GetObj.IsChecked = (Mas.FirstOrDefault(s => s.Trim() == "GetObj") != null);
                GetFuel.IsChecked = (Mas.FirstOrDefault(s => s.Trim() == "GetFuel") != null);
                GetCashDesk.IsChecked = (Mas.FirstOrDefault(s => s.Trim() == "GetCashDesk") != null);
                GetTank.IsChecked = (Mas.FirstOrDefault(s => s.Trim() == "GetTank") != null);
            }
        }


        private void GetObj_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox comboBox)
            {

                if (Module.CurrentItem.Status != null && Module.CurrentItem.Status != "")
                {
                    string[] Mas = Module.CurrentItem.Status.Split(',');
                    if (Mas.FirstOrDefault(s => s.Trim() == comboBox.Name) == null)
                    {
                        Module.CurrentItem.Status = string.Format("{0}, {1}", Module.CurrentItem.Status, comboBox.Name);
                    }
                }
                else
                {
                    Module.CurrentItem.Status = comboBox.Name;
                }
            }
            Module.CurrentItem.MChangeProperty = "Status";
        }
        private void GetObj_Unchecked(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox comboBox)
            {
                if (Module.CurrentItem.Status != null && Module.CurrentItem.Status != "")
                {
                    string newStatus = "";
                    string[] Mas = Module.CurrentItem.Status.Split(',');
                    foreach (var item in Mas)
                    {
                        if (item.Trim() != comboBox.Name)
                        {
                            newStatus = (newStatus == "") ? item : string.Format("{0}, {1}", newStatus, item);
                        }
                    }
                    Module.CurrentItem.Status = newStatus;
                }
                else
                {
                    Module.CurrentItem.Status = comboBox.Name;
                }
            }
            Module.CurrentItem.MChangeProperty = "Status";
        }
        private void GetWorkDirectory(object sender, RoutedEventArgs e)
        {
            Module.Zip_Dir = Module.GetChooseDirectory("Директория куда будет перенесен архив", Module.CurrentItem.Zip_Dir);
        }
        private void ControllersVisible()//string selectTypeTask)
        {
            g_Name_Task.Visibility = Visibility.Visible;
            g_NObject.Visibility = Visibility.Visible;
            g_Type_Device.Visibility = Visibility.Visible;

            g_Zip_Dir.Visibility = Visibility.Collapsed;
            g_Date_LastLine_S.Visibility = Visibility.Collapsed;
            g_Date_LastLine_C.Visibility = Visibility.Collapsed;
            g_Date_History_W.Visibility = Visibility.Collapsed;
            g_CountDaySave.Visibility = Visibility.Collapsed;
            grig_PingCass.Visibility = Visibility.Collapsed;
            grig_DisableTables.Visibility = Visibility.Collapsed;


            switch (Module.CurrentItem?.Type_Device)//(selectTypeTask)
            {
                case "th":
                    {
                        grig_DisableTables.Visibility = Visibility.Visible;

                        break;
                    }
                case "zip":
                    {
                        g_Zip_Dir.Visibility = Visibility.Visible;
                        g_Date_LastLine_S.Visibility = Visibility.Visible;
                        g_Date_LastLine_C.Visibility = Visibility.Visible;
                        g_Date_History_W.Visibility = Visibility.Visible;

                        break;
                    }
                case "auditarch":
                    {
                        g_Date_LastLine_S.Visibility = Visibility.Visible;
                        g_CountDaySave.Visibility = Visibility.Visible;
                        break;
                    }
                case "level":
                    {
                        g_Date_LastLine_S.Visibility = Visibility.Visible;
                        g_CountDaySave.Visibility = Visibility.Visible;

                        break;
                    }
                case "cassa":
                    {
                        g_Date_LastLine_C.Visibility = Visibility.Visible;
                        g_CountDaySave.Visibility = Visibility.Visible;
                        break;
                    }
                case "MSG_Water":
                    {
                        grig_PingCass.Visibility = Visibility.Collapsed;
                        grig_PingCass.Visibility = Visibility.Visible;
                        break;
                    }
                case "MSG_Fuel":
                    {
                        grig_PingCass.Visibility = Visibility.Collapsed;
                        grig_PingCass.Visibility = Visibility.Visible;
                        break;
                    }
                default:
                    {
                        g_Name_Task.Visibility = Visibility.Visible;
                        g_NObject.Visibility = Visibility.Visible;
                        g_Type_Device.Visibility = Visibility.Visible;

                        g_Zip_Dir.Visibility = Visibility.Visible;
                        g_Date_LastLine_S.Visibility = Visibility.Visible;
                        g_Date_LastLine_C.Visibility = Visibility.Visible;
                        g_Date_History_W.Visibility = Visibility.Visible;
                        g_CountDaySave.Visibility = Visibility.Visible;
                        grig_PingCass.Visibility = Visibility.Visible;
                        grig_DisableTables.Visibility = Visibility.Visible;

                        break;
                    }
            }
        }
    }
}
