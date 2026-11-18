
using Microsoft.EntityFrameworkCore;
using PersonalDiary.Models;

namespace PersonalDiary.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<DiaryEntry> DiaryEntries { get; set; }

}
