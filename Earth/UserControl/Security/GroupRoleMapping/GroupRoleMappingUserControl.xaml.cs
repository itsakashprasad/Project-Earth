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
    public partial class GroupRoleMappingUserControl : System.Windows.Controls.UserControl
    {
        public GroupRoleMappingUserControl()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                AttachListOfppsgroupsForMapping();
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
            if (cmbGroup.Text == "")
            {
                Error = Error + "Please select Group.\n";
            }
            if (Error != "")
            {
                //MessageBox.Show("ERROR");

                MessageControl.ShowMessage("ERROR", Error, 200, 400, Brushes.Red);
                return false;
            }
            return true;
        }
        private void cmbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lstNotAssignRole.ItemsSource = null;
                lstAssignRole.ItemsSource = null;
                if (cmbGroup.SelectedValue != null)
                {
                    GetppsgrouprolesMapping(cmbGroup.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "cmbGroup_SelectionChanged");
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void AttachListOfppsgroupsForMapping()
        {
            try
            {
                int ReturnValue = -1;
                string RetunMessage = "";
                GroupRoleMappingBL BLppsgrouprolesMapping = new GroupRoleMappingBL();
                List<MappingGroup> ListOfppsgroups = new List<MappingGroup>();
                ListOfppsgroups = BLppsgrouprolesMapping.GetgroupsForMapping(out ReturnValue, out RetunMessage);
                cmbGroup.ItemsSource = ListOfppsgroups;
                cmbGroup.SelectedValuePath = "Guid";
                cmbGroup.DisplayMemberPath = "Name";
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "AttachListOfppsgroupsForMapping");
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void GetppsgrouprolesMapping(string GroupGuid)
        {
            try
            {
                int ReturnValue = -1;
                string ReturnMessage = "";
                GroupRoleMappingBL BLppsgrouprolesMapping = new GroupRoleMappingBL();
                GroupRoleMappingEntities lstppsgrouprolesMapping = new GroupRoleMappingEntities();
                GroupRoleMappingEntities AdditionalFieldppsgrouprolesMapping = new GroupRoleMappingEntities();
                lstppsgrouprolesMapping = BLppsgrouprolesMapping.GetgrouprolesMapping(GroupGuid, out AdditionalFieldppsgrouprolesMapping, out ReturnValue, out ReturnMessage);
                if (lstppsgrouprolesMapping.NotAssignRole.Count > 0)
                {
                    lstNotAssignRole.ItemsSource = lstppsgrouprolesMapping.NotAssignRole;
                    lstNotAssignRole.SelectedValuePath = "Guid";
                    lstNotAssignRole.DisplayMemberPath = "Name";
                }
                if (lstppsgrouprolesMapping.AssignRole.Count > 0)
                {
                    lstAssignRole.ItemsSource = lstppsgrouprolesMapping.AssignRole;
                    lstAssignRole.SelectedValuePath = "Guid";
                    lstAssignRole.DisplayMemberPath = "Name";
                }
                NotAssignedCountRole.Icon = lstNotAssignRole.Items.Count;
                AssignedCountRole.Icon = lstAssignRole.Items.Count;
                if (AdditionalFieldppsgrouprolesMapping != null)
                {
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "GetppsgrouprolesMapping");
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
                bool IsAuthorized = Authorization.UserAuthorization("GroupRoleMapping.xaml", "ADD");

                if (ValidateInputs() && IsAuthorized)
                {
                    int ReturnValue = -1;
                    string RetunMessage = "";
                    List<MappingRole> ListOfAssignedRole = new List<MappingRole>();
                    GroupRoleMappingBL BLppsgrouprolesMapping = new GroupRoleMappingBL();
                    GroupRoleMappingEntities objParam = new GroupRoleMappingEntities();
                    ListOfAssignedRole = (List<MappingRole>)(lstAssignRole.ItemsSource);
                    objParam.GroupGuid = cmbGroup.SelectedValue.ToString();
                    BLppsgrouprolesMapping.UpsertgrouprolesMapping(ListOfAssignedRole, objParam, out ReturnValue, out RetunMessage);
                    if (ReturnValue == 0)
                    {
                        GetppsgrouprolesMapping(objParam.GroupGuid);
                        MessageControl.ShowMessage("SUCCESS", RetunMessage, 200, 300, Brushes.Green);
                        //MessageBox.Show("Success");

                    }
                    else
                    {
                        //MessageBox.Show("Error");

                        MessageControl.ShowMessage("ERROR", RetunMessage, 200, 300, Brushes.Red);
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //    Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "btnAdd_Click");
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
            //    String Message = "";
            //    //Window RefWindow = Class.ParentHelper.FoundParent(sender, out Message);
            //    MainWindow RefMainWindow = (MainWindow)RefWindow;
            //    RefMainWindow.GridMain.Children.Clear();
        }
    }
}
