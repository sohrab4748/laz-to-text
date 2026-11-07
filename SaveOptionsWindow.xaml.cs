using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LAS_to_TEXT
{
    public partial class SaveOptionsWindow : Window
    {
        private Dictionary<string, string> columnMappings; // Store header-property mappings
        public SaveOptionsWindow(Dictionary<string, string> columns)
        {
            InitializeComponent();
            columnMappings = columns;
            PopulateColumns();
        }

        private void PopulateColumns()
        {
            lstColumns.ItemsSource = columnMappings.Keys; // Populate with display headers

            lstColumns.SelectionChanged += (s, e) => {
                foreach (var item in e.AddedItems)
                {
                    if (!lstColumnOrder.Items.Contains(item))
                    {
                        lstColumnOrder.Items.Add(item);
                    }
                }
            };
        }

        private void SaveWithOptions_Click(object sender, RoutedEventArgs e)
        {
            bool saveHeader = chkSaveHeader.IsChecked ?? false;
            List<string> selectedColumns = lstColumnOrder.Items.Cast<string>()
                .Select(header => columnMappings[header]) // Map headers to property names
                .ToList();

            this.DialogResult = true; // Close the window
        }

        private void lstColumnOrder_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string column = (string)e.Data.GetData(DataFormats.StringFormat);
                if (!lstColumnOrder.Items.Contains(column))
                {
                    lstColumnOrder.Items.Add(column);
                }
            }
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = lstColumnOrder.SelectedIndex;
            if (selectedIndex > 0)
            {
                object selectedItem = lstColumnOrder.SelectedItem; // Use object type
                lstColumnOrder.Items.RemoveAt(selectedIndex);
                lstColumnOrder.Items.Insert(selectedIndex - 1, selectedItem);
                lstColumnOrder.SelectedIndex = selectedIndex - 1;
            }
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = lstColumnOrder.SelectedIndex;
            if (selectedIndex < lstColumnOrder.Items.Count - 1 && selectedIndex >= 0) // Check for valid index
            {
                object selectedItem = lstColumnOrder.SelectedItem; // Use object type
                lstColumnOrder.Items.RemoveAt(selectedIndex);
                lstColumnOrder.Items.Insert(selectedIndex + 1, selectedItem);
                lstColumnOrder.SelectedIndex = selectedIndex + 1;
            }
        }

        private void lstColumns_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lstColumns.SelectedItem != null)
            {
                DragDrop.DoDragDrop(lstColumns, lstColumns.SelectedItem.ToString(), DragDropEffects.Copy);
            }
        }
    }
}