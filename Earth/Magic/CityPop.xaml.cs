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
    /// Interaction logic for CityPop.xaml
    /// </summary>
    public partial class CityPop : System.Windows.Controls.UserControl
    {
        public string Mode { get; set; }
        public CityEntities objParam { get; set; }
        public CityPop()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //GetCountries();
                //OnLoad();
            }
            catch (Exception ex)
            {
                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "UserControl_Loaded");
               MessageBox.Show("EXCEPTION", ex.Message);
            }
        }
        private void GetCountries()
        {
            //try
            //{
            //    int ReturnValue = -1;
            //    string ReturnMessage = "";
            //    GetCountries BLC = new GetCountries();
            //    List<CountryEntities> ListOfCountry = new List<CountryEntities>();
            //    ListOfCountry = BLC.GetCountriesFromDatabase("SELECT", out ReturnValue, out ReturnMessage);
            //    if (ReturnValue == 0)
            //    {
            //        cmbCountry.ItemsSource = ListOfCountry;
            //        cmbCountry.DisplayMemberPath = "Name";
            //        cmbCountry.SelectedValuePath = "Guid";
            //    }
            //    else
            //    {
            //        MessageBox.Show("EXCEPTION", ex.Message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            //}
        }
        private void GetStateByCountryGuid(string CountryGuid)
        {
            try
            {
                //int ReturnValue = -1;
                //string ReturnMessage = "";
                //BL.Master.State BLC = new BL.Master.State();
                //List<ENTITIES.Master.State> ListOfState = new List<ENTITIES.Master.State>();
                //ListOfState = BLC.GetStateByCountryGuid(CountryGuid, out ReturnValue, out ReturnMessage);
                //if (ReturnValue == 0)
                //{
                //    cmbState.ItemsSource = ListOfState;
                //    cmbState.DisplayMemberPath = "Name";
                //    cmbState.SelectedValuePath = "Guid";
                //}
                //else
                //{
                //    MessageBox.Show("EXCEPTION", ex.Message);
                //}
            }
            catch (Exception )
            {
                //Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void OnLoad()
        {
            try
            {
                //txtHeaderName.Text = "CITY - " + Mode.ToUpper();
                //btnAdd.Content = Mode.ToUpper();
                //int ReturnValue = -1;
                //string ReturnMessage = "";
                //BL.Master.City BLppsroles = new BL.Master.City();
                //ENTITIES.Master.City objCity = new ENTITIES.Master.City();
                //List<ENTITIES.Master.City> ListOfppsroles = new List<ENTITIES.Master.City>();
                //if (objParam.Guid != new Guid().ToString())
                //{
                //    ListOfppsroles = BLppsroles.GetCity(objParam.Guid, out ReturnValue, out ReturnMessage);
                //    objCity = ListOfppsroles.FirstOrDefault();
                //    if (objCity != null)
                //    {
                //        txtName.Text = objCity.Name.ToString();
                //        txtCityCode.Text = objCity.CityCode.ToString();
                //        cmbCountry.SelectedValue = objCity.CountryGuid;
                //        cmbState.SelectedValue = objCity.StateGuid;
                //        txtIsValid.IsChecked = objCity.IsValid;
                //    }
                //}
                //switch (Mode)
                //{
                //    case "ADD":
                //        btnAdd.Content = Mode.ToUpper();
                //        txtIsValid.IsEnabled = false;
                //        txtIsValid.IsChecked = true;
                //        break;
                //    case "EDIT":
                //        btnAdd.Content = Mode.ToUpper();
                //        break;
                //    case "VIEW":
                //        btnAdd.Content = Mode.ToUpper();
                //        btnAdd.IsEnabled = false;

                //        cmbCountry.IsEnabled = false;
                //        cmbState.IsEnabled = false;
                //        txtName.IsEnabled = false;
                //        txtCityCode.IsEnabled = false;
                //        txtIsValid.IsEnabled = false;
                //        break;
                //    case "DELETE":
                //        btnAdd.Content = Mode.ToUpper();
                //        txtName.IsEnabled = false;
                //        cmbCountry.IsEnabled = false;
                //        cmbState.IsEnabled = false;
                //        txtCityCode.IsEnabled = false;
                //        txtIsValid.IsEnabled = false;
                //        break;
                //    default: break;
                //}
            }
            catch (Exception)
            {
                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "OnLoad");
                //Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void ClearInputs()
        {
            txtName.Text = "";
            txtCityCode.Text = "";
        }
        private bool ValidateInputs()
        {
            string Error = "";
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Error = Error + "Please Enter Name.\n";
            }
            if (string.IsNullOrEmpty(txtCityCode.Text))
            {
                Error = Error + "Please Enter City code.\n";
            }

            if (Error != "")
            {
                //Class.MessageControl.ShowMessage("ERROR", Error, 200, 400, Brushes.Red);
                return false;
            }
            return true;
        }
       

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (ValidateInputs())
                //{
                //    if (Mode == "DELETE")
                //    {
                //        bool Result = Class.WarningMessage.ShowWaningMessage("DELETE - Label", "Do you want to delete this pps_roles ? ", 200, 300, Brushes.Red);
                //        if (Result)
                //        {
                //            City(sender);
                //        }
                //    }
                //    else
                //    {
                //        City(sender);
                //    }
                //}
            }
            catch (Exception )
            {
                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "btnAdd_Click");
                //Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void City(object sender)
        {
            try
            {
                //string ReturnMessage = "";
                //int ReturnValue = -1;
                //BL.Master.City BLppsroles = new BL.Master.City();
                //ENTITIES.Master.City objCity = new ENTITIES.Master.City();
                //objCity.Guid = objParam.Guid;
                //objCity.CountryGuid = cmbCountry.SelectedValue.ToString();
                //objCity.StateGuid = cmbState.SelectedValue.ToString();
                //objCity.Name = txtName.Text;
                //objCity.CityCode = txtCityCode.Text;

                //objCity.IsValid = (bool)txtIsValid.IsChecked;
                //objCity.IsDeleted = (Mode == "DELETE") ? true : objParam.IsDeleted;
                //BLppsroles.UpsertCity(objCity, out ReturnValue, out ReturnMessage);
                //if (ReturnValue == 0)
                //{
                //    ClearInputs();
                //    String Message = "";
                //    Window ParentWindow = ParentHelper.FoundParent(this, out Message);
                //    ParentWindow.Close();
                //}
                //else
                //{
                //    MessageBox.Show("EXCEPTION", ex.Message);
                //}
            }
            catch (Exception )
            {
                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "UpsertLabel");
                //Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            btnCancel.IsCancel = true;
        }

        private void cmbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    if (cmbCountry.SelectedValue != null)
            //    {
            //        GetStateByCountryGuid(cmbCountry.SelectedValue.ToString());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            //}
        }
    }
}
