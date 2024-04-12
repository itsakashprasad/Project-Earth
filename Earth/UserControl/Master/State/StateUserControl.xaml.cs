using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Earth;


namespace Earth
{
    public partial class StateUserControl : System.Windows.Controls.UserControl
    {
        public StateUserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                GetStates getStates = new GetStates();
                List<StateEntities> StatesList = getStates.GetStatesFromDatabase("SELECT", out string returnResult, out int returnValue);
                dtgDataGrid.ItemsSource = StatesList;
            }
            catch (Exception)
            {
                throw;
            }
        }



        private void AddData_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                Earth.StatePopup popupWindow = new Earth.StatePopup();
                popupWindow.Mode = "ADD";
                popupWindow.ShowDialog();
                LoadData(); 

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StateEntities selectedEntity = (StateEntities)dtgDataGrid.SelectedItem;
                Earth.StatePopup popupWindow = new Earth.StatePopup();
                popupWindow.Mode = "VIEW";
                popupWindow.SelectedEntity = selectedEntity;
                 
                popupWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                StateEntities selectedEntity = (StateEntities)dtgDataGrid.SelectedItem;
                Earth.StatePopup popupWindow = new Earth.StatePopup();
                popupWindow.Mode = "EDIT";
                popupWindow.SelectedEntity = selectedEntity;
                popupWindow.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }


        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StateEntities selectedEntity = (StateEntities)dtgDataGrid.SelectedItem;
                Earth.StatePopup popupWindow = new Earth.StatePopup();
                popupWindow.Mode = "DELETE";
                popupWindow.SelectedEntity = selectedEntity;
                popupWindow.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }
    }
}
