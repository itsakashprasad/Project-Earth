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
    /// Interaction logic for StateUserControlMagic.xaml
    /// </summary>
    public partial class StateUserControlMagic : System.Windows.Controls.UserControl
    {
        public StateUserControlMagic()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            try
            {
                GetStates getStates = new GetStates();
                List<StateEntities> StatesList = getStates.GetStatesFromDatabase("SELECT", out string returnResult, out int returnValue);
                dtgDataGrid.ItemsSource = StatesList;
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
                bool IsAuthorized = Authorization.UserAuthorization("State.xaml", "ADD");
                if (IsAuthorized)
                {
                    StateEntities SelectedEntity = new StateEntities();
                    CallUpsertPage("ADD", SelectedEntity);
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
                bool IsAuthorized = Authorization.UserAuthorization("State.xaml", "VIEW");
                if (IsAuthorized)
                {
                    StateEntities SelectedEntity = (StateEntities)dtgDataGrid.SelectedItem;
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
            bool IsAuthorized = Authorization.UserAuthorization("State.xaml", "EDIT");
            if (IsAuthorized)
            {
                StateEntities SelectedEntity = (StateEntities)dtgDataGrid.SelectedItem;
                CallUpsertPage("EDIT", SelectedEntity);
            }
            
            // SelectedEntity.Guid = new Guid().ToString();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            bool IsAuthorized = Authorization.UserAuthorization("State.xaml", "DELETE");
            if (IsAuthorized)
            {
                StateEntities SelectedEntity = (StateEntities)dtgDataGrid.SelectedItem;
                CallUpsertPage("DELETE", SelectedEntity);
            }
            
            // SelectedEntity.Guid = new Guid().ToString();
        }
        private void CallUpsertPage(string Mode, StateEntities SelectedEntity)
        {
            try
            {

                System.Windows.Media.Animation.DoubleAnimation animFadeIn = new DoubleAnimation();
                animFadeIn.From = 0;
                animFadeIn.To = 1;
                animFadeIn.Duration = new Duration(TimeSpan.FromSeconds(0.5));
                StatePopupMagic Popup = new StatePopupMagic();
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
