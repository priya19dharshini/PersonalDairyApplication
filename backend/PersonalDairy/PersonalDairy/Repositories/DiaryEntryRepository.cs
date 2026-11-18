
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

    // UPDATE
    public async Task UpdateAsync(
        DiaryEntry diaryEntry,
        CancellationToken cancellationToken)
    {
        _context.DiaryEntries.Update(diaryEntry);

        await _context.SaveChangesAsync(cancellationToken);
    }


    public async Task<bool> DeleteAsync(
        int diaryEntryId,
        CancellationToken cancellationToken)
    {
        // 1. Find the existing diary entry
        var diaryEntry = await _context.DiaryEntries
            .FirstOrDefaultAsync(
                x => x.DiaryEntryId == diaryEntryId,
                cancellationToken);

        // 2. Check whether the entry exists
        if (diaryEntry == null)
        {
            return false;
        }

        // 3. Mark the entity for deletion
        _context.DiaryEntries.Remove(diaryEntry);

        // 4. Save changes into SQL Server
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

}
