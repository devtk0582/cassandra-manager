using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CassandraManager.Services
{
    public interface ICassandraDbContext : IDisposable
    {
        ISession Session { get; }
        string Host { get; }
    }

    public class CassandraDbContext : ICassandraDbContext
    {
        public ISession Session { get; }
        public string Host => string.Join(",", Session?.Cluster?.AllHosts()?.Select(x => x.Address.ToString()) ?? Enumerable.Empty<string>());

        public CassandraDbContext(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            var cluster = Cluster.Builder()
                .WithConnectionString(connectionString)
                .Build();

            Session = cluster.Connect();
        }

        public void Dispose() => Session?.Dispose();
    }
}
