using CassandraManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CassandraManager.App
{
    public static class AppContext
    {
        public static ICassandraDbContext Instance { get; set; }
    }
}
