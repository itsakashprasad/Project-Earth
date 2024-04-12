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
namespace Earth
{
    public partial class PageRoleMappingUserControl : System.Windows.Controls.UserControl
    {
        public PageRoleMappingUserControl()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                AttachListOfppspagesForMapping();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "UserControl_Loaded");
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private bool ValidateInputs()
        {
            string Error = "";
            if (cmbPage.Text == "")
            {
                Error = Error + "Please select Page.\n";
            }
            if (Error != "")
            {
                MessageBox.Show(Error);

                MessageControl.ShowMessage("ERROR", Error, 200, 400, Brushes.Red);
                return false;
            }
            return true;
        }
        private void cmbPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lstNotAssignRole.ItemsSource = null;
                lstAssignRole.ItemsSource = null;
                if (cmbPage.SelectedValue != null)
                {
                    GetppspagerolesMapping(cmbPage.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "cmbPage_SelectionChanged");
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void AttachListOfppspagesForMapping()
        {
            try
            {
                int ReturnValue = -1;
                string RetunMessage = "";
                PageRoleMappingBL BLppspagerolesMapping = new PageRoleMappingBL();
                List<MappingPage> ListOfppspages = new List<MappingPage>();
                ListOfppspages = BLppspagerolesMapping.GetpagesForMapping(out ReturnValue, out RetunMessage);
                cmbPage.ItemsSource = ListOfppspages;
                cmbPage.SelectedValuePath = "Guid";
                cmbPage.DisplayMemberPath = "Name";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "AttachListOfppspagesForMapping");
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void GetppspagerolesMapping(string PageGuid)
        {
            try
            {
                int ReturnValue = -1;
                string ReturnMessage = "";
                PageRoleMappingBL BLppspagerolesMapping = new PageRoleMappingBL();
                PageRoleMappingEntities lstppspagerolesMapping = new PageRoleMappingEntities();
                PageRoleMappingEntities AdditionalFieldppspagerolesMapping = new PageRoleMappingEntities();
                lstppspagerolesMapping = BLppspagerolesMapping.GetpagerolesMapping(PageGuid, out AdditionalFieldppspagerolesMapping, out ReturnValue, out ReturnMessage);
                if (lstppspagerolesMapping.NotAssignRole.Count > 0)
                {
                    lstNotAssignRole.ItemsSource = lstppspagerolesMapping.NotAssignRole;
                    lstNotAssignRole.SelectedValuePath = "Guid";
                    lstNotAssignRole.DisplayMemberPath = "Name";
                }
                if (lstppspagerolesMapping.AssignRole.Count > 0)
                {
                    lstAssignRole.ItemsSource = lstppspagerolesMapping.AssignRole;
                    lstAssignRole.SelectedValuePath = "Guid";
                    lstAssignRole.DisplayMemberPath = "Name";
                }
                NotAssignedCountRole.Icon = lstNotAssignRole.Items.Count;
                AssignedCountRole.Icon = lstAssignRole.Items.Count;
                if (AdditionalFieldppspagerolesMapping != null)
                {
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "GetppspagerolesMapping");
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void btnForword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<MappingRole> objNotAssign = new List<MappingRole>();
                foreach (MappingRole item in lstNotAssignRole.SelectedItems)
                {
                    objNotAssign.Add(item);
                }
                if (objNotAssign.Count > 0)
                {
                    List<MappingRole> ListOfAssignedRole = new List<MappingRole>();
                    List<MappingRole> ListOfNotAssignedRole = new List<MappingRole>();
                    ListOfAssignedRole = (List<MappingRole>)(lstAssignRole.ItemsSource);
                    ListOfNotAssignedRole = (List<MappingRole>)(lstNotAssignRole.ItemsSource);
                    if (ListOfAssignedRole == null)
                    {
                        ListOfAssignedRole = new List<MappingRole>();
                    }
                    if (ListOfNotAssignedRole == null)
                    {
                        ListOfNotAssignedRole = new List<MappingRole>();
                    }
                    ListOfNotAssignedRole.RemoveAll(x => objNotAssign.Exists(y => y.Guid == x.Guid));
                    ListOfAssignedRole.AddRange(objNotAssign);
                    lstNotAssignRole.ItemsSource = null;
                    lstAssignRole.ItemsSource = null;
                    lstNotAssignRole.ItemsSource = ListOfNotAssignedRole;
                    lstNotAssignRole.SelectedValuePath = "Guid";
                    lstNotAssignRole.DisplayMemberPath = "Name";
                    lstAssignRole.ItemsSource = ListOfAssignedRole;
                    lstAssignRole.SelectedValuePath = "Guid";
                    lstAssignRole.DisplayMemberPath = "Name";
                    NotAssignedCountRole.Icon = lstNotAssignRole.Items.Count;
                    AssignedCountRole.Icon = lstAssignRole.Items.Count;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "btnForword_Click");
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void btnBackword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<MappingRole> objAssign = new List<MappingRole>();
                foreach (MappingRole item in lstAssignRole.SelectedItems)
                {
                    objAssign.Add(item);
                }
                if (objAssign.Count > 0)
                {
                    List<MappingRole> ListOfAssignedRole = new List<MappingRole>();
                    List<MappingRole> ListOfNotAssignedRole = new List<MappingRole>();
                    ListOfAssignedRole = (List<MappingRole>)(lstAssignRole.ItemsSource);
                    ListOfNotAssignedRole = (List<MappingRole>)(lstNotAssignRole.ItemsSource);
                    if (ListOfAssignedRole == null)
                    {
                        ListOfAssignedRole = new List<MappingRole>();
                    }
                    if (ListOfNotAssignedRole == null)
                    {
                        ListOfNotAssignedRole = new List<MappingRole>();
                    }
                    ListOfAssignedRole.RemoveAll(x => objAssign.Exists(y => y.Guid == x.Guid));
                    ListOfNotAssignedRole.AddRange(objAssign);
                    lstNotAssignRole.ItemsSource = null;
                    lstAssignRole.ItemsSource = null;
                    lstNotAssignRole.ItemsSource = ListOfNotAssignedRole;
                    lstNotAssignRole.SelectedValuePath = "Guid";
                    lstNotAssignRole.DisplayMemberPath = "Name";
                    lstAssignRole.ItemsSource = ListOfAssignedRole;
                    lstAssignRole.SelectedValuePath = "Guid";
                    lstAssignRole.DisplayMemberPath = "Name";
                    NotAssignedCountRole.Icon = lstNotAssignRole.Items.Count;
                    AssignedCountRole.Icon = lstAssignRole.Items.Count;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "btnBackword_Click");
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool IsAuthorized = Authorization.UserAuthorization("PageRoleMapping.xaml", "ADD");

                if (ValidateInputs() && IsAuthorized)
                {                
                int ReturnValue = -1;
                    string RetunMessage = "";
                    List<MappingRole> ListOfAssignedRole = new List<MappingRole>();
                    PageRoleMappingBL BLppspagerolesMapping = new PageRoleMappingBL();
                    PageRoleMappingEntities objParam = new PageRoleMappingEntities();
                    ListOfAssignedRole = (List<MappingRole>)(lstAssignRole.ItemsSource);
                    objParam.PageGuid = cmbPage.SelectedValue.ToString();
                    BLppspagerolesMapping.UpsertpagerolesMapping(ListOfAssignedRole, objParam, out ReturnValue, out RetunMessage);
                    if (ReturnValue == 0)
                    {
                        GetppspagerolesMapping(objParam.PageGuid);
                        //MessageBox.Show("Success");

                       MessageControl.ShowMessage("SUCCESS", RetunMessage, 200, 300, Brushes.Green);
                    }
                    else
                    {
                        //MessageBox.Show("ERROR");

                        MessageControl.ShowMessage("ERROR", RetunMessage, 200, 300, Brushes.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "btnAdd_Click");
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
            //String Message = "";
            //Window RefWindow = Class.ParentHelper.FoundParent(sender, out Message);
            //MainWindow RefMainWindow = (MainWindow)RefWindow;
            //RefMainWindow.GridMain.Children.Clear();
        }
    }
}
