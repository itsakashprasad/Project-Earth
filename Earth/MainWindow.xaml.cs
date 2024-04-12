using Earth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string UserName { get; set; }

        public MainWindow(string username)
        {
            InitializeComponent();
            //  UsernameLabel.Content = $"Welcome, {username}!";
        }

        private void MenuCountry_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //CountryUserControl usc = new CountryUserControl();
                GridMain.Children.Clear();
                CountryUserControlMagic usc = new CountryUserControlMagic();
                GridMain.Children.Add(usc);
                btnToggle.IsChecked = false;
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }

            

        }

        private void MenuState_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GridMain.Children.Clear();
                //StateUserControl usc = new StateUserControl();
                StateUserControlMagic usc = new StateUserControlMagic();

                GridMain.Children.Add(usc);
                btnToggle.IsChecked = false;
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            



        }

        private void MenuCity_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GridMain.Children.Clear();
                //CityUserControl usc = new CityUserControl();
                CityUserControlMagic usc = new CityUserControlMagic();

                GridMain.Children.Add(usc);
                btnToggle.IsChecked = false;
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            


        }

        private void MenuLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginWindow login = new LoginWindow();
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
           
        }

        private void ApplicationClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();

            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            
        }

        //private void MenuLogout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    LoginWindow login = new LoginWindow();
        //    login.Show();
        //    this.Close();
        //}

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UsernameLabel.Content = $"Welcome, {UserName}!";
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            
        }

        private void mnuUsers_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GridMain.Children.Clear();
                User u = new User();
                // WorkinProgress u = new WorkinProgress();

                GridMain.Children.Add(u);
                btnToggle.IsChecked = false;
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            

        
        }

        private void mnuGroups_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GridMain.Children.Clear();
                Groups u = new Groups();
                GridMain.Children.Add(u);
                btnToggle.IsChecked = false;
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            

        }

        private void mnuRoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GridMain.Children.Clear();
                Roles u = new Roles();
                GridMain.Children.Add(u);
                btnToggle.IsChecked = false;
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
           

        }

        private void mnuGroupUsers_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GridMain.Children.Clear();
                GroupUserMappingUserControl u = new GroupUserMappingUserControl();
                GridMain.Children.Add(u);
                btnToggle.IsChecked = false;
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            

        }

        private void mnuPageRoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GridMain.Children.Clear();
                PageRoleMappingUserControl u = new PageRoleMappingUserControl();
                GridMain.Children.Add(u);
                btnToggle.IsChecked = false;
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            

        }

        private void mnuPages_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GridMain.Children.Clear();
                Pages u = new Pages();
                GridMain.Children.Add(u);
                btnToggle.IsChecked = false;
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            

        }

        private void logout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GridMain.Children.Clear();
                WorkinProgress u = new WorkinProgress();
                GridMain.Children.Add(u);
                btnToggle.IsChecked = false;
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
           

        }

        private void mnuGroupRoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                GridMain.Children.Clear();
                GroupRoleMappingUserControl u = new GroupRoleMappingUserControl();
                GridMain.Children.Add(u);
                btnToggle.IsChecked = false;
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
           
        }
        private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ThemeToggleButton.IsChecked == true)
                {
                    // Apply dark theme 
                    ResourceDictionary darkTheme = new ResourceDictionary();
                    darkTheme.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml", UriKind.RelativeOrAbsolute);
                    Application.Current.Resources.MergedDictionaries.Add(darkTheme);
                }
                else
                {
                    // Apply light theme
                    ResourceDictionary lightTheme = new ResourceDictionary();
                    lightTheme.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml", UriKind.RelativeOrAbsolute);
                    Application.Current.Resources.MergedDictionaries.Add(lightTheme);
                }
            }
            catch (Exception ex )
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
           
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginWindow login = new LoginWindow();
                login.Show();
                this.Close();
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
            
        }
    }
}
