using Cassandra.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CassandraManager.Models
{
    public class DataListColumn
    {
        [Column("column_name")]
        public string Name { get; set; }
        public string Type { get; set; }

        public DataListColumn() { }

        public DataListColumn(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
