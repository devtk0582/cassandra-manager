using Cassandra.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CassandraManager.Models
{
    public class DataListIndex
    {
        [Column("index_name")]
        public string Name { get; set; }
        public string Details { get; set; }
    }
}
