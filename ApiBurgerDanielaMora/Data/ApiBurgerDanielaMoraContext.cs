using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiBurgerDanielaMora.Models;

namespace ApiBurgerDanielaMora.Data
{
    public class ApiBurgerDanielaMoraContext : DbContext
    {
        public ApiBurgerDanielaMoraContext (DbContextOptions<ApiBurgerDanielaMoraContext> options)
            : base(options)
        {
        }

        public DbSet<ApiBurgerDanielaMora.Models.Burger> Burger { get; set; } = default!;
    }
}
