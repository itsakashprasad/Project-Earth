using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Earth
{
    /// <summary>
    /// Interaction logic for CityUserControl.xaml
    /// </summary>
    public partial class CityUserControl : System.Windows.Controls.UserControl
    {
        public CityUserControl()
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
                GetCities getCities = new GetCities();
                List<CityEntities> CityList = getCities.GetCitiesFromDatabase( out string returnResult, out int returnValue);
                dtgDataGrid.ItemsSource = CityList;
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

                Earth.CityPopup popupWindow = new Earth.CityPopup("ADD");
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
            CityEntities selectedEntity = dtgDataGrid.SelectedItem as CityEntities;


            try
            {
                Earth.CityPopup popupWindow = new Earth.CityPopup("VIEW");
                popupWindow.SetSelectedEntity(selectedEntity);
                popupWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            CityEntities selectedEntity = dtgDataGrid.SelectedItem as CityEntities;
            try
            {

                Earth.CityPopup popupWindow = new Earth.CityPopup("EDIT");
                popupWindow.SetSelectedEntity(selectedEntity);
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
            CityEntities selectedEntity = dtgDataGrid.SelectedItem as CityEntities;
            try
            {

                Earth.CityPopup popupWindow = new Earth.CityPopup("DELETE");
                popupWindow.SetSelectedEntity(selectedEntity);
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
