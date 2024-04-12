using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth
{
    public class CountryEntities
    {
        public string Mode { get; set; }
        public string Guid { get; set; }
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public bool IsValid { get; set; }
        public bool IsDeleted { get; set; }
        public string AddBy { get; set; }
        public DateTime? AddDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
