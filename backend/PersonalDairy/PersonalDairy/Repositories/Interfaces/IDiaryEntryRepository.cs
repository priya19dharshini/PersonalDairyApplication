
using PersonalDiary.Models;

namespace PersonalDiary.Repositories.Interfaces;

public interface IDiaryEntryRepository
{
    Task<DiaryEntry> AddAsync(
        DiaryEntry diaryEntry,
        CancellationToken cancellationToken);

    Task<List<DiaryEntry>> GetAllAsync(
       CancellationToken cancellationToken);

    Task<DiaryEntry?> GetByIdAsync(
       int diaryEntryId,
       CancellationToken cancellationToken);

    Task UpdateAsync(
       DiaryEntry diaryEntry,
       CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
    int diaryEntryId,
    CancellationToken cancellationToken);
}
