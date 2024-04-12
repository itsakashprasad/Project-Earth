using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth
{
    public class GroupUserMappingEntities
    {
        public String GroupGuid { get; set; }
        public List<MappingUser> AssignUser { get; set; }
        public List<MappingUser> NotAssignUser { get; set; }
        public GroupUserMappingEntities()
        {
            AssignUser = new List<MappingUser>();
            NotAssignUser = new List<MappingUser>();
        }
    }
    public class MappingUser
    {
        public string Guid { get; set; }
        public String Name { get; set; }
    }
}
