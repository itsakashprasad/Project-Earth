using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
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
namespace PrintingSetup.UserControls.Security.Users
{
    public partial class Users : UserControl
    {
        List<ENTITIES.Security.Users> ListOfppsusers = null;
        public Users()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                AttachListppsusers();
            }
            catch (Exception ex)
            {
                Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "UserControl_Loaded");
                Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void AttachListppsusers()
        {
            try
            {
                string FilePathofImagePath = ConfigurationSettings.AppSettings["UploadppsusersPath"]+"\\";
                int ReturnValue = -1;
                string ReturnMessage = "";
                BL.Security.Users BLppsusers = new BL.Security.Users();
                ListOfppsusers = new List<ENTITIES.Security.Users>();
                ListOfppsusers = BLppsusers.Getusers(new Guid().ToString(), out ReturnValue, out ReturnMessage);
                ListOfppsusers.ForEach(c => c.ImagePath = FilePathofImagePath + c.ImagePath);
                dataGrid.ItemsSource = ListOfppsusers;
            }
            catch (Exception ex)
            {
                Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "AttachListppsusers");
                Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ENTITIES.Security.Users objParam = new ENTITIES.Security.Users();
                objParam.Guid = new Guid().ToString();
                CallUpsertPage("ADD", objParam);
            }
            catch (Exception ex)
            {
                Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "btnAddNew_Click");
                Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string Mode = dataGrid.CurrentColumn.Header.ToString();
                ENTITIES.Security.Users objParam = new ENTITIES.Security.Users();
                objParam = dataGrid.SelectedItem as ENTITIES.Security.Users;
                if (Mode == "EDIT" || Mode == "VIEW" || Mode == "DELETE")
                {
                    CallUpsertPage(Mode, objParam);
                }
            }
            catch (Exception ex)
            {
                Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "dataGrid_MouseDoubleClick");
                Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string filterText = txtSearch.Text;
                if (filterText != "")
                {
                    List<ENTITIES.Security.Users> SearchList = ListOfppsusers.FindAll(
                   item => item.Name.ToString().Contains(filterText.ToUpper()) ||
                   item.Email.ToString().Contains(filterText.ToUpper()) ||
                   item.Password.ToString().Contains(filterText.ToUpper()) ||
                   item.Phone.ToString().Contains(filterText.ToUpper()) ||
                   item.ImagePath.ToString().Contains(filterText.ToUpper()) ||
                   item.AllowAccessFrom.ToString().Contains(filterText.ToUpper()) ||
                   item.AllowAccessTill.ToString().Contains(filterText.ToUpper()) ||
                   item.ActivationDate.ToString().Contains(filterText.ToUpper()) ||
                   item.Otp.ToString().Contains(filterText.ToUpper())
                   );
                    dataGrid.ItemsSource = SearchList;
                }
                else if (filterText == "")
                {
                    dataGrid.ItemsSource = ListOfppsusers;
                }
            }
            catch (Exception ex)
            {
                Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "txtSearch_TextChanged");
                Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string filterText = txtSearch.Text;
                if (filterText != "")
                {
                    List<ENTITIES.Security.Users> SearchList = ListOfppsusers.FindAll(
                   item => item.Name.ToString().Contains(filterText.ToUpper()) ||
                   item.Email.ToString().Contains(filterText.ToUpper()) ||
                   item.Password.ToString().Contains(filterText.ToUpper()) ||
                   item.Phone.ToString().Contains(filterText.ToUpper()) ||
                   item.ImagePath.ToString().Contains(filterText.ToUpper()) ||
                   item.AllowAccessFrom.ToString().Contains(filterText.ToUpper()) ||
                   item.AllowAccessTill.ToString().Contains(filterText.ToUpper()) ||
                   item.ActivationDate.ToString().Contains(filterText.ToUpper()) ||
                   item.Otp.ToString().Contains(filterText.ToUpper())
                   );
                    dataGrid.ItemsSource = SearchList;
                }
                else if (filterText == "")
                {
                    dataGrid.ItemsSource = ListOfppsusers;
                }
            }
            catch (Exception ex)
            {
                Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "btnSearch_Click");
                Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            String Message = "";
            Window RefWindow = Class.ParentHelper.FoundParent(sender, out Message);
            MainWindow RefMainWindow = (MainWindow)RefWindow;
            RefMainWindow.GridMain.Children.Clear();
        }
        private void CallUpsertPage(string Mode, ENTITIES.Security.Users objParam)
        {
            try
            {
                bool IsAuthorized = Class.Authorization.UserAuthorization("Users.xaml", Mode);
                if (IsAuthorized == true)
                {
                    DoubleAnimation animFadeIn = new DoubleAnimation();
                    animFadeIn.From = 0;
                    animFadeIn.To = 1;
                    animFadeIn.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                    UserControls.Security.Users.CreateUsers PageObj = new CreateUsers();
                    PageObj.Mode = Mode;
                    PageObj.objParam = objParam;
                    Window window = new Window
                    {
                        WindowStyle = WindowStyle.None,
                        WindowState = WindowState.Normal,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Content = PageObj,
                    };
                    window.BeginAnimation(Window.OpacityProperty, animFadeIn);
                    window.ShowDialog();
                    AttachListppsusers();
                }
            }
            catch (Exception ex)
            {
                Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "CallUpsertPage");
                Class.MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
    }
}
