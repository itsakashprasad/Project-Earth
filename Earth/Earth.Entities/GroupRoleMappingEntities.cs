using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth
{
   public class GroupRoleMappingEntities
    {
        public String GroupGuid { get; set; }
        public List<MappingRole> AssignRole { get; set; }
        public List<MappingRole> NotAssignRole { get; set; }
        public GroupRoleMappingEntities()
        {
            AssignRole = new List<MappingRole>();
            NotAssignRole = new List<MappingRole>();
        }
    }
    public class MappingGroup
    {
        public string Guid { get; set; }
        public String Name { get; set; }
    }
    public class MappingRole
    {
        public string Guid { get; set; }
        public String Name { get; set; }
    }
}
