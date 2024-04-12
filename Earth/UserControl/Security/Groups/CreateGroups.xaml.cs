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
    public partial class CreateGroups : System.Windows.Controls.UserControl
    {
        public string Mode { get; set; }
        public GroupsEntities SelectedEntity { get; set; }

        public CreateGroups()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs() && Mode == "ADD")
                {
                    GroupsEntities Groups = new GroupsEntities
                    {
                        Mode = "ADD",
                        Guid = new Guid().ToString(),
                        Name = txtName.Text,
                        Description = txtDesc.Text,
                        IsValid = Convert.ToBoolean(IsValidCheckBox.IsChecked) ? true : false,
                        AddBy = "AP",
                        AddDate = DateTime.Now
                    };
                    UpsertGroups upsertGroups = new UpsertGroups();
                    upsertGroups.ExecuteUpsertGroups(Groups);
                }


                else if (ValidateInputs() && Mode == "DELETE")
                {
                    //var Result = MessageBox.Show("Are You Sure ?", "Warning!!", MessageBoxButton.YesNo);
                    bool Result = WarningMessage.ShowWaningMessage("Delete","Are You Sure ?",200,300,Brushes.Red);

                    if (Result)
                    {

                        try
                        {
                            GroupsEntities Groups = new GroupsEntities
                            {
                                Mode = Mode,
                                Guid = SelectedEntity.Guid,
                                IsDeleted = true,
                            };
                            UpsertGroups upsertGroups = new UpsertGroups();
                            upsertGroups.ExecuteUpsertGroups(Groups);
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
                    GroupsEntities Groups = new GroupsEntities
                    {
                        Mode = Mode,
                        Guid = SelectedEntity.Guid,
                        Name = txtName.Text,
                        Description = txtDesc.Text,
                        IsValid = (bool)IsValidCheckBox.IsChecked,

                    };
                    UpsertGroups upsertGroups = new UpsertGroups();
                    upsertGroups.ExecuteUpsertGroups(Groups);
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


                txtPopupTitle.Text = "ADD GroupsS";
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

                txtPopupTitle.Text = "VIEW GroupsS";
                txtName.Text = SelectedEntity.Name;
                txtName.IsReadOnly = true;
                txtDesc.Text = SelectedEntity.Description;
                txtDesc.IsReadOnly = true;
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
                txtPopupTitle.Text = "EDIT GroupsS";
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
                txtPopupTitle.Text = "DELETE GroupsS";
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
                Error = Error + "Please Enter Groups Name.\n";
            }
            if (string.IsNullOrEmpty(txtDesc.Text))
            {
                Error = Error + "Please Enter Groups code.\n";
            }

            if (Error != "")
            {
                //MessageBox.Show(Error);
                MessageControl.ShowMessage("ERROR", Error, 200, 300, Brushes.Red);
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
