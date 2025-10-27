using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcExercise.Models;

namespace MvcExercise.Data
{
    public class MvcExerciseContext : DbContext
    {
        public MvcExerciseContext (DbContextOptions<MvcExerciseContext> options)
            : base(options)
        {
        }

        public DbSet<MvcExercise.Models.SimpleDataSet>? SimpleDataSet { get; set; }
        public DbSet<MvcExercise.Models.StatusValues>? StatusValues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SimpleDataSet>()
                .HasIndex(s => s.Name)
                .IsUnique();

            builder.Entity<SimpleDataSet>()
                .Property(x => x.UpdateTimeStamp)
                .HasDefaultValueSql("GETDATE()");
        }

    }
}
