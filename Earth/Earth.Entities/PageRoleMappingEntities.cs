using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth
{
    public class PageRoleMappingEntities
    {
        public String PageGuid { get; set; }
        public List<MappingRole> AssignRole { get; set; }
        public List<MappingRole> NotAssignRole { get; set; }
        public PageRoleMappingEntities()
        {
            AssignRole = new List<MappingRole>();
            NotAssignRole = new List<MappingRole>();
        }
    }
    public class MappingPage
    {
        public string Guid { get; set; }
        public String Name { get; set; }
    }
}
