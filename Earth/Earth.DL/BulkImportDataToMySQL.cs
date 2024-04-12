using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Earth
{
    public class MySQLBulkInsertData : List<object[]>
    {
        public struct MySQLFieldDefinition
        {
            public MySQLFieldDefinition(string field, MySqlDbType type)
                : this()
            {
                FieldName = field;
                ParameterType = type;
            }

            public string FieldName { get; private set; }
            public MySqlDbType ParameterType { get; private set; }
        }
        public MySQLBulkInsertData(params MySQLFieldDefinition[] fieldnames)
        {
            Fields = fieldnames;
        }
        public MySQLFieldDefinition[] Fields { get; private set; }
    }

    public static class BulkImportDataToMySQL
    {
        
        public static string ToCSV<T>(this IEnumerable<T> intValues, string separator = ",", string encloser = "")
        {
            string result = String.Empty;
            foreach (T value in intValues)
            {
                result = String.IsNullOrEmpty(result)
                    ? string.Format("{1}{0}{1}", value, encloser)
                    : String.Format("{0}{1}{3}{2}{3}", result, separator, value, encloser);
            }
            return result;
        }
        
    }
}
