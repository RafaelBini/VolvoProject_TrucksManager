#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using trucks_backend.Model;

namespace trucks_backend.Data
{
    public class trucks_backendContext : DbContext
    {
        public trucks_backendContext (DbContextOptions<trucks_backendContext> options)
            : base(options)
        {
        }

        public DbSet<trucks_backend.Model.Truck> Truck { get; set; }
    }
}
