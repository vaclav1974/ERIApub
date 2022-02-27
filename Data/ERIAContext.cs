using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ERIA.Models;

    public class ERIAContext : DbContext
    {
        public ERIAContext (DbContextOptions<ERIAContext> options)
            : base(options)
        {
        }

        public DbSet<ERIA.Models.WorkType> WorkType { get; set; }

        public DbSet<ERIA.Models.WorkTask> WorkTask { get; set; }
    }
