using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Earth
{
    public static class Authorization
    {
        public static bool UserAuthorization(string PageName, string RoleType)
        {
            try
            {
                int ReturnValue = -1;
                string ReturnMessage = "";
                UserAuthorization SiteAuthorization = new UserAuthorization();
                string authorizedStatus = SiteAuthorization.UserAuthorizedForThisOperation(PageName, RoleType, out ReturnValue, out ReturnMessage);
                if (ReturnValue == 0)
                {
                    return true;
                }
                else
                {
                    MessageControl.ShowMessage("UNAUTHORIZED TO VIEW THIS PAGE", ReturnMessage, 200, 400, Brushes.Red);
                    return false;
                }


            }
            catch (Exception ex)
            {
                MessageControl.ShowMessage("EXCEPTION - AUTHORIZATION", ex.Message, 200, 400, Brushes.Red);
                return false;
            }
        }

        internal static bool UserAuthorization(string v, object mode)
        {
            throw new NotImplementedException();
        }
    }
}
