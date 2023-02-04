using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShMI.ClientMain.MainWindowControls
{
    /// <summary>
    /// Логика взаимодействия для uc_TimeSpan.xaml
    /// </summary>
    public partial class Uc_TimeSpan : UserControl
    {
        public Uc_TimeSpan()
        {
            InitializeComponent();
        }
        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Module_Sec.List_Number = new List<TextBlock>
            {
                t_D,
                t_H,
                t_M,
                t_S
            };
        }
        public Module_TimeSpan Module
        {
            get => MainGrid.DataContext as Module_TimeSpan;
            set => MainGrid.DataContext = value;
        }

        public int IntSeconds
        {
            get => (int)GetValue(IntSecondsProperty);
            set => SetValue(IntSecondsProperty, value);
        }
        public bool Show_Grid_Sec
        {
            get => Grid_Sec.Visibility == Visibility.Visible;
            set => Grid_Sec.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public static readonly DependencyProperty IntSecondsProperty =
            DependencyProperty.Register(
            "IntSeconds",
            typeof(int),
            typeof(Uc_TimeSpan),
            new FrameworkPropertyMetadata(IntSecondsChanged)
            );

        private static void IntSecondsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            try
            {
                Uc_TimeSpan t = (obj as Uc_TimeSpan);
                if (t.ThisNotNull())
                {
                    t.Module_Sec.InputSeconds = (int)args.NewValue;
                    long tik = (int)args.NewValue;
                    tik *= 10000000;
                    t.Module_Sec.UcTimeSpan = new TimeSpan(tik);
                }
            }
            catch (Exception) { }
        }

        public Module_TimeSpan Module_Sec
        {
            get => MainGrid.DataContext as Module_TimeSpan;
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
                    #region "t_D"
                    case "t_D":
                        {
                            Module_Sec.UcTimeSpan = Module_Sec.Days == 0
                                ? new TimeSpan(2 * 24 + Module_Sec.Hours, Module_Sec.Minutes, Module_Sec.Seconds)
                                : Module_Sec.UcTimeSpan - new TimeSpan(24, 0, 0);
                            break;
                        }
                    #endregion "t_D"
                    #region "t_H"
                    case "t_H":
                        {
                            Module_Sec.UcTimeSpan = Module_Sec.Hours == 0
                                ? new TimeSpan(Module_Sec.Days * 24 + 23, Module_Sec.Minutes, Module_Sec.Seconds)
                                : Module_Sec.UcTimeSpan - new TimeSpan(1, 0, 0);
                            break;
                        }
                    #endregion "t_H"
                    #region "t_M"
                    case "t_M":
                        {
                            Module_Sec.UcTimeSpan = Module_Sec.Minutes == 0
                                ? new TimeSpan(Module_Sec.Days * 24 + Module_Sec.Hours, 59, Module_Sec.Seconds)
                                : Module_Sec.UcTimeSpan - new TimeSpan(0, 1, 0);
                            break;
                        }
                    #endregion "t_M"
                    #region "t_S"
                    case "t_S":
                        {
                            Module_Sec.UcTimeSpan = Module_Sec.Seconds == 0
                                ? new TimeSpan(Module_Sec.Days * 24 + Module_Sec.Hours, Module_Sec.Minutes, 59)
                                : Module_Sec.UcTimeSpan - new TimeSpan(0, 0, 1);
                            break;
                        }

                    default:
                        break;
                        #endregion "t_S"
                }
                IntSeconds = Module_Sec.InputSeconds;
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
                    #region "t_D"
                    case "t_D":
                        {
                            Module_Sec.UcTimeSpan = Module_Sec.Days == 2
                                ? new TimeSpan(Module_Sec.Hours, Module_Sec.Minutes, Module_Sec.Seconds)
                                : Module_Sec.UcTimeSpan + new TimeSpan(24, 0, 0);
                            break;
                        }
                    #endregion "t_D"
                    #region "t_H"
                    case "t_H":
                        {
                            Module_Sec.UcTimeSpan = Module_Sec.Hours == 23
                                ? new TimeSpan(Module_Sec.Days * 24, Module_Sec.Minutes, Module_Sec.Seconds)
                                : Module_Sec.UcTimeSpan + new TimeSpan(1, 0, 0);
                            break;
                        }
                    #endregion "t_H"
                    #region "t_M"
                    case "t_M":
                        {
                            Module_Sec.UcTimeSpan = Module_Sec.Minutes == 59
                                ? new TimeSpan(Module_Sec.Days * 24 + Module_Sec.Hours, 0, Module_Sec.Seconds)
                                : Module_Sec.UcTimeSpan + new TimeSpan(0, 1, 0);
                            break;
                        }
                    #endregion "t_M"
                    #region "t_S"
                    case "t_S":
                        {
                            Module_Sec.UcTimeSpan = Module_Sec.Seconds == 59
                                ? new TimeSpan(Module_Sec.Days * 24 + Module_Sec.Hours, Module_Sec.Minutes, 0)
                                : Module_Sec.UcTimeSpan + new TimeSpan(0, 0, 1);
                            break;
                        }

                    default:
                        break;
                        #endregion "t_S"

                }
                IntSeconds = Module_Sec.InputSeconds;
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
    }
    public class Module_TimeSpan : System.ComponentModel.INotifyPropertyChanged
    {
        public Module_TimeSpan()
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

        private TimeSpan _timeSpan;
        public TimeSpan UcTimeSpan
        {
            get => _timeSpan;
            set
            {
                _timeSpan = value;
                MChangeProperty = "UcTimeSpan";
                MChangeProperty = "Days";
                MChangeProperty = "Hours";
                MChangeProperty = "Minutes";
                MChangeProperty = "Seconds";
                InputSeconds = (int)(_timeSpan.Ticks / 10000000);
            }
        }
        public int Days
        {
            get => UcTimeSpan.Days;
            set => MChangeProperty = "Days";
        }
        public int Hours
        {
            get => UcTimeSpan.Hours;
            set => MChangeProperty = "Hours";
        }
        public int Minutes
        {
            get => UcTimeSpan.Minutes;
            set => MChangeProperty = "Minutes";
        }
        public int Seconds
        {
            get => UcTimeSpan.Seconds;
            set => MChangeProperty = "Seconds";
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
