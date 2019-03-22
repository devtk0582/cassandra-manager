using Cassandra.Mapping;
using CassandraManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CassandraManager.Services.Repository
{
    public class TableRepository
    {
        private readonly ICassandraDbContext _dbContext;

        public TableRepository(ICassandraDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<string> GetTables()
        {
            var mapper = new Mapper(_dbContext.Session);
            return mapper.Fetch<string>(Queries.GET_TABLE_NAMES_BY_KEYSPACE_NAME, _dbContext.Session.Keyspace).ToList();
        }
    }
}
