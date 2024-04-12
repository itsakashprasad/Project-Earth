using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
    /// Interaction logic for CityPopupMagic.xaml
    /// </summary>
    public partial class CityPopupMagic : System.Windows.Controls.UserControl
    {
        public string Mode { get; set; }
        public CityEntities SelectedEntity { get; set; }
        public List<CountryEntities> CountryList { get; set; }
        public List<StateEntities> StateList { get; set; }
        public CityPopupMagic()
        {
            InitializeComponent();
        }
        private void LoadCountry()
        {
            try
            {
                CountryList = new List<CountryEntities>();
                CountryList.Clear();

                string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
                MySqlConnection connection = new MySqlConnection(connectionString);

                MySqlCommand command = new MySqlCommand("GetCountries", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@p_Result", MySqlDbType.VarChar, 4000).Direction = ParameterDirection.Output;
                command.Parameters.Add("@p_Return", MySqlDbType.Int32).Direction = ParameterDirection.Output;

                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CountryEntities country = new CountryEntities
                    {
                        Guid = reader["Guid"].ToString(),
                        Name = reader["Name"].ToString(),

                    };

                    CountryList.Add(country);
                }
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }

        }
        private void LoadStates(string countryGuid)
        {
            try
            {
                StateList = new List<StateEntities>();
                StateList.Clear();

                string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
                MySqlConnection connection = new MySqlConnection(connectionString);

                MySqlCommand command = new MySqlCommand("GetStatesByCountry", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@p_CountryGuid", countryGuid);

                command.Parameters.Add("@p_Result", MySqlDbType.VarChar, 4000).Direction = ParameterDirection.Output;
                command.Parameters.Add("@p_Return", MySqlDbType.Int32).Direction = ParameterDirection.Output;


                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    StateEntities state = new StateEntities
                    {
                        Guid = reader["Guid"].ToString(),
                        Name = reader["Name"].ToString(),

                    };

                    StateList.Add(state);
                }
                reader.Close();
                connection.Close();

                StateDrop.ItemsSource = StateList;
                StateDrop.SelectedIndex = -1;

            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }

        }
        private void CountryDrop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CountryDrop.SelectedItem != null)
                {

                    CountryEntities selectedCountry = (CountryEntities)CountryDrop.SelectedItem;

                    LoadStates(selectedCountry.Guid);
                }

            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }


        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs() && Mode == "ADD")
                {

                    StateEntities selectedState = (StateEntities)StateDrop.SelectedItem;

                    if (selectedState != null)
                    {
                        CityEntities City = new CityEntities
                        {
                            Mode = "ADD",
                            Guid = new Guid().ToString(),
                            StateGuid = selectedState.Guid,
                            Name = txtName.Text,
                            CityCode = txtCityCode.Text,
                            IsValid = IsValidCheckBox.IsChecked ?? false,
                            AddBy = "AP",
                            AddDate = DateTime.Now
                        };

                        UpsertCity upsertCity = new UpsertCity();
                        upsertCity.ExecuteUpsertCity(City);
                    }
                    else
                    {
                        MessageBox.Show("Please select a country from the ComboBox.");
                    }
                }
                else if (ValidateInputs() && Mode == "DELETE")
                {
                    bool Result = WarningMessage.ShowWaningMessage("Delete", "Are You Sure ?", 200, 300, Brushes.Red);

                    if (Result)
                    {

                        try
                        {
                            CityEntities City = new CityEntities
                            {
                                Mode = Mode,
                                Guid = SelectedEntity.Guid,
                                IsDeleted = true,
                            };
                            UpsertCity upsertCity = new UpsertCity();
                            upsertCity.ExecuteUpsertCity(City);
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
                StateEntities selectedState = (StateEntities)StateDrop.SelectedItem;
                if (ValidateInputs())
                {
                    CityEntities City = new CityEntities
                    {
                        Mode = Mode,
                        Guid = SelectedEntity.Guid,
                        StateGuid = selectedState.Guid,
                        Name = txtName.Text,
                        CityCode = txtCityCode.Text,
                        IsValid = (bool)IsValidCheckBox.IsChecked,

                    };
                    UpsertCity upsertCity = new UpsertCity();
                    upsertCity.ExecuteUpsertCity(City);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void IsValidCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            btnAdd.IsEnabled = true;

        }

        private void IsValidCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            btnAdd.IsEnabled = false;


        }

        private bool ValidateInputs()
        {
            string Error = "";
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Error = Error + "Please Enter City Name.\n";
            }
            if (string.IsNullOrEmpty(txtCityCode.Text))
            {
                Error = Error + "Please Enter City code.\n";
            }

            if (Error != "")
            {
                MessageControl.ShowMessage("ERROR", Error, 200, 300, Brushes.Red);
                return false;
            }
            return true;
        }
        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            LoadCountry();
            CountryDrop.ItemsSource = CountryList;

            IsValidCheckBox.Checked += IsValidCheckBox_Checked;
            IsValidCheckBox.Unchecked += IsValidCheckBox_Unchecked;

            if (Mode == "ADD")
            {


                txtPopupTitle.Text = "ADD CITY";
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
                //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() =>
                //{
                //    CountryDrop.SelectedValue = SelectedEntity.CountryGuid;
                //}));
                if (SelectedEntity != null)
                {
                    CountryDrop.Text = SelectedEntity.CountryName;
                    StateDrop.SelectedValue = SelectedEntity.StateGuid;
                }


                CountryDrop.IsEnabled = false;
                StateDrop.IsEnabled = false;
                txtPopupTitle.Text = "VIEW CITY";
                txtName.Text = SelectedEntity.Name;
                txtName.IsReadOnly = true;
                txtCityCode.Text = SelectedEntity.CityCode;
                txtCityCode.IsReadOnly = true;
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


                //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() =>
                //{
                //    CountryDrop.SelectedValue = SelectedEntity.CountryGuid;
                //}));
                if (SelectedEntity != null)
                {
                    CountryDrop.Text = SelectedEntity.CountryName;
                    StateDrop.SelectedValue = SelectedEntity.StateGuid;
                }
                CountryDrop.IsEnabled = false;
                txtName.Text = SelectedEntity.Name;
                txtCityCode.Text = SelectedEntity.CityCode;
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
                //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() =>
                //{
                //    CountryDrop.SelectedValue = SelectedEntity.CountryGuid;
                //}));
                if (SelectedEntity != null)
                {
                    CountryDrop.Text = SelectedEntity.CountryName;
                    StateDrop.SelectedValue = SelectedEntity.StateGuid;
                }
                CountryDrop.IsEnabled = false;
                StateDrop.IsEnabled = false;
                txtName.Text = SelectedEntity.Name;
                txtCityCode.Text = SelectedEntity.CityCode;
                CountryDrop.IsEnabled = false;
                txtPopupTitle.Text = "DELETE COUNTRY";
                txtName.IsReadOnly = true;
                txtCityCode.IsReadOnly = true;
                btnEdit.IsEnabled = false;
                btnEdit.Visibility = Visibility.Collapsed;
                btnAdd.IsEnabled = true;
                btnAdd.Content = "DELETE";
                IsValidCheckBox.IsEnabled = false;

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
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }


    }
}
