
using Microsoft.EntityFrameworkCore;
using PersonalDiary.Data;
using PersonalDiary.Models;
using PersonalDiary.Repositories.Interfaces;

namespace PersonalDiary.Repositories;

public class DiaryEntryRepository : IDiaryEntryRepository
{
    private readonly ApplicationDbContext _context;

    public DiaryEntryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DiaryEntry> AddAsync(
        DiaryEntry diaryEntry,
        CancellationToken cancellationToken)
    {
        await _context.DiaryEntries.AddAsync(
            diaryEntry,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return diaryEntry;
    }

    public async Task<List<DiaryEntry>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.DiaryEntries
            .AsNoTracking()
            .OrderByDescending(x => x.EntryDate)
            .ToListAsync(cancellationToken);
    }

    // Read by ID
    public async Task<DiaryEntry?> GetByIdAsync(
        int diaryEntryId,
        CancellationToken cancellationToken)
    {
        return await _context.DiaryEntries
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.DiaryEntryId == diaryEntryId,
                cancellationToken);
    }
}
