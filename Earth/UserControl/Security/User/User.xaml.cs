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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Earth
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : System.Windows.Controls.UserControl
    {
        public User()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

            LoadData();
            Loaded += UserControl_Loaded;
        }
        public void LoadData()
        {
            try
            {
                GetUser getUser = new GetUser();
                List<UserEntities> UserList = getUser.GetUserFromDatabase(out string returnResult, out int returnValue);
                dtgDataGrid.ItemsSource = UserList;
            }
            catch (Exception ex)
            {
               MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var parent = this.Parent as Panel;


            if (parent != null)
            {
                parent.Children.Remove(this);
            }
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool IsAuthorized = Authorization.UserAuthorization("User.xaml", "ADD");
                if (IsAuthorized)
                {
                    CallUpsertPage("ADD", null);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }


        }

        private void ViewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool IsAuthorized = Authorization.UserAuthorization("User.xaml", "VIEW");
                if (IsAuthorized)
                {
                    UserEntities SelectedEntity = (UserEntities)dtgDataGrid.SelectedItem;
                    CallUpsertPage("VIEW", SelectedEntity);
                }
                


            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool IsAuthorized = Authorization.UserAuthorization("User.xaml", "EDIT");
                if (IsAuthorized)
                {
                    UserEntities SelectedEntity = (UserEntities)dtgDataGrid.SelectedItem;
                    CallUpsertPage("EDIT", SelectedEntity);
                }
            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }

            // SelectedEntity.Guid = new Guid().ToString();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool IsAuthorized = Authorization.UserAuthorization("User.xaml", "DELETE");
                if (IsAuthorized)
                {
                    UserEntities SelectedEntity = (UserEntities)dtgDataGrid.SelectedItem;
                    CallUpsertPage("DELETE", SelectedEntity);
                }
            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }

            // SelectedEntity.Guid = new Guid().ToString();
        }
        private void CallUpsertPage(string Mode, UserEntities SelectedEntity)
        {
            try
            {

                System.Windows.Media.Animation.DoubleAnimation animFadeIn = new DoubleAnimation();
                animFadeIn.From = 0;
                animFadeIn.To = 1;
                animFadeIn.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                CreateUsers Popup = new CreateUsers();
                Popup.Mode = Mode;
                Popup.SelectedEntity = SelectedEntity;
                Window window = new Window
                {
                    WindowStyle = WindowStyle.None,
                    WindowState = WindowState.Normal,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    AllowsTransparency = true,
                    Background = Brushes.Transparent,
                    Content = Popup,
                };
                window.BeginAnimation(Window.OpacityProperty, animFadeIn);
                window.ShowDialog();
                LoadData();

            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
    }
}

