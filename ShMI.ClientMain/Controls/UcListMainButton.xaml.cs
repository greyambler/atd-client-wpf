using ShMI.ClientMain.Modules;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcListMainButton.xaml
    /// </summary>
    public partial class UcListMainButton : UserControl
    {
        public UcListMainButton()
        {
            InitializeComponent();
        }

        public ModuleMainWindow Module
        {
            get => DataContext as ModuleMainWindow;
            set => DataContext = value;
        }

        private void ListButton_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Init();
            }
            catch { }
        }
        private void Init()
        {
            bool isFirst = Module.IsExistObject();
            if (isFirst)
            {
                IntinListButton();
            }
            else
            {
                IntinListButtonFirst();
            }
        }
        private void IntinListButtonFirst()
        {
            ResourceDictionary Resources = Module?.ResourcesDict;

            List<object> ListBtns = new List<object>
            {
                
                GetButton("Инициализация", Resources , (RoutedEventHandler)BtnFirstInit_Click),
                "",
                GetButton("Объект", Resources, (RoutedEventHandler)BtnObjects_Click),
#if _DEBUG
                GetButton("Test", Resources , (RoutedEventHandler)BtnTest_Click),
                GetButton("Задачи", Resources, (RoutedEventHandler)BtnTasks_Click),
                "",
#endif
                 "",
                GetButton("Выход", Resources, (RoutedEventHandler)BtnExit_Click),
            };
            listButton.ItemsSource = ListBtns;
        }
        public void IntinListButton()
        {
            ResourceDictionary Resources = Module?.ResourcesDict;

            List<object> ListBtns = new List<object>
            {
#if _DEBUG
                GetButton("Инициализация", Resources , (RoutedEventHandler)BtnFirstInit_Click),

                GetButton("Test", Resources , (RoutedEventHandler)BtnTest_Click),
                "",

#endif
                GetButton("TradeHouse", Resources, (RoutedEventHandler)BtnTH_Click),
                "",
                GetButton("Коды соответствия", Resources, (RoutedEventHandler)BtnCodesGoods_Click),
                GetButton("Объект", Resources, (RoutedEventHandler)BtnObjects_Click),
                GetButton("Кассы", Resources, (RoutedEventHandler)BtnCassa_Click),
                GetButton("Уровнемеры", Resources, (RoutedEventHandler)BtnLevelsMeter_Click),
                GetButton("Резервуары", Resources, (RoutedEventHandler)BtnTanks_Click),
                GetButton("Задачи", Resources, (RoutedEventHandler)BtnTasks_Click),
                "",
                GetButton("Сервис", Resources, (RoutedEventHandler)BtnService_Click),
            };

            if (Module.IsAdmin)
            {
                ListBtns.Add(GetButton("Импорт/Экспорт", Resources, (RoutedEventHandler)BtnImpExp_Click));
                ListBtns.Add("");
                ListBtns.Add(GetButton("Тест кассы", Resources, (RoutedEventHandler)BtnTestCassa_Click));
                ListBtns.Add(GetButton("Тест сообщения кассы", Resources, (RoutedEventHandler)BtnTestMessageCassa_Click));
                ListBtns.Add(GetButton("Тест TradeHouse", Resources, (RoutedEventHandler)BtnTestTH_Click));
                ListBtns.Add(GetButton("Тест струны", Resources, (RoutedEventHandler)BtnTestLevelsMeter_Click));
                ListBtns.Add(GetButton("Тест ZIP", Resources, (RoutedEventHandler)BtnTestZip_Click));
                ListBtns.Add(GetButton("Тест Auditarch", Resources, (RoutedEventHandler)BtnTestAuditarch_Click));
                ListBtns.Add("");
                ListBtns.Add(GetButton("Очистить ДБ", Resources, (RoutedEventHandler)BtnTestClearnDB_Click));
                ListBtns.Add("");
                ListBtns.Add(GetButton("Прочесть JW", Resources, (RoutedEventHandler)BtnTestReadJW_Click));

            }

            ListBtns.Add("");

            ListBtns.Add(GetButton("Выход", Resources, (RoutedEventHandler)BtnExit_Click));
            listButton.ItemsSource = ListBtns;
        }

        private TextBlock GetText()
        {
            TextBlock txb = new TextBlock()
            {
                Text = Module.IsAdmin.ToString(),
                Margin = new Thickness(0, 3, 0, 3),
            };
            return txb;
        }

        private Button GetButton(params object[] list)
        {
            Button btn = new Button()
            {
                Content = list[0],
                Tag = list[0],
                Margin = new Thickness(0, 3, 0, 3),
                Style = (Style)(list[1] as ResourceDictionary)["ButtonRound_Change"],

            };
            if (list.Length >= 3 && list[2] != null)
            {
                btn.Click += (RoutedEventHandler)list[2];
            }

            return btn;
        }

        private void BtnFirstInit_Click(object sender, RoutedEventArgs e)
        {
            //WpfTutorialSamples t = new WpfTutorialSamples();
            //t.Owner = Module.ShellWindow;
            //t.ShowDialog();

            Module.InitFirstDB();
            Init();
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            Module.SetWidthListButton(new Uc_Test(Module));

            //_ = new Uc_Test(ModuleMain);
        }

        #region /*********Рабочие***********/

        private void BtnTH_Click(object sender, RoutedEventArgs e)
        {
            Module.SetWidthListButton(new UcTH(Module));
        }
        private void BtnCodesGoods_Click(object sender, RoutedEventArgs e)
        {
            Module.SetWidthListButton(new UcCodesGoods(Module));
        }
        private void BtnObjects_Click(object sender, RoutedEventArgs e)
        {
            Module.SetWidthListButton(new UcObjects(Module));
        }
        private void BtnCassa_Click(object sender, RoutedEventArgs e)
        {
            Module.SetWidthListButton(new UcCassa(Module));
        }
        private void BtnLevelsMeter_Click(object sender, RoutedEventArgs e)
        {
            Module.SetWidthListButton(new UcLevelsMeter(Module));
        }
        private void BtnTanks_Click(object sender, RoutedEventArgs e)
        {
            Module.SetWidthListButton(new UcTanks(Module));
        }
        private void BtnTasks_Click(object sender, RoutedEventArgs e)
        {
            Module.SetWidthListButton(new UcTasks(Module));
        }
        private void BtnService_Click(object sender, RoutedEventArgs e)
        {
            Module.SetWidthListButton(new UcService(Module));
        }
        #endregion/*********Рабочие***********/

        #region/*********Тестовые***********/
        private void BtnImpExp_Click(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("BtnImpExp_Click");
        }
        private void BtnTestCassa_Click(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("BtnTestCassa_Click");
        }
        private void BtnTestMessageCassa_Click(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("BtnTestMessageCassa_Click");
        }
        private void BtnTestTH_Click(object sender, RoutedEventArgs e)
        {
            //_ = new UcTestTradeHouse(ModuleMain);
        }
        private void BtnTestLevelsMeter_Click(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("BtnTestLevelsMeter_Click");
        }
        private void BtnTestZip_Click(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("BtnTestZip_Click");
        }
        private void BtnTestAuditarch_Click(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("BtnTestAuditarch_Click");
        }
        private void BtnTestClearnDB_Click(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("BtnTestClearnDB_Click");
        }
        private void BtnTestReadJW_Click(object sender, RoutedEventArgs e)
        {
            _ = MessageBox.Show("BtnTestReadJW_Click");
        }
        #endregion/*********Тестовые***********/

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Module?.ShellWindow.Close();
        }

    }
}
