using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class App1Context : DbContext
    {
        public App1Context(DbContextOptions<App1Context> options)
            : base(options)
        {
        }

        public DbSet<App1Item> App1Items { get; set; }
    }
}
