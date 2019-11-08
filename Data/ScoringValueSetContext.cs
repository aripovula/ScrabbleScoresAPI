using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScrabbleScoreAPI.Models;

    public class ScoringValueSetContext : DbContext
    {
        public ScoringValueSetContext (DbContextOptions<ScoringValueSetContext> options)
            : base(options)
        {
        }

        public DbSet<ScrabbleScoreAPI.Models.ScoringValueSet> ScoringValueSet { get; set; }
    }
