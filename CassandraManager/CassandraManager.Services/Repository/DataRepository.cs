using Cassandra.Mapping;
using CassandraManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CassandraManager.Services.Repository
{
    public class DataRepository
    {
        private readonly ICassandraDbContext _dbContext;

        public DataRepository(ICassandraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DataTable GetData(string tableName, List<DataListColumn> columns)
        {
            var result = new DataTable();
            foreach (var column in columns)
            {
                result.Columns.Add(column.Name);
            }
            var session = _dbContext.Session;

            var rowSet = session.Execute(BuildQuery(tableName, columns));
            var columnCount = columns.Count();
            var rows = rowSet.ToList();

            foreach (var row in rows)
            {
                var newRow = result.NewRow();
                for (int i = 0; i < columnCount; i++)
                {
                    newRow[i] = row[i]?.ToString();
                }
                result.Rows.Add(newRow);
            }

            return result;
        }

        private string BuildQuery(string tableName, IEnumerable<DataListColumn> columns)
        {
            if (columns == null)
                return $"SELECT * FROM {tableName}";

            var projections = columns.Select(x => x.Type.Contains("frozen")
            || x.Type.Contains("map")
            || x.Type.Contains("list")
            || x.Type.Contains("set")
                ? $"toJson({x.Name})"
                : x.Name);
            return $"SELECT {string.Join(",", projections)} FROM {tableName}";
        }
        public IEnumerable<DataListColumn> GetTableColumnInfo(string tableName)
        {
            var session = _dbContext.Session;
            var mapper = new Mapper(session);
            return mapper.Fetch<DataListColumn>($"SELECT column_name, type FROM system_schema.columns WHERE keyspace_name='{session.Keyspace}' AND table_name='{tableName}'")
                .ToList();
        }

        public IEnumerable<DataListIndex> GetTableIndexInfo(string tableName)
        {
            var session = _dbContext.Session;
            var mapper = new Mapper(session);
            return mapper.Fetch<DataListIndex>($"SELECT index_name, toJson(options) AS details FROM system_schema.indexes WHERE keyspace_name='{session.Keyspace}' AND table_name='{tableName}'")
                .ToList();
        }

        public DataTable GetData(string query)
        {
            var result = new DataTable();
            var session = _dbContext.Session;

            var rowSet = session.Execute(query);
            var columnCount = rowSet.Columns.Count();
            foreach (var column in rowSet.Columns)
            {
                result.Columns.Add(column.Name);
            }
            var rows = rowSet.ToList();

            foreach (var row in rows)
            {
                var newRow = result.NewRow();
                for (int i = 0; i < columnCount; i++)
                {
                    newRow[i] = row[i]?.ToString();
                }
                result.Rows.Add(newRow);
            }

            return result;
        }
    }
}
