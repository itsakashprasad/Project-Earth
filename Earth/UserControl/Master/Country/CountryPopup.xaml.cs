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
using System.Windows.Shapes;

namespace Earth
{
    /// <summary>
    /// Interaction logic for CountryPopup.xaml
    /// </summary>
    public partial class CountryPopup : Window
    {
        public string Mode { get; set; }
        public string CapturedGuid = "";
        public CountryEntities selectedEntity { get; set; }
        public CountryPopup()
        {
            InitializeComponent();


        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------
        //View Control
        public void SetSelectedEntity(CountryEntities selectedEntity)
        {


            CapturedGuid = selectedEntity.Guid;
            txtFirstInputBox.Text = selectedEntity.Name;
            txtSecondInputBox.Text = selectedEntity.CountryCode;
            if (selectedEntity.IsValid)
            {
                IsValidCheckBox.IsChecked = true;
            }
            else
            {
                IsValidCheckBox.IsChecked = false;

            }

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs() && Mode == "ADD")
                {
                    CountryEntities Country = new CountryEntities
                    {
                        Mode = "ADD",
                        Guid = new Guid().ToString(),
                        Name = txtFirstInputBox.Text,
                        CountryCode = txtSecondInputBox.Text,
                        IsValid = Convert.ToBoolean(IsValidCheckBox.IsChecked) ? true : false,
                        AddBy = "AP",
                        AddDate = DateTime.Now
                    };
                    UpsertCountry upsertCountry = new UpsertCountry();
                    upsertCountry.ExecuteUpsertCountry(Country);
                }


                else if (ValidateInputs() && Mode == "DELETE")
                {
                    var Result = MessageBox.Show("Are You Sure ?", "Warning!!", MessageBoxButton.YesNo);

                    if (Result == MessageBoxResult.Yes)
                    {

                        try
                        {
                            CountryEntities Country = new CountryEntities
                            {
                                Mode = Mode,
                                Guid = CapturedGuid,
                                IsDeleted = true,
                            };
                            UpsertCountry upsertCountry = new UpsertCountry();
                            upsertCountry.ExecuteUpsertCountry(Country);
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
                }
                else if (ValidateInputs() && Mode == "VIEW")
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }



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

        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            IsValidCheckBox.Checked += IsValidCheckBox_Checked;
            IsValidCheckBox.Unchecked += IsValidCheckBox_Unchecked;

            if (Mode == "ADD")
            {

                txtPopupTitle.Text = "ADD COUNTRY";
                IsValidCheckBox.IsChecked = true;
                BtnEdit.IsEnabled = false;
                BtnEdit.Visibility = Visibility.Hidden;


            }
            else if (Mode == "VIEW")
            {
                txtFirstInputBox.Text = selectedEntity.Name;
                txtSecondInputBox.Text = selectedEntity.CountryCode;
                IsValidCheckBox.IsChecked = true;
                txtPopupTitle.Text = "View Country";
                BtnAdd.Content = "OK";
                txtFirstInputBox.IsReadOnly = true;
                txtSecondInputBox.IsReadOnly = true;
                IsValidCheckBox.IsEnabled = false;
                BtnEdit.IsEnabled = false;
                BtnEdit.Visibility = Visibility.Hidden;


            }
            else if (Mode == "EDIT")
            {
                txtFirstInputBox.Text = selectedEntity.Name;
                txtSecondInputBox.Text = selectedEntity.CountryCode;
                txtPopupTitle.Text = "Edit Country";
                
                BtnAdd.IsEnabled = false;
                BtnAdd.Visibility = Visibility.Hidden;
                if(selectedEntity.IsValid)
                {
                    IsValidCheckBox.IsChecked = true;
                }
                else
                {
                    IsValidCheckBox.IsChecked = false;

                }
            }
            else if (Mode == "DELETE")
            {

                txtFirstInputBox.Text = selectedEntity.Name;
                txtSecondInputBox.Text = selectedEntity.CountryCode;
                IsValidCheckBox.IsChecked = true;
                txtPopupTitle.Text = "Delete Country";
                BtnAdd.Content = "Delete";
                BtnAdd.IsEnabled = false;
                IsValidCheckBox.IsEnabled = false;
                txtFirstInputBox.IsReadOnly = true;
                txtSecondInputBox.IsReadOnly = true;
                BtnEdit.IsEnabled = false;
                BtnEdit.Visibility = Visibility.Hidden;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    CountryEntities Country = new CountryEntities
                    {
                        Mode = Mode,
                        Guid = CapturedGuid,
                        Name = txtFirstInputBox.Text,
                        CountryCode = txtSecondInputBox.Text,
                        IsValid = (bool)IsValidCheckBox.IsChecked ? true : false,

                    };
                    UpsertCountry upsertCountry = new UpsertCountry();
                    upsertCountry.ExecuteUpsertCountry(Country);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
