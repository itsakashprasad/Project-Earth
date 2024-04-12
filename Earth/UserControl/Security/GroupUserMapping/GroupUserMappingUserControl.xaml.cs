using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Earth
{
    public partial class GroupUserMappingUserControl : System.Windows.Controls.UserControl
    {
        public GroupUserMappingUserControl()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                bool IsAuthorized = Authorization.UserAuthorization("GroupUserMapping.xaml", "VIEW");

                if (ValidateInputs() && IsAuthorized)
                {
                    AttachListOfppsgroupsForMapping();

                }


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
                //MessageBox.Show(Error);

                MessageControl.ShowMessage("ERROR", Error, 200, 400, Brushes.Red);
                return false;
            }
            return true;
        }
        private void cmbGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lstNotAssignUser.ItemsSource = null;
                lstAssignUser.ItemsSource = null;
                if (cmbGroup.SelectedValue != null)
                {
                    GetppsgroupusersMapping(cmbGroup.SelectedValue.ToString());
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
                GroupUserMappingBL BLppsgroupusersMapping = new GroupUserMappingBL();
                List<MappingGroup> ListOfppsgroups = new List<MappingGroup>();
                ListOfppsgroups = BLppsgroupusersMapping.GetgroupsForMapping(out ReturnValue, out RetunMessage);

                //if (BL.Classes.SiteRegistration.objRegistration.GroupName != "Admin")
                //{
                //    ListOfppsgroups = ListOfppsgroups.Where(x => x.Name != "Admin").ToList();
                //}


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
        private void GetppsgroupusersMapping(string GroupGuid)
        {
            try
            {
                int ReturnValue = -1;
                string ReturnMessage = "";
                GroupUserMappingBL BLppsgroupusersMapping = new GroupUserMappingBL();
                GroupUserMappingEntities lstppsgroupusersMapping = new GroupUserMappingEntities();
                GroupUserMappingEntities AdditionalFieldppsgroupusersMapping = new GroupUserMappingEntities();
                lstppsgroupusersMapping = BLppsgroupusersMapping.GetgroupusersMapping(GroupGuid, out AdditionalFieldppsgroupusersMapping, out ReturnValue, out ReturnMessage);
                if (lstppsgroupusersMapping.NotAssignUser.Count > 0)
                {
                    lstNotAssignUser.ItemsSource = lstppsgroupusersMapping.NotAssignUser;
                    lstNotAssignUser.SelectedValuePath = "Guid";
                    lstNotAssignUser.DisplayMemberPath = "Name";
                }
                if (lstppsgroupusersMapping.AssignUser.Count > 0)
                {
                    lstAssignUser.ItemsSource = lstppsgroupusersMapping.AssignUser;
                    lstAssignUser.SelectedValuePath = "Guid";
                    lstAssignUser.DisplayMemberPath = "Name";
                }
                NotAssignedCountUser.Icon = lstNotAssignUser.Items.Count;
                AssignedCountUser.Icon = lstAssignUser.Items.Count;
                if (AdditionalFieldppsgroupusersMapping != null)
                {
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);

                //Class.Global.WPFLog("EXCEPTION", ex.Message + " " + ex.InnerException, this.GetType().Name, "GetppsgroupusersMapping");
                MessageControl.ShowMessage("EXCEPTION", ex.Message, 200, 300, Brushes.Red);
            }
        }
        private void btnForword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<MappingUser> objNotAssign = new List<MappingUser>();
                foreach (MappingUser item in lstNotAssignUser.SelectedItems)
                {
                    objNotAssign.Add(item);
                }
                if (objNotAssign.Count > 0)
                {
                    List<MappingUser> ListOfAssignedUser = new List<MappingUser>();
                    List<MappingUser> ListOfNotAssignedUser = new List<MappingUser>();
                    ListOfAssignedUser = (List<MappingUser>)(lstAssignUser.ItemsSource);
                    ListOfNotAssignedUser = (List<MappingUser>)(lstNotAssignUser.ItemsSource);
                    if (ListOfAssignedUser == null)
                    {
                        ListOfAssignedUser = new List<MappingUser>();
                    }
                    if (ListOfNotAssignedUser == null)
                    {
                        ListOfNotAssignedUser = new List<MappingUser>();
                    }
                    ListOfNotAssignedUser.RemoveAll(x => objNotAssign.Exists(y => y.Guid == x.Guid));
                    ListOfAssignedUser.AddRange(objNotAssign);
                    lstNotAssignUser.ItemsSource = null;
                    lstAssignUser.ItemsSource = null;
                    lstNotAssignUser.ItemsSource = ListOfNotAssignedUser;
                    lstNotAssignUser.SelectedValuePath = "Guid";
                    lstNotAssignUser.DisplayMemberPath = "Name";
                    lstAssignUser.ItemsSource = ListOfAssignedUser;
                    lstAssignUser.SelectedValuePath = "Guid";
                    lstAssignUser.DisplayMemberPath = "Name";
                    NotAssignedCountUser.Icon = lstNotAssignUser.Items.Count;
                    AssignedCountUser.Icon = lstAssignUser.Items.Count;
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
                List<MappingUser> objAssign = new List<MappingUser>();
                foreach (MappingUser item in lstAssignUser.SelectedItems)
                {
                    objAssign.Add(item);
                }
                if (objAssign.Count > 0)
                {
                    List<MappingUser> ListOfAssignedUser = new List<MappingUser>();
                    List<MappingUser> ListOfNotAssignedUser = new List<MappingUser>();
                    ListOfAssignedUser = (List<MappingUser>)(lstAssignUser.ItemsSource);
                    ListOfNotAssignedUser = (List<MappingUser>)(lstNotAssignUser.ItemsSource);
                    if (ListOfAssignedUser == null)
                    {
                        ListOfAssignedUser = new List<MappingUser>();
                    }
                    if (ListOfNotAssignedUser == null)
                    {
                        ListOfNotAssignedUser = new List<MappingUser>();
                    }
                    ListOfAssignedUser.RemoveAll(x => objAssign.Exists(y => y.Guid == x.Guid));
                    ListOfNotAssignedUser.AddRange(objAssign);
                    lstNotAssignUser.ItemsSource = null;
                    lstAssignUser.ItemsSource = null;
                    lstNotAssignUser.ItemsSource = ListOfNotAssignedUser;
                    lstNotAssignUser.SelectedValuePath = "Guid";
                    lstNotAssignUser.DisplayMemberPath = "Name";
                    lstAssignUser.ItemsSource = ListOfAssignedUser;
                    lstAssignUser.SelectedValuePath = "Guid";
                    lstAssignUser.DisplayMemberPath = "Name";
                    NotAssignedCountUser.Icon = lstNotAssignUser.Items.Count;
                    AssignedCountUser.Icon = lstAssignUser.Items.Count;
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
                bool IsAuthorized = Authorization.UserAuthorization("GroupUserMapping.xaml", "ADD");

                if (ValidateInputs() && IsAuthorized)
                {
                    int ReturnValue = -1;
                    string RetunMessage = "";
                    List<MappingUser> ListOfAssignedUser = new List<MappingUser>();
                    GroupUserMappingBL BLppsgroupusersMapping = new GroupUserMappingBL();
                    GroupUserMappingEntities objParam = new GroupUserMappingEntities();
                    ListOfAssignedUser = (List<MappingUser>)(lstAssignUser.ItemsSource);
                    objParam.GroupGuid = cmbGroup.SelectedValue.ToString();
                    BLppsgroupusersMapping.UpsertgroupusersMapping(ListOfAssignedUser, objParam, out ReturnValue, out RetunMessage);
                    if (ReturnValue == 0)
                    {
                        GetppsgroupusersMapping(objParam.GroupGuid);
                        MessageControl.ShowMessage("SUCCESS", RetunMessage, 200, 300, Brushes.Green);
                        //MessageBox.Show("Success");

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
