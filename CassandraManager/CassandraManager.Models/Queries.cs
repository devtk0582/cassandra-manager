using System;

namespace CassandraManager.Models
{
    public static class Queries
    {
        public const string GET_TABLE_NAMES_BY_KEYSPACE_NAME = "SELECT table_name FROM system_schema.tables WHERE keyspace_name = ? ";
    }
}
