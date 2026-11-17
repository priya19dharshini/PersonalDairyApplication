
using PersonalDiary.Models;

namespace PersonalDiary.Repositories.Interfaces;

public interface IDiaryEntryRepository
{
    Task<DiaryEntry> AddAsync(
        DiaryEntry diaryEntry,
        CancellationToken cancellationToken);
}
