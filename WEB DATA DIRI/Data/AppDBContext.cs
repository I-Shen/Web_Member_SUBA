using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WEB_DATA_DIRI.Models;

namespace WEB_DATA_DIRI.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext (DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<WEB_DATA_DIRI.Models.DDiri> DDiri { get; set; } = default!;
    }
}
