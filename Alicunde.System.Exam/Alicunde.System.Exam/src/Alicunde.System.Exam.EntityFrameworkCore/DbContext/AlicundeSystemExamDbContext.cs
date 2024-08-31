using Alicunde.System.Exam.Domain;
using Microsoft.EntityFrameworkCore;

namespace Alicunde.System.Exam.EntityFrameworkCore.DbContext;

public class AlicundeSystemExamDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    #region DbSets

    public DbSet<Bank> Banks { get; set; } = null!;

    #endregion
    
    public AlicundeSystemExamDbContext(DbContextOptions<AlicundeSystemExamDbContext> options) 
        : base(options)
    {
    }
}