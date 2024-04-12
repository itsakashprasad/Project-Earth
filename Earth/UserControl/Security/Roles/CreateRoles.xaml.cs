using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Earth
{
    public partial class CreateRoles : System.Windows.Controls.UserControl
    {
        public string Mode { get; set; }
        public RolesEntities SelectedEntity { get; set; }

        public CreateRoles()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs() && Mode == "ADD")
                {
                    RolesEntities Roles = new RolesEntities
                    {
                        Mode = "ADD",
                        Guid = new Guid().ToString(),
                        Name = txtName.Text,
                        Description = txtDesc.Text,
                        RoleType = txtRoleType.Text,
                        IsValid = Convert.ToBoolean(IsValidCheckBox.IsChecked) ? true : false,
                        AddBy = "AP",
                        AddDate = DateTime.Now
                    };
                    UpsertRoles upsertRoles = new UpsertRoles();
                    upsertRoles.ExecuteUpsertRoles(Roles);
                }


                else if (ValidateInputs() && Mode == "DELETE")
                {
                    bool Result = WarningMessage.ShowWaningMessage("Delete", "Are You Sure ?", 200, 300, Brushes.Red);

                    if (Result)
                    {

                        try
                        {
                            RolesEntities Roles = new RolesEntities
                            {
                                Mode = Mode,
                                Guid = SelectedEntity.Guid,
                                IsDeleted = true,
                            };
                            UpsertRoles upsertRoles = new UpsertRoles();
                            upsertRoles.ExecuteUpsertRoles(Roles);
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
                    RolesEntities Roles = new RolesEntities
                    {
                        Mode = Mode,
                        Guid = SelectedEntity.Guid,
                        Name = txtName.Text,
                        Description = txtDesc.Text,
                        RoleType = txtRoleType.Text,
                        IsValid = (bool)IsValidCheckBox.IsChecked,

                    };
                    UpsertRoles upsertRoles = new UpsertRoles();
                    upsertRoles.ExecuteUpsertRoles(Roles);
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


                txtPopupTitle.Text = "ADD ROLES";
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

                txtPopupTitle.Text = "VIEW ROLES";
                txtName.Text = SelectedEntity.Name;
                txtName.IsReadOnly = true;
                txtDesc.Text = SelectedEntity.Description;
                txtDesc.IsReadOnly = true;
                txtRoleType.Text = SelectedEntity.RoleType;
                txtRoleType.IsReadOnly = true;
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
                txtDesc.Text = SelectedEntity.Description;
                txtRoleType.Text = SelectedEntity.RoleType;               
                txtPopupTitle.Text = "EDIT ROLES";
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
                txtDesc.Text = SelectedEntity.Description;
                txtRoleType.Text = SelectedEntity.RoleType;
                txtRoleType.IsReadOnly = true;
                txtPopupTitle.Text = "DELETE ROLES";
                btnEdit.IsEnabled = false;
                btnEdit.Visibility = Visibility.Collapsed;
                btnAdd.IsEnabled = true;
                btnAdd.Content = "DELETE";
                IsValidCheckBox.IsEnabled = false;
                txtName.IsReadOnly = true;
                txtDesc.IsReadOnly = true;
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
                Error = Error + "Please Enter Role Name.\n";
            }
            if (string.IsNullOrEmpty(txtDesc.Text))
            {
                Error = Error + "Please Enter Role Desccription.\n";
            }
            if (string.IsNullOrEmpty(txtRoleType.Text))
            {
                Error = Error + "Please Enter Role Type.\n";
            }

            if (Error != "")
            {
                MessageControl.ShowMessage("EXCEPTION", Error, 200, 300, Brushes.Red);
                return false;
            }
            return true;
        }

        private void ClearInputs()
        {
            txtName.Text = "";
            txtDesc.Text = "";
        }


        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
