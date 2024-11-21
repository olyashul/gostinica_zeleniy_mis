using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ptoba_svoego_vhoda_reg_2.Models;

namespace ptoba_svoego_vhoda_reg_2.Data
{
    public class ptoba_svoego_vhoda_reg_2Context : DbContext
    {
        public ptoba_svoego_vhoda_reg_2Context (DbContextOptions<ptoba_svoego_vhoda_reg_2Context> options)
            : base(options)
        {
        }

        public DbSet<ptoba_svoego_vhoda_reg_2.Models.User> User { get; set; } = default!;
    }
}
