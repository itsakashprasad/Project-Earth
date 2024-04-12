using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth
{
    public class UserEntities
    {
        public string Mode { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? AllowAccessFrom { get; set; }
        public DateTime? AllowAccessTill { get; set; }

        public bool IsValid { get; set; }
        public bool IsDeleted { get; set; }
        public string AddBy { get; set; }
        public DateTime? AddDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public List<RolesEntities> Roles { get; internal set; }
    }
}
