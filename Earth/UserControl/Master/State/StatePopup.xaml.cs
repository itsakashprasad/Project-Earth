using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for StatePopup.xaml
    /// </summary>
    public partial class StatePopup : Window
    {
        public string Mode { get; set; }
        public StateEntities SelectedEntity { get; set; }
        //public string Guid { get; set; }
        public string CaptureCountryGuid = "";

        public string CapturedGuid = "";
        public List<CountryEntities> CountryList { get; set; }

        public StatePopup()
        {
            InitializeComponent();

        }

        private void LoadCountry()
        {
            CountryList = new List<CountryEntities>();
            CountryList.Clear();
            string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand("Select Name,Guid from country where IsValid = true order by Name", connection);


            connection.Open();

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                CountryEntities country = new CountryEntities
                {
                    Name = reader["Name"].ToString(),
                    Guid = reader["Guid"].ToString(),

                };

                CountryList.Add(country);
            }
            reader.Close();
        }
        //public void SetSelectedEntity(StateEntities selectedEntity)
        // {


        //    CapturedGuid = selectedEntity.Guid;
        //    CaptureCountryGuid = selectedEntity.CountryGuid;



        //    //CountryDrop.SelectedValue = selectedEntity.CountryGuid;
        //    //CountryDrop.DisplayMemberPath=selectedEntity.CountryName;

        //     CountryDrop.SelectedItem = selectedEntity.CountryName;
        //    CountryDrop.Text = selectedEntity.CountryName;



        //    txtFirstInputBox.Text = selectedEntity.Name;
        //    txtSecondInputBox.Text = selectedEntity.StateCode;
        //}
        //-----------------------------------------------------------------------------------+---------------------------------------------------------------------------
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {

                    CountryEntities selectedCountry = (CountryEntities)CountryDrop.SelectedItem;

                    if (selectedCountry != null)
                    {
                        StateEntities State = new StateEntities
                        {
                            Mode = "ADD",
                            Guid = new Guid().ToString(),
                            CountryGuid = selectedCountry.Guid,
                            Name = txtFirstInputBox.Text,
                            StateCode = txtSecondInputBox.Text,
                            IsValid = IsValidCheckBox.IsChecked ?? false,
                            AddBy = "AP",
                            AddDate = DateTime.Now
                        };

                        UpsertState upsertState = new UpsertState();
                        upsertState.ExecuteUpsertState(State);
                    }
                    else
                    {
                        MessageBox.Show("Please select a country from the ComboBox.");
                    }
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
                    StateEntities State = new StateEntities
                    {
                        Mode = Mode,
                        Guid = CapturedGuid,

                    };
                    UpsertState upsertState = new UpsertState();
                    upsertState.ExecuteUpsertState(State);
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


            try
            {
                StateEntities State = new StateEntities
                {
                    Guid = CapturedGuid,
                    CountryGuid = CaptureCountryGuid,
                    //CountryName = CountryDrop.SelectedItem.ToString(),
                    Name = txtFirstInputBox.Text,
                    StateCode = txtSecondInputBox.Text,
                    IsValid = (bool)IsValidCheckBox.IsChecked ? true : false,
                    UpdateBy = "Akash",
                    UpdateDate = DateTime.Now,

                };
                UpsertState upsertState = new UpsertState();
                upsertState.ExecuteUpsertState(State);
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
                Error = Error + "Please Enter State Name.\n";
            }
            if (string.IsNullOrEmpty(txtSecondInputBox.Text))
            {
                Error = Error + "Please Enter State code.\n";
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
            LoadCountry();
            CountryDrop.ItemsSource = CountryList;

            IsValidCheckBox.Checked += IsValidCheckBox_Checked;
            IsValidCheckBox.Unchecked += IsValidCheckBox_Unchecked;

            if (Mode == "ADD")
            {


                txtPopupTitle.Text = "Add State";
                //BtnDelete.IsEnabled = false;
                //BtnDelete.Visibility = Visibility.Hidden;
                //BtnSave.IsEnabled = false;
                //BtnSave.Visibility = Visibility.Hidden;


            }
            else if (Mode == "VIEW")
            {
                //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() => {
                //    CountryDrop.SelectedValue = selectedEntity.CountryGuid;
                //}));
                CountryDrop.SelectedValue = SelectedEntity.CountryGuid;
                txtFirstInputBox.Text = SelectedEntity.Name;
                txtSecondInputBox.Text = SelectedEntity.StateCode;
                CountryDrop.IsEnabled = false;
                txtPopupTitle.Text = "View State";
                //BtnCancel.Content = "OK";
                //BtnCancel.HorizontalAlignment = HorizontalAlignment.Center;
                txtFirstInputBox.IsReadOnly = true;
                txtSecondInputBox.IsReadOnly = true;


                //BtnDelete.IsEnabled = false;
                //BtnDelete.Visibility = Visibility.Collapsed;
                //BtnSave.IsEnabled = false;
                //BtnSave.Visibility = Visibility.Hidden;
                BtnAdd.IsEnabled = false;
                BtnAdd.Visibility = Visibility.Hidden;
                IsValidCheckBox.IsChecked = true;
                IsValidCheckBox.IsEnabled = false;


            }
            else if (Mode == "EDIT")
            {


                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() =>
                {
                    CountryDrop.SelectedValue = SelectedEntity.CountryGuid;
                }));

                txtFirstInputBox.Text = SelectedEntity.Name;
                txtSecondInputBox.Text = SelectedEntity.StateCode;
                txtPopupTitle.Text = "Edit State";
                BtnAdd.IsEnabled = false;
                BtnAdd.Visibility = Visibility.Hidden;
                //BtnDelete.IsEnabled = false;
                //BtnDelete.Visibility = Visibility.Collapsed;





            }
            else if (Mode == "DELETE")
            {
                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() =>
                {
                    CountryDrop.SelectedValue = SelectedEntity.CountryGuid;
                }));
                txtFirstInputBox.Text = SelectedEntity.Name;
                txtSecondInputBox.Text = SelectedEntity.StateCode;
                CountryDrop.IsEnabled = false;
                txtPopupTitle.Text = "Delete State";
                //BtnSave.IsEnabled = false;
                //BtnSave.Visibility = Visibility.Hidden;
                BtnAdd.IsEnabled = false;
                BtnAdd.Visibility = Visibility.Hidden;
                IsValidCheckBox.IsChecked = true;
                IsValidCheckBox.IsEnabled = false;
                txtFirstInputBox.IsReadOnly = true;
                txtSecondInputBox.IsReadOnly = true;


            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
