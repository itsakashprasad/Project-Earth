using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth
{
   public class UsersEntity
    {
        public int RowIndex { get; set; }
        public String Guid { get; set; }
        public string GroupName { get; set; }
        public String Name { get; set; }
        public String EmailId { get; set; }
        public String Password { get; set; }
        public String PhoneNo { get; set; }       
        public DateTime AllowAccessFrom { get; set; }
        public DateTime AllowAccessTill { get; set; }
        public DateTime ActivationDate { get; set; }        
        public bool IsValid { get; set; }
        public bool IsDeleted { get; set; }
        public String AddBy { get; set; }
        public DateTime AddDate { get; set; }
        public String UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

        public List<Roles> Roles { get; set; }
        public UsersEntity()
        {
            Roles = new List<Roles>();
        }
    }
}
