using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Earth;

namespace Earth
{
    /// <summary>
    /// Interaction logic for StudentUserControl.xaml
    /// </summary>
    public partial class StudentUserControl : System.Windows.Controls.UserControl
    {
        
        public StudentUserControl()
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
                GetStudent getStudent = new GetStudent();
                List<StudentEntities> StudentList = getStudent.GetStudentFromDatabase(out string returnResult, out int returnValue);
                dtgDataGrid.ItemsSource = StudentList;
            }
            catch (Exception ex)        
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void AddData_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Earth.UserControl.Student.StudentPopup popupWindow = new Earth.UserControl.Student.StudentPopup("ADD");
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
            StudentEntities SelectedEntity = dtgDataGrid.SelectedItem as StudentEntities;


            try
            {
                Earth.UserControl.Student.StudentPopup popupWindow = new Earth.UserControl.Student.StudentPopup("VIEW");
                popupWindow.SetSelectedEntity(SelectedEntity);
                popupWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }


        }


        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            StudentEntities selectedEntity = dtgDataGrid.SelectedItem as StudentEntities;
            try
            {

                Earth.UserControl.Student.StudentPopup popupWindow = new Earth.UserControl.Student.StudentPopup("EDIT");
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
            StudentEntities selectedEntity = dtgDataGrid.SelectedItem as StudentEntities;
            try
            {

                Earth.UserControl.Student.StudentPopup popupWindow = new Earth.UserControl.Student.StudentPopup("DELETE");
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
