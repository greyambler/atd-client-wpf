using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShMI.ClientMain.MainWindowControls
{
    public partial class Uc_TimeHMSF : UserControl
    {
        public Uc_TimeHMSF()
        {
            InitializeComponent();
        }
        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Module_Sec.List_Number = new List<TextBlock>
            {
                //Module_Sec.List_Number.Add(this.t_D);
                t_H,
                t_M,
                t_S,
                t_MiS
            };
            Module_Sec.Millisecond = Module_Sec.InputDateTime.Millisecond;
        }

        public DateTime Int_DateTime
        {
            get => (DateTime)GetValue(Int_DateTimeProperty);
            set => SetValue(Int_DateTimeProperty, value);
        }
        public static readonly DependencyProperty Int_DateTimeProperty =
            DependencyProperty.Register(
            "Int_DateTime",
            typeof(DateTime),
            typeof(Uc_TimeHMSF),
            new FrameworkPropertyMetadata(Int_DateTimeChanged)
            );

        private static void Int_DateTimeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                Uc_TimeHMSF t = obj as Uc_TimeHMSF;
                if (t.ThisNotNull())
                {
                    t.Int_DateTime = t.Module_Sec.InputDateTime = (DateTime)args.NewValue;

                    //long tik = (int)args.NewValue;
                    //tik *= 10000000;
                    //t.Module_Sec.timeSpan = new TimeSpan(tik);
                }

            }
            catch (Exception)
            {
            }
        }

        public Module_TimeHMSF Module_Sec
        {
            get => MainGrid.DataContext as Module_TimeHMSF;
            set => MainGrid.DataContext = value;
        }

        private void Btn_M_Click(object sender, RoutedEventArgs e)
        {
            if (!Module_Sec.Seletc_Number.ThisNotNull())
            {
                Module_Sec.Seletc_Number = Module_Sec.List_Number[0];
            }
            if (Module_Sec.Seletc_Number.ThisNotNull())
            {
                switch (Module_Sec.Seletc_Number.Name)
                {
                    case "t_H":
                        { Module_Sec.InputDateTime = (Module_Sec.InputDateTime != new DateTime()) ? Module_Sec.InputDateTime.AddHours(-1) : new DateTime(); break; }
                    case "t_M":
                        { Module_Sec.InputDateTime = (Module_Sec.InputDateTime != new DateTime()) ? Module_Sec.InputDateTime.AddMinutes(-1) : new DateTime(); break; }
                    case "t_S":
                        { Module_Sec.InputDateTime = (Module_Sec.InputDateTime != new DateTime()) ? Module_Sec.InputDateTime.AddSeconds(-1) : new DateTime(); break; }
                    case "t_MiS":
                        { Module_Sec.InputDateTime = (Module_Sec.InputDateTime != new DateTime()) ? Module_Sec.InputDateTime.AddMilliseconds(-10) : new DateTime(); break; }

                    default:
                        break;
                }
                Int_DateTime = Module_Sec.InputDateTime;
            }
        }

        private void Btn_P_Click(object sender, RoutedEventArgs e)
        {
            if (!Module_Sec.Seletc_Number.ThisNotNull())
            {
                Module_Sec.Seletc_Number = Module_Sec.List_Number[0];
            }
            if (Module_Sec.Seletc_Number.ThisNotNull())
            {
                switch (Module_Sec.Seletc_Number.Name)
                {
                    case "t_H":
                        { Module_Sec.InputDateTime = Module_Sec.InputDateTime.AddHours(1); break; }
                    case "t_M":
                        { Module_Sec.InputDateTime = Module_Sec.InputDateTime.AddMinutes(1); break; }
                    case "t_S":
                        { Module_Sec.InputDateTime = Module_Sec.InputDateTime.AddSeconds(1); break; }
                    case "t_MiS":
                        { Module_Sec.InputDateTime = Module_Sec.InputDateTime.AddMilliseconds(10); break; }

                    default:
                        break;
                }
                Int_DateTime = Module_Sec.InputDateTime;
            }
        }

        private void UcMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            if (tb.ThisNotNull())
            {
                Module_Sec.Seletc_Number = tb;
            }
        }

        private void Btn_DelAll_Click(object sender, RoutedEventArgs e)
        {
            Int_DateTime = (Int_DateTime != new DateTime())
                ? Int_DateTime = new DateTime()
                : Int_DateTime = DateTime.Now;
        }

        private void DatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DateTime.TryParse(sender.ToString(), out DateTime dt) && dt != new DateTime() && Int_DateTime != Module_Sec.InputDateTime)
                {
                    Int_DateTime =
                        Module_Sec.InputDateTime = new DateTime(dt.Year, dt.Month, dt.Day, Int_DateTime.Hour, Int_DateTime.Minute, Int_DateTime.Second, Int_DateTime.Millisecond);
                }
            }
            catch (Exception)
            {
            }
        }
    }

    public class Module_TimeHMSF : System.ComponentModel.INotifyPropertyChanged
    {
        public Module_TimeHMSF()
        {

        }

        private List<TextBlock> _List_Number;
        public List<TextBlock> List_Number
        {
            get => _List_Number;
            set
            {
                _List_Number = value;
                MChangeProperty = "List_Number";
            }
        }

        private TextBlock _Seletc_Number;
        public TextBlock Seletc_Number
        {
            get => _Seletc_Number;
            set
            {
                _Seletc_Number = value;
                foreach (TextBlock tb in List_Number)
                {
                    tb.Background = null;
                }
                if (_Seletc_Number.ThisNotNull())
                {
                    _Seletc_Number.Background = new SolidColorBrush(Colors.Aqua);
                }
                MChangeProperty = "Seletc_Number";
            }
        }

        private DateTime _InputDateTime;
        public DateTime InputDateTime
        {
            get => _InputDateTime;
            set
            {
                _InputDateTime = value;
                MChangeProperty = "InputDateTime";


                MChangeProperty = "Day";
                MChangeProperty = "Hour";
                MChangeProperty = "Minute";
                MChangeProperty = "Second";
                MChangeProperty = "Millisecond";
            }
        }

        private int _InputSeconds;
        public int InputSeconds
        {
            get => _InputSeconds;
            set
            {
                _InputSeconds = value;
                MChangeProperty = "InputSeconds";
            }
        }

        public int Day
        {
            get => InputDateTime.Day;
            set => MChangeProperty = "Day";
        }
        public int Hour
        {
            get => InputDateTime.Hour;
            set => MChangeProperty = "Hour";
        }
        public int Minute
        {
            get => InputDateTime.Minute;
            set => MChangeProperty = "Minute";
        }
        public int Second
        {
            get => InputDateTime.Second;
            set => MChangeProperty = "Second";
        }
        public int Millisecond
        {
            get => InputDateTime.Millisecond;
            set => MChangeProperty = "Millisecond";
        }

        #region INotifyPropertyChanged Members
        public string MChangeProperty
        {
            set
            {
                try
                {
                    NotifyPropertyChanged(value);
                }
                catch (Exception)
                {
                }
            }
        }
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
