using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Earth;
//using Earth.UserControl.Country;


namespace Earth
{
    public partial class CountryUserControl : System.Windows.Controls.UserControl
    {
        public CountryUserControl()
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
                GetCountries getCountries = new GetCountries();
                List<CountryEntities> countriesList = getCountries.GetCountriesFromDatabase( out string returnResult, out int returnValue);
                dtgDataGrid.ItemsSource = countriesList;
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

                Earth.CountryPopup popupWindow = new Earth.CountryPopup();
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
                CountryEntities selectedEntity = (CountryEntities)dtgDataGrid.SelectedItem;
                Earth.CountryPopup popupWindow = new Earth.CountryPopup();
                popupWindow.Mode = "VIEW";
                popupWindow.selectedEntity = selectedEntity;
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
                CountryEntities selectedEntity = (CountryEntities)dtgDataGrid.SelectedItem;
                Earth.CountryPopup popupWindow = new Earth.CountryPopup();
                popupWindow.Mode = "EDIT";
                popupWindow.selectedEntity = selectedEntity;
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
                CountryEntities selectedEntity = (CountryEntities)dtgDataGrid.SelectedItem;
                Earth.CountryPopup popupWindow = new Earth.CountryPopup();
                popupWindow.Mode = "DELETE";
                popupWindow.selectedEntity = selectedEntity;
                popupWindow.ShowDialog();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();

        }
    }
}
