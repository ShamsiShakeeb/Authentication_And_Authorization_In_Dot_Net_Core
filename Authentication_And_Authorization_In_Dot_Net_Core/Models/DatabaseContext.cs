using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication_And_Authorization_In_Dot_Net_Core.Models
{
    public class DatabaseContext :DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Employee> employee { get; set; }
        public DbSet<Boss> boss { get; set; }
    }
}
