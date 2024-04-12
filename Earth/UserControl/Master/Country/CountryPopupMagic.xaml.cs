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
    /// Interaction logic for CountryPopupMagic.xaml
    /// </summary>
    public partial class UserPopup : System.Windows.Controls.UserControl
    {
        public string Mode { get; set; }
        //public string Guid { get; set; }
        public CountryEntities SelectedEntity { get; set; }
        public UserPopup()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs() && Mode == "ADD")
                {
                    CountryEntities Country = new CountryEntities
                    {
                        Mode = Mode,
                        Guid = new Guid().ToString(),
                        Name = txtName.Text,
                        CountryCode = txtCountryCode.Text,
                        IsValid = Convert.ToBoolean(IsValidCheckBox.IsChecked) ? true : false,
                        AddBy = "AP",
                        AddDate = DateTime.Now
                    };
                    UpsertCountry upsertCountry = new UpsertCountry();
                    upsertCountry.ExecuteUpsertCountry(Country);
                }


                else if (ValidateInputs() && Mode == "DELETE")
                {
                    bool Result = WarningMessage.ShowWaningMessage("Delete", "Are You Sure ?", 200, 300, Brushes.Red);

                    if (Result)
                    {

                        try
                        {
                            CountryEntities Country = new CountryEntities
                            {
                                Mode = Mode,
                                Guid = SelectedEntity.Guid,
                                IsDeleted = true,
                            };
                            UpsertCountry upsertCountry = new UpsertCountry();
                            upsertCountry.ExecuteUpsertCountry(Country);
                            Window.GetWindow(this).Close();
                        }
                        catch (Exception ex)
                        {
                            MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
                        }
                       
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    CountryEntities Country = new CountryEntities
                    {
                        Mode = Mode,
                        Guid = SelectedEntity.Guid,
                        Name = txtName.Text,
                        CountryCode = txtCountryCode.Text,
                        IsValid = (bool)IsValidCheckBox.IsChecked,

                    };
                    UpsertCountry upsertCountry = new UpsertCountry();
                    upsertCountry.ExecuteUpsertCountry(Country);
                }

            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
        private void IsValidCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            btnAdd.IsEnabled = true;

        }

        private void IsValidCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            btnAdd.IsEnabled = false;


        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            IsValidCheckBox.Checked += IsValidCheckBox_Checked;
            IsValidCheckBox.Unchecked += IsValidCheckBox_Unchecked;

            if (Mode == "ADD")
            {


                txtPopupTitle.Text = "ADD COUNTRY";
                btnEdit.IsEnabled = false;
                btnEdit.Visibility = Visibility.Collapsed;
                if (SelectedEntity.IsValid)
                {
                    IsValidCheckBox.IsChecked = true;
                }
                else
                {
                    IsValidCheckBox.IsChecked = false;

                }


            }
            else if (Mode == "VIEW")
            {
                
                txtPopupTitle.Text = "VIEW COUNTRY";
                txtName.Text = SelectedEntity.Name;
                txtName.IsReadOnly = true;
                txtCountryCode.Text = SelectedEntity.CountryCode;                
                txtCountryCode.IsReadOnly = true;
                btnAdd.IsEnabled = false;
                btnAdd.Visibility = Visibility.Collapsed;
                btnEdit.IsEnabled = false;
                btnEdit.Visibility = Visibility.Collapsed;
                if (SelectedEntity.IsValid)
                {
                    IsValidCheckBox.IsChecked = true;
                }
                else
                {
                    IsValidCheckBox.IsChecked = false;

                }
                IsValidCheckBox.IsEnabled = false;
                


            }
            else if (Mode == "EDIT")
            {



                txtName.Text = SelectedEntity.Name;
                txtCountryCode.Text = SelectedEntity.CountryCode;
                txtPopupTitle.Text = "EDIT COUNTRY";
                btnAdd.IsEnabled = false;
                btnAdd.Visibility = Visibility.Collapsed;
                if (SelectedEntity.IsValid)
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
               
                txtName.Text = SelectedEntity.Name;
                txtCountryCode.Text = SelectedEntity.CountryCode;
                txtPopupTitle.Text = "DELETE COUNTRY";
                btnEdit.IsEnabled = false;
                btnEdit.Visibility = Visibility.Collapsed;
                btnAdd.IsEnabled = true;
                btnAdd.Content = "DELETE";
                IsValidCheckBox.IsEnabled = false;
                txtName.IsReadOnly = true;
                txtCountryCode.IsReadOnly = true;
                if (SelectedEntity.IsValid)
                {
                    IsValidCheckBox.IsChecked = true;
                }
                else
                {
                    IsValidCheckBox.IsChecked = false;

                }


            }
        }
        private bool ValidateInputs()
        {
            string Error = "";
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Error = Error + "Please Enter Country Name.\n";
            }
            if (string.IsNullOrEmpty(txtCountryCode.Text))
            {
                Error = Error + "Please Enter Country code.\n";
            }

            if (Error != "")
            {
                MessageControl.ShowMessage("ERROR", Error, 200, 300, Brushes.Red);
                return false;
            }
            return true;
        }
    }
}
