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
    /// Interaction logic for CityPopup.xaml
    /// </summary>
    public partial class CityPopup : Window
    {
        public string Mode { get; set; }
        //public string Guid { get; set; }
        public string CaptureCountryGuid = "";
        public string CaptureStateGuid = "";


        public bool Valid = false;

        public string CapturedGuid = "";
        public List<CountryEntities> CountryList { get; set; }
        public List<StateEntities> StateList { get; set; }


        public CityPopup(string mode)
        {
            InitializeComponent();
            Mode = mode;
            IsValidCheckBox.Checked += IsValidCheckBox_Checked;
            IsValidCheckBox.Unchecked += IsValidCheckBox_Unchecked;

            if (mode == "ADD")
            {


                txtPopupTitle.Text = "Add City";
                BtnDelete.IsEnabled = false;
                BtnDelete.Visibility = Visibility.Hidden;
                BtnSave.IsEnabled = false;
                BtnSave.Visibility = Visibility.Hidden;


            }
            else if (mode == "VIEW")
            {


                txtPopupTitle.Text = "View City";
                BtnCancel.Content = "OK";
                BtnCancel.HorizontalAlignment = HorizontalAlignment.Center;
                txtFirstInputBox.IsReadOnly = true;
                //txtSecondInputBox.IsReadOnly = true;
                BtnDelete.IsEnabled = false;
                BtnDelete.Visibility = Visibility.Collapsed;
                BtnSave.IsEnabled = false;
                BtnSave.Visibility = Visibility.Hidden;
                BtnAdd.IsEnabled = false;
                BtnAdd.Visibility = Visibility.Hidden;
                IsValidCheckBox.IsChecked = true;
                IsValidCheckBox.IsEnabled = false;
                CountryDrop.IsEnabled=false;
                StateDrop.IsEnabled = false;



            }
            else if (mode == "EDIT")
            {

                txtPopupTitle.Text = "Edit City";
                BtnAdd.IsEnabled = false;
                BtnAdd.Visibility = Visibility.Hidden;
                BtnDelete.IsEnabled = false;
                BtnDelete.Visibility = Visibility.Collapsed;





            }
            else
            {

                txtPopupTitle.Text = "Delete City";
                BtnSave.IsEnabled = false;
                BtnSave.Visibility = Visibility.Hidden;
                BtnAdd.IsEnabled = false;
                BtnAdd.Visibility = Visibility.Hidden;
                IsValidCheckBox.IsChecked = true;
                IsValidCheckBox.IsEnabled = false;
                txtFirstInputBox.IsReadOnly = true;
                CountryDrop.IsEnabled = false;
                StateDrop.IsEnabled = false;
                //txtSecondInputBox.IsReadOnly = true;


            }
        }
        //-------------------------------------------------------------------------------------------------------------------------------------------------------------
        //View Control


        public void SetSelectedEntity(CityEntities selectedEntity)
        {


            CapturedGuid = selectedEntity.Guid;
            CaptureStateGuid = selectedEntity.StateGuid;
            txtFirstInputBox.Text = selectedEntity.Name;
            //txtSecondInputBox.Text = selectedEntity.Age.ToString();
           


        }
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StateEntities selectedState = (StateEntities)CountryDrop.SelectedItem;
                if (ValidateInputs())
                {
                    CityEntities City = new CityEntities
                    {
                        Mode = "ADD",
                        Guid = new Guid().ToString(),
                        StateGuid = selectedState.Guid,
                        Name = txtFirstInputBox.Text,                        
                        IsValid = IsValidCheckBox.IsChecked ?? false,
                        AddBy = "AP",
                        AddDate = DateTime.Now
                    };
                    UpsertCity upsertCity = new UpsertCity();
                    upsertCity.ExecuteUpsertCity(City);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }



        }


        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var Result = MessageBox.Show("Are You Sure ?", "Warning!!", MessageBoxButton.YesNo);

            if (Result == MessageBoxResult.Yes)
            {


                try
                {
                    CityEntities City = new CityEntities
                    {
                        Mode="DELETE",
                        Guid = CapturedGuid,
                        
                    };
                    UpsertCity upsertCity = new UpsertCity();
                    upsertCity.ExecuteUpsertCity(City);
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
        //EDIT
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //StateEntities selectedCountry = (StateEntities)CountryDrop.SelectedItem;
            //CityEntities selectedState = (CityEntities)StateDrop.SelectedItem;
            try
            {
                CityEntities City = new CityEntities
                {
                    Mode = Mode,
                    Guid = CapturedGuid,
                    StateGuid = CaptureStateGuid,
                    //CountryName = CountryDrop.SelectedItem.ToString(),
                    Name = txtFirstInputBox.Text,                    
                    IsValid = (bool)IsValidCheckBox.IsChecked ? true : false,
                    UpdateBy = "Akash",
                    UpdateDate = DateTime.Now,

                };
                UpsertCity upsertCity = new UpsertCity();
                upsertCity.ExecuteUpsertCity(City);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
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
            //if (string.IsNullOrEmpty(txtSecondInputBox.Text))
            //{
            //    Error = Error + "Please Enter Country code.\n";
            //}

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
