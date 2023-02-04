﻿using ShMI.BaseMain;
using ShMI.ClientMain.Modules;
using System.Windows;
using System.Windows.Controls;

namespace ShMI.ClientMain.Controls
{
    /// <summary>
    /// Логика взаимодействия для UcLevelsMeterEdit.xaml
    /// </summary>
    public partial class UcLevelsMeterEdit : UserControl
    {
        public UcLevelsMeterEdit(ModuleUcLevelsMeter ModuleMain, NStruna EditItem)
        {
            InitializeComponent();
            Module = ModuleMain;
            Module.CurrentItem = EditItem;
        }

        public ModuleUcLevelsMeter Module
        {
            get => mainGrid.DataContext as ModuleUcLevelsMeter;
            set => mainGrid.DataContext = value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string SetFirstCatalog = "";
            if (Module != null && Module.CurrentItem != null)
            {
                SetFirstCatalog = Module.CurrentItem.NameBatFile;
            }
            Module.CurrentItem.NameBatFile = Module.GetChooseFile("Путь до BAT файла", SetFirstCatalog);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox _ComboBox)
            {
                switch (_ComboBox.SelectedItem)
                {
                    case "NEW_servise":
                        {
                            g_batFileLabel.Visibility = Visibility.Collapsed;
                            g_batFile.Visibility = Visibility.Collapsed;
                            l_IP.Visibility = Visibility.Visible;
                            break;
                        }
                    default:
                        {
                            g_batFileLabel.Visibility = Visibility.Visible;
                            g_batFile.Visibility = Visibility.Visible;
                            l_IP.Visibility = Visibility.Collapsed;
                            break;
                        }
                }
            }
        }

    }
}