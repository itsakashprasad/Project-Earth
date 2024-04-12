using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using System.Data;
using System.Windows.Media;

namespace Earth
{
    /// <summary>
    /// Interaction logic for StatePopupMagic.xaml
    /// </summary>
    public partial class StatePopupMagic : System.Windows.Controls.UserControl
    {
        public string Mode { get; set; }
        public StateEntities SelectedEntity { get; set; }
        public List<CountryEntities> CountryList { get; set; }

        public StatePopupMagic()
        {
            InitializeComponent();
        }
        private void LoadCountry()
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
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs() && Mode == "ADD")
                {

                    CountryEntities selectedCountry = (CountryEntities)CountryDrop.SelectedItem;

                    if (selectedCountry != null)
                    {
                        StateEntities State = new StateEntities
                        {
                            Mode = "ADD",
                            Guid = new Guid().ToString(),
                            CountryGuid = selectedCountry.Guid,
                            Name = txtName.Text,
                            StateCode = txtStateCode.Text,
                            IsValid = IsValidCheckBox.IsChecked ?? false,
                            AddBy = "AP",
                            AddDate = DateTime.Now
                        };

                        UpsertState upsertState = new UpsertState();
                        upsertState.ExecuteUpsertState(State);
                    }
                    else
                    {
                        MessageControl.ShowMessage("Message", "Please Select Country", 200, 300, Brushes.Red);
                    }
                }
                else if (ValidateInputs() && Mode == "DELETE")
                {
                    bool Result = WarningMessage.ShowWaningMessage("Delete", "Are You Sure ?", 200, 300, Brushes.Red);

                    if (Result)
                    {

                        try
                        {
                            StateEntities State = new StateEntities
                            {
                                Mode = Mode,
                                Guid = SelectedEntity.Guid,
                                IsDeleted = true,
                            };
                            UpsertState upsertState = new UpsertState();
                            upsertState.ExecuteUpsertState(State);
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
                    StateEntities State = new StateEntities
                    {
                        Mode = Mode,
                        Guid = SelectedEntity.Guid,
                        CountryGuid = CountryDrop.SelectedValue.ToString(),
                        Name = txtName.Text,
                        StateCode = txtStateCode.Text,
                        IsValid = (bool)IsValidCheckBox.IsChecked,

                    };
                    UpsertState upsertState = new UpsertState();
                    upsertState.ExecuteUpsertState(State);
                }

            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
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
                Error = Error + "Please Enter State Name.\n";
            }
            if (string.IsNullOrEmpty(txtStateCode.Text))
            {
                Error = Error + "Please Enter State code.\n";
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


                txtPopupTitle.Text = "ADD STATE";
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
                CountryDrop.SelectedValue = SelectedEntity.CountryGuid;
                CountryDrop.IsEnabled = false;
                txtPopupTitle.Text = "VIEW STATE";
                txtName.Text = SelectedEntity.Name;
                txtName.IsReadOnly = true;
                txtStateCode.Text = SelectedEntity.StateCode;
                txtStateCode.IsReadOnly = true;
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
                CountryDrop.SelectedValue = SelectedEntity.CountryGuid;
                txtName.Text = SelectedEntity.Name;
                txtStateCode.Text = SelectedEntity.StateCode;
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
                CountryDrop.SelectedValue = SelectedEntity.CountryGuid;
                txtName.Text = SelectedEntity.Name;
                txtStateCode.Text = SelectedEntity.StateCode;
                CountryDrop.IsEnabled = false;
                txtPopupTitle.Text = "DELETE COUNTRY";
                txtName.IsReadOnly = true;
                txtStateCode.IsReadOnly = true;
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
