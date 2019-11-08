using Microsoft.EntityFrameworkCore;

namespace ScrabbleScoreAPI.Models
{
    public class ScoringValueSetContext : DbContext
    {
        public ScoringValueSetContext(DbContextOptions<ScoringValueSetContext> options)
            : base(options)
        {
        }

    }
}