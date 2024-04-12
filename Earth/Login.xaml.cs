
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace Earth
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
	public partial class Login : Window
    {
        public string username { get; set; }

        public Login()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                txtUsername.Text = "Admin@gmail.com";
                txtUserPassword.Password = "Admin";
                //btnLogin_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 400, Brushes.Red);
            }
        }

        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var uiElement = sender as UIElement;
            if (uiElement != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    DependencyObject parent = uiElement;
                    int avoidInfiniteLoop = 0;
                    // Search up the visual tree to find the first parent window.
                    while ((parent is Window) == false)
                    {
                        parent = VisualTreeHelper.GetParent(parent);
                        avoidInfiniteLoop++;
                        if (avoidInfiniteLoop == 1000)
                        {
                            // Something is wrong - we could not find the parent window.
                            return;
                        }
                    }
                    var window = parent as Window;
                    window.DragMove();
                }
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ReturnValue = -1;
                string ReturnMessage = "";
                string UserEmail = txtUsername.Text;
                string UserPassword = txtUserPassword.Password;
                UserEntities objUser = new UserEntities();
                UserAuthorization BLLogin = new UserAuthorization();
                objUser = BLLogin.UserLogin(UserEmail, UserPassword, out ReturnValue, out ReturnMessage);
                if (ReturnValue == 0)
                {
                    SiteRegistration.objRegistration = objUser;
                    MainWindowMagic MW = new MainWindowMagic();
                    MW.Show();
                    
                    this.Close();

                }
                else
                {
                    MessageControl.ShowMessage("ERROR - LOGIN", ReturnMessage, 200, 400, Brushes.Red);
                }
            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("ERROR - LOGIN", ex.Message, 200, 400, Brushes.Red);
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidateRegistration()
        {
            string Error = "";
            if (string.IsNullOrEmpty(txtRegName.Text))
            {
                Error = "Please enter name.\n";
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                Error = Error + "Please enter email.\n";
            }
            if (string.IsNullOrEmpty(txtPhone.Text))
            {
                Error = Error + "Please enter phone no.\n";
            }
            if (string.IsNullOrEmpty(txtRegPassword.Password))
            {
                Error = Error + "Please enter password.\n";
            }
            if (string.IsNullOrEmpty(txtConfirmPassword.Password))
            {
                Error = Error + "Please enter confirm password.\n";
            }
            if (txtRegPassword.Password != txtConfirmPassword.Password)
            {
                Error = Error + "Password and confirm password not matched.\n";
            }
            if (Error != "")
            {
                MessageControl.ShowMessage("ERROR", Error, 200, 400, Brushes.Red);
                return false;
            }

            return true;
        }

        private void ClearRegistraion()
        {
            txtRegName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtRegPassword.Password = string.Empty;
            txtConfirmPassword.Password = string.Empty;
        }
        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateRegistration())
                {
                    string ReturnGuid = new Guid().ToString();
                    int ReturnValue = -1;
                    string ReturnMessage = "";
                    UsersBL BLMU = new UsersBL();
                    UsersEntity objSR = new UsersEntity();
                    objSR.Guid = new Guid().ToString();
                    objSR.Name = txtRegName.Text;
                    objSR.EmailId = txtEmail.Text;
                    objSR.PhoneNo = txtPhone.Text;
                    objSR.Password = txtConfirmPassword.Password;
                    BLMU.Upsertusers(objSR, out ReturnGuid, out ReturnValue, out ReturnMessage);
                    if (ReturnValue == 0)
                    {
                        ClearRegistraion();
                        MessageControl.ShowMessage("SUCCESS - REGISTRATION", ReturnMessage, 200, 400, Brushes.Green);
                    }
                    else
                    {
                        MessageControl.ShowMessage("ERROR - REGISTRATION", ReturnMessage, 200, 400, Brushes.Red);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("ERROR - LOGIN", ex.Message, 200, 400, Brushes.Red);
            }
        }

        private void btnRegCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

