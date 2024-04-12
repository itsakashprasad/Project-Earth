using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Earth
{
    public partial class CreateUsers : System.Windows.Controls.UserControl
    {
        public string Mode { get; set; }
        //public string Mode="ADD";
        //public string Guid { get; set; }
        public UserEntities SelectedEntity { get; set; }
        public CreateUsers()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                OnLoad();
                IsValidCheckBox.Checked += IsValidCheckBox_Checked;
                IsValidCheckBox.Unchecked += IsValidCheckBox_Unchecked;
            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void OnLoad()
        {
            try
            {
                if (Mode == "ADD")
                {
                    txtPopupTitle.Text = "ADD USER";
                    btnEdit.IsEnabled = false;
                    btnEdit.Visibility = Visibility.Collapsed;
                    btnAdd.IsEnabled = false;
                    IsValidCheckBox.IsChecked = false;


                }

                else if (Mode == "VIEW")
                {

                    txtPopupTitle.Text = "VIEW USER";
                    txtName.Text = SelectedEntity.Name;
                    txtName.IsReadOnly = true;
                    txtEmail.Text = SelectedEntity.EmailId;
                    txtEmail.IsReadOnly = true;
                    txtPhone.Text = SelectedEntity.PhoneNo;
                    txtPhone.IsReadOnly = true;
                    txtPassword.Password = SelectedEntity.Password;
                    txtPassword.IsEnabled = false;

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
                    txtEmail.Text = SelectedEntity.EmailId;
                    txtPhone.Text = SelectedEntity.PhoneNo;
                    txtPassword.Password = SelectedEntity.Password;

                    txtPopupTitle.Text = "EDIT USER";
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
                    txtName.IsReadOnly = true;
                    txtEmail.Text = SelectedEntity.EmailId;
                    txtEmail.IsReadOnly = true;
                    txtPhone.Text = SelectedEntity.PhoneNo;
                    txtPhone.IsReadOnly = true;
                    txtPassword.Password = SelectedEntity.Password;
                    txtPassword.IsEnabled = false;

                    txtPopupTitle.Text = "DELETE USER";


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
        private void ClearInputs()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPassword.Password = "";
            txtPhone.Text = "";
            //txtImagePath.Text = "";
            txtAllowAccessFrom.Text = "";
            txtAllowAccessTill.Text = "";
            txtActivationDate.Text = "";
        }
        private bool ValidateInputs()
        {
            string Error = "";
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Error = Error + "Please Enter Name.\n";
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                Error = Error + "Please Enter Email.\n";
            }
            if (string.IsNullOrEmpty(txtPassword.Password))
            {
                Error = Error + "Please Enter Password.\n";
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                Error = Error + "Please Enter Phone.\n";
            }
            if (string.IsNullOrEmpty(txtActivationDate.Text))
            {
                Error = Error + "Please Enter Activation Date.\n";
            }
            if (string.IsNullOrEmpty(txtAllowAccessFrom.Text))
            {
                Error = Error + "Allow Access From ?.\n";
            }
            if (string.IsNullOrEmpty(txtAllowAccessTill.Text))
            {
                Error = Error + "Allow Access Till ?.\n";
            }
            if (Error != "")
            {
               MessageControl.ShowMessage("EXCEPTION", Error, 200, 300, Brushes.Red);
                return false;
            }
            return true;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs() && Mode == "ADD")
                {
                    UserEntities User = new UserEntities
                    {
                        Mode = Mode,
                        Guid = new Guid().ToString(),
                        Name = txtName.Text,
                        EmailId = txtEmail.Text,
                        PhoneNo = txtPhone.Text,
                        Password = txtPassword.Password,
                        ActivationDate=DateTime.Parse( txtActivationDate.Text),
                        AllowAccessFrom= DateTime.Parse(txtAllowAccessFrom.Text),
                        AllowAccessTill= DateTime.Parse(txtAllowAccessTill.Text),
                        IsValid = Convert.ToBoolean(IsValidCheckBox.IsChecked) ? true : false,
                        AddBy = "AP",
                        AddDate = DateTime.Now
                    };
                    UpsertUser upsertUser = new UpsertUser();
                    upsertUser.ExecuteUpsertUser(User);
                }


                else if (ValidateInputs() && Mode == "DELETE")
                {
                    bool Result = WarningMessage.ShowWaningMessage("Delete", "Are You Sure ?", 200, 300, Brushes.Red);

                    if (Result)
                    {

                        try
                        {
                            UserEntities User = new UserEntities
                            {
                                Mode = Mode,
                                Guid = SelectedEntity.Guid,
                                IsDeleted = true,
                            };
                            UpsertUser upsertUser = new UpsertUser();
                            upsertUser.ExecuteUpsertUser(User);
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
        private void Upsertppsusers(object sender)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //btnCancel.IsCancel = true;
            Window.GetWindow(this).Close();
        }


        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    UserEntities User = new UserEntities
                    {
                        Mode = Mode,
                        Guid = SelectedEntity.Guid,
                        Name = txtName.Text,
                        EmailId = txtEmail.Text,
                        PhoneNo = txtPhone.Text,
                        Password = txtPassword.Password,
                        ActivationDate = DateTime.Parse(txtActivationDate.Text),
                        AllowAccessFrom = DateTime.Parse(txtAllowAccessFrom.Text),
                        AllowAccessTill = DateTime.Parse(txtAllowAccessTill.Text),
                        IsValid = (bool)IsValidCheckBox.IsChecked,
                        UpdateBy = "Akash",
                        UpdateDate = DateTime.Now

                    };
                    UpsertUser upsertUser = new UpsertUser();
                    upsertUser.ExecuteUpsertUser(User);
                }

            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
    }
}
