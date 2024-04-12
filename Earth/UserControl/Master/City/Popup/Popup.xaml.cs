using System;
using System.Windows;
using System.Windows.Documents;
using Earth;

namespace Earth
{
    public partial class Popup : Window
    {

        public string Mode { get; set; }
        public string Guid { get; set; }

        public bool Valid = false;

        public string NavBar;

        public Popup(string mode,string Nav)
        {
            InitializeComponent();
            Mode = mode;
            IsValidCheckBox.Checked += IsValidCheckBox_Checked;
            IsValidCheckBox.Unchecked += IsValidCheckBox_Unchecked;
            NavBar = Nav;
            if (mode == "ADD")
            {
                if(NavBar == "Student")
                {
                    txtPopupTitle.Text = "Add Student";
                    BtnDelete.IsEnabled = false;
                    BtnDelete.Visibility = Visibility.Hidden;
                    BtnSave.IsEnabled = false;
                    BtnSave.Visibility = Visibility.Hidden;
                }
                else if(NavBar =="Country")
                {
                    txtPopupTitle.Text = "Add Country";
                    BtnDelete.IsEnabled = false;
                    BtnDelete.Visibility = Visibility.Hidden;
                    BtnSave.IsEnabled = false;
                    BtnSave.Visibility = Visibility.Hidden;
                }
                

            }
            else if (mode == "VIEW")
            {
                if (NavBar == "Student")
                {
                    txtPopupTitle.Text = "View Student";
                    BtnCancel.Content = "OK";
                    BtnCancel.HorizontalAlignment = HorizontalAlignment.Center;
                    txtFirstInputBox.IsReadOnly = true;
                    txtSecondInputBox.IsReadOnly = true;
                    BtnDelete.IsEnabled = false;
                    BtnDelete.Visibility = Visibility.Collapsed;
                    BtnSave.IsEnabled = false;
                    BtnSave.Visibility = Visibility.Hidden;
                    BtnAdd.IsEnabled = false;
                    BtnAdd.Visibility = Visibility.Hidden;
                    IsValidCheckBox.IsChecked = true;
                    IsValidCheckBox.IsEnabled = false;
                }
                else if (NavBar == "Country")
                {
                    txtPopupTitle.Text = "View Country";
                    BtnCancel.Content = "OK";
                    BtnCancel.HorizontalAlignment = HorizontalAlignment.Center;
                    txtFirstInputBox.IsReadOnly = true;
                    txtSecondInputBox.IsReadOnly = true;
                    BtnDelete.IsEnabled = false;
                    BtnDelete.Visibility = Visibility.Collapsed;
                    BtnSave.IsEnabled = false;
                    BtnSave.Visibility = Visibility.Hidden;
                    BtnAdd.IsEnabled = false;
                    BtnAdd.Visibility = Visibility.Hidden;
                    IsValidCheckBox.IsChecked = true;
                    IsValidCheckBox.IsEnabled = false;
                }                   
                
            }
            else if (mode == "EDIT")
            {
                if (NavBar == "Student")
                {
                    txtPopupTitle.Text = "Edit Student";
                    BtnAdd.IsEnabled = false;
                    BtnAdd.Visibility = Visibility.Hidden;
                    BtnDelete.IsEnabled = false;
                    BtnDelete.Visibility = Visibility.Collapsed;

                }
                else if (NavBar == "Country")
                {
                    txtPopupTitle.Text = "Edit Country";
                    BtnAdd.IsEnabled = false;
                    BtnAdd.Visibility = Visibility.Hidden;
                    BtnDelete.IsEnabled = false;
                    BtnDelete.Visibility = Visibility.Collapsed;

                }



            }
            else
            {
                if (NavBar == "Student")
                {
                    txtPopupTitle.Text = "Delete Student";
                    BtnSave.IsEnabled = false;
                    BtnSave.Visibility = Visibility.Hidden;
                    BtnAdd.IsEnabled = false;
                    BtnAdd.Visibility = Visibility.Hidden;
                    IsValidCheckBox.IsChecked = true;
                    IsValidCheckBox.IsEnabled = false;
                    txtFirstInputBox.IsReadOnly = true;
                    txtSecondInputBox.IsReadOnly = true;

                }
                else if (NavBar == "Country")
                {
                    txtPopupTitle.Text = "Delete Country";
                    BtnSave.IsEnabled = false;
                    BtnSave.Visibility = Visibility.Hidden;
                    BtnAdd.IsEnabled = false;
                    BtnAdd.Visibility = Visibility.Hidden;
                    IsValidCheckBox.IsChecked = true;
                    IsValidCheckBox.IsEnabled = false;
                    txtFirstInputBox.IsReadOnly = true;
                    txtSecondInputBox.IsReadOnly = true;
                }    
            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------
        //View Control
        public void SetSelectedEntity(CountryEntities selectedEntity)
        {
           

            Guid = selectedEntity.Guid;
            txtFirstInputBox.Text = selectedEntity.Name;
            txtSecondInputBox.Text = selectedEntity.CountryCode;

        }

        public void SetSelectedEntity(StudentEntities selectedEntity)
        {
            

            Guid = selectedEntity.Guid;
            txtFirstInputBox.Text = selectedEntity.Name;
            txtSecondInputBox.Text = selectedEntity.Age.ToString();
            //if (selectedEntity.IsValid = true)
            //{
            //    IsValidCheckBox.Content = IsValidCheckBox.IsChecked;
            //}
            //else
            //{
            //    IsValidCheckBox.Content = IsValidCheckBox.Unchecked;


            //}


        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (NavBar == "Student")
            {
                try
                {
                    if (ValidateInputs())
                    {
                        StudentEntities Student = new StudentEntities
                        {

                            Name = txtFirstInputBox.Text,
                            Age = Convert.ToInt32(txtSecondInputBox.Text),
                            IsValid = Convert.ToBoolean(IsValidCheckBox.IsChecked) ? true : false,
                            AddBy = "AP",
                            AddDate = DateTime.Now
                        };
                        InsertStudent insertstudent = new InsertStudent();
                        insertstudent.ExecuteInsertStudent(Student);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else if (NavBar == "Country")
            {

            }
            

        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("Are You Sure ?", "Warning!!", MessageBoxButton.YesNo);

            if (Result == MessageBoxResult.Yes)
            {
                if(NavBar == "Student")
                {

                    try
                    {
                        StudentEntities Student = new StudentEntities
                        {
                            Guid = Guid,
                            IsDeleted = true,
                        };
                        DeleteStudent deletestudent = new DeleteStudent();
                        deletestudent.ExecuteDeleteStudent(Student);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    finally
                    {
                        this.Close();
                    }

                }
                else if(NavBar == "Country")
                {

                }
                


            }

        }
        //EDIT
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (NavBar == "Student")
            {
            }
            else if (NavBar == "Country")
            {

            }
            


        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IsValidCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
            BtnAdd.IsEnabled = true;
            
        }

        private void IsValidCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            
            BtnAdd.IsEnabled = false;
           

        }

        private bool ValidateInputs()
        {
            string Error = "";
            if (string.IsNullOrEmpty(txtFirstInputBox.Text))
            {
                Error = Error + "Please Enter Country Name.\n";
            }
            if (string.IsNullOrEmpty(txtSecondInputBox.Text))
            {
                Error = Error + "Please Enter Country code.\n";
            }

            if (Error != "")
            {
                MessageBox.Show(Error);
                return false;
            }
            return true;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

