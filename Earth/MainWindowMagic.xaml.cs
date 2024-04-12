using PrintingSetup.Class;
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
    public partial class MainWindowMagic : Window
    {

        public MainWindowMagic()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                lblUserName.Content = SiteRegistration.objRegistration.Name.ToUpper();
                GetUserMenu();
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        public void GetUserMenu()
        {
            try
            {
                int ReturnValue = -1;
                string ReturnMessage = "";
                List<UserMenuEntities> ListOfUM = new List<UserMenuEntities>();
                UserAuthorization BLUA = new UserAuthorization();
                ListOfUM = BLUA.GetUserMenu(out ReturnValue, out ReturnMessage);
                if (ReturnValue == 0)
                {

                    foreach (UserMenuEntities objUM in ListOfUM)
                    {
                        //for Master/Security
                        foreach (TreeViewItem tv in MainTreeview.Items)
                        {
                            int i = 0;
                            //for sub treeviewitems Security --> Users/groups/Pages....
                            foreach (TreeViewItem item in tv.Items)
                            {
                                string PageName = item.Name + ".xaml";
                                if (objUM.PageName == PageName)
                                {
                                    i = i + 1;
                                    item.Visibility = Visibility.Visible;
                                }

                                if (i > 0)
                                {
                                    tv.Visibility = Visibility.Visible;
                                }
                            }
                        }
                    }

                }
                else
                {
                    MessageControl.ShowMessage("ERROR ON GET USER MENU", ReturnMessage, 200, 400, Brushes.Red);
                }
            }
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION ON GET USER MENU", ex.Message, 200, 400, Brushes.Red);
            }
        }



        private void mnuCountry_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            bool IsAuthorized = Authorization.UserAuthorization("Country.xaml", "ListView");
            if (IsAuthorized)
            {
                GridMain.Children.Clear();
                CountryUserControlMagic usc = new CountryUserControlMagic();
                GridMain.Children.Add(usc);
            }
            btnToggle.IsChecked = false;
        }

        private void mnuState_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool IsAuthorized = Authorization.UserAuthorization("State.xaml", "ListView");
            if (IsAuthorized)
            {
                GridMain.Children.Clear();
                StateUserControlMagic usc = new StateUserControlMagic();

                GridMain.Children.Add(usc);
            }
            btnToggle.IsChecked = false;
        }



        private void mnuCity_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool IsAuthorized = Authorization.UserAuthorization("City.xaml", "ListView");
            if (IsAuthorized)
            {
                GridMain.Children.Clear();
                CityUserControlMagic usc = new CityUserControlMagic();

                GridMain.Children.Add(usc);
            }
            btnToggle.IsChecked = false;
        }

        private void mnuUsers_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool IsAuthorized = Authorization.UserAuthorization("User.xaml", "ListView");
            if (IsAuthorized)
            {

                GridMain.Children.Clear();
                User u = new User();
                GridMain.Children.Add(u);

            }
            btnToggle.IsChecked = false;
        }

        private void mnuGroups_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool IsAuthorized = Authorization.UserAuthorization("Groups.xaml", "ListView");
            if (IsAuthorized)
            {

                GridMain.Children.Clear();
                Groups u = new Groups();
                GridMain.Children.Add(u);
            }
            btnToggle.IsChecked = false;
        }

        private void mnuRoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool IsAuthorized = Authorization.UserAuthorization("Roles.xaml", "ListView");
            if (IsAuthorized)
            {
                GridMain.Children.Clear();
                Roles u = new Roles();
                GridMain.Children.Add(u);
            }
            btnToggle.IsChecked = false;
        }

        private void mnuPages_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool IsAuthorized = Authorization.UserAuthorization("Pages.xaml", "ListView");
            if (IsAuthorized)
            {
                GridMain.Children.Clear();
                Pages u = new Pages();
                GridMain.Children.Add(u);
            }
            btnToggle.IsChecked = false;
        }

        private void mnuGroupUsers_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool IsAuthorized = Authorization.UserAuthorization("GroupUserMapping.xaml", "ListView");
            if (IsAuthorized)
            {
                GridMain.Children.Clear();
                GroupUserMappingUserControl u = new GroupUserMappingUserControl();
                GridMain.Children.Add(u);
            }
            btnToggle.IsChecked = false;
        }

        private void mnuGroupRoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool IsAuthorized = Authorization.UserAuthorization("GroupRoleMapping.xaml", "ListView");
            if (IsAuthorized)
            {
                GridMain.Children.Clear();
                GroupRoleMappingUserControl u = new GroupRoleMappingUserControl();
                GridMain.Children.Add(u);
            }
            btnToggle.IsChecked = false;
        }

        private void mnuPageRoles_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool IsAuthorized = Authorization.UserAuthorization("PageRoleMapping.xaml", "ListView");
            if (IsAuthorized)
            {
                GridMain.Children.Clear();
                PageRoleMappingUserControl u = new PageRoleMappingUserControl();
                GridMain.Children.Add(u);
            }
            btnToggle.IsChecked = false;
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
            catch (Exception ex)
            {

                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Login login = new Login();
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
        private void mnuLogout_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
