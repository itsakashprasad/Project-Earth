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
    /// Interaction logic for PagesPopupMagic.xaml
    /// </summary>
    public partial class PagesPopup : System.Windows.Controls.UserControl
    {
        public string Mode { get; set; }
        //public string Guid { get; set; }
        public PagesEntities SelectedEntity { get; set; }
        public PagesPopup()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateInputs() && Mode == "ADD")
                {
                    PagesEntities Pages = new PagesEntities
                    {
                        Mode = "ADD",
                        Guid = new Guid().ToString(),
                        Name = txtName.Text,
                        Description = txtDesc.Text,
                        IsValid = Convert.ToBoolean(IsValidCheckBox.IsChecked) ? true : false,
                        AddBy = "AP",
                        AddDate = DateTime.Now
                    };
                    UpsertPages upsertPages = new UpsertPages();
                    upsertPages.ExecuteUpsertPages(Pages);
                }


                else if (ValidateInputs() && Mode == "DELETE")
                {
                    bool Result = WarningMessage.ShowWaningMessage("Delete", "Are You Sure ?", 200, 300, Brushes.Red);

                    if (Result)
                    {

                        try
                        {
                            PagesEntities Pages = new PagesEntities
                            {
                                Mode = Mode,
                                Guid = SelectedEntity.Guid,
                                IsDeleted = true,
                            };
                            UpsertPages upsertPages = new UpsertPages();
                            upsertPages.ExecuteUpsertPages(Pages);
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
                    PagesEntities Pages = new PagesEntities
                    {
                        Mode = Mode,
                        Guid = SelectedEntity.Guid,
                        Name = txtName.Text,
                        Description = txtDesc.Text,
                        IsValid = (bool)IsValidCheckBox.IsChecked,

                    };
                    UpsertPages upsertPages = new UpsertPages();
                    upsertPages.ExecuteUpsertPages(Pages);
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


                txtPopupTitle.Text = "ADD PAGES";
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
                
                txtPopupTitle.Text = "VIEW PAGES";
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
                txtPopupTitle.Text = "EDIT PAGES";
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
                txtPopupTitle.Text = "DELETE PAGES";
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
                Error = Error + "Please Enter Pages Name.\n";
            }
            if (string.IsNullOrEmpty(txtDesc.Text))
            {
                Error = Error + "Please Enter Pages code.\n";
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
