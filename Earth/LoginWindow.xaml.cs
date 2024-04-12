using MySql.Data.MySqlClient;
using System;

using System.Configuration;

using System.Windows;

using System.Windows.Media;


namespace Earth
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text;
                string password = txtUserPassword.Password;
                if (IsValidUser(username, password) && ValidateLogin())
                {
                    if (IsPermitted(username))
                    {

                        MainWindow mainWindow = new MainWindow(username);
                        mainWindow.UserName = username;
                        mainWindow.Show();

                        Window.GetWindow(this).Close();
                    }
                    else
                    {

                        DummyPage dummy = new DummyPage();
                        dummy.Show();
                        this.Close();
                    }
                }
                else if(ValidateLogin())
                {
                    MessageControl.ShowMessage("Message","User does not exist", 200, 300, Brushes.Red);

                }

            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            


            
        }

        private bool IsValidUser(string username, string password)
        {
            bool isValid = false;

            try
            {
                if (ValidateLogin()) 
                {
                    string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT COUNT(*) FROM user WHERE Name = @p_UserName AND Password = @p_UserPassWord";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@p_UserName", username);
                            command.Parameters.AddWithValue("@p_UserPassWord", password);

                            int count = Convert.ToInt32(command.ExecuteScalar());
                            isValid = count > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }

            return isValid;
        }


        private bool IsPermitted(string username)
        {
            bool isValid = false;

            try
            {
                if (ValidateLogin())
                {
                    string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "SELECT COUNT(*) FROM user WHERE Name = @p_UserName AND IsValid = 1";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@p_UserName", username);

                            int count = Convert.ToInt32(command.ExecuteScalar());
                            isValid = count > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }

            return isValid;
        }

       
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool ValidateLogin()
        {
            string Error = "";
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                Error = "Please enter Username.\n";
            }
            if (string.IsNullOrEmpty(txtUserPassword.Password))
            {
                Error = Error + "Please enter Password.\n";
            }
            
            if (Error != "")
            {
                MessageControl.ShowMessage("ERROR", Error, 200, 300, Brushes.Red);
                return false;
            }

            return true;
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
                MessageControl.ShowMessage("ERROR", Error, 200, 300, Brushes.Red);

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
                    string connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];

                    MySqlConnection connection = new MySqlConnection(connectionString);

                    connection.Open();

                    MySqlCommand command = new MySqlCommand("INSERT INTO user (Guid, Name, EmailId, Password, PhoneNo, IsValid, AddBy, AddDate)VALUES(@p_Guid,@p_Name,@p_Email,@p_Password,@p_Phone,@p_IsValid,@p_AddBy,@p_AddDate)", connection);

                    


                    command.Parameters.AddWithValue("@p_Guid", Guid.NewGuid());
                    command.Parameters.AddWithValue("@p_Name", txtRegName.Text);
                    command.Parameters.AddWithValue("@p_Email", txtEmail.Text);
                    command.Parameters.AddWithValue("@p_Password", txtRegPassword.Password);
                    command.Parameters.AddWithValue("@p_Phone", txtPhone.Text);
                    command.Parameters.AddWithValue("@p_IsValid", false);
                    command.Parameters.AddWithValue("@p_AddBy", "AkashPrasad");
                    command.Parameters.AddWithValue("@p_AddDate", DateTime.Now);
                    command.ExecuteNonQuery();
                    connection.Close();
                    MessageControl.ShowMessage("Message", "Successfully Registered... Wait For Approval", 200, 300, Brushes.Red);
                    //MessageBox.Show("Successfully Registered... Wait For Approval");
                }

            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            
        }

        private void btnRegCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
