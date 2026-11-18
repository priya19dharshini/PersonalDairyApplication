
using PersonalDiary.DTOs.DiaryEntries;

namespace PersonalDiary.Services.Interfaces;

public interface IDiaryEntryService
{
    Task<DiaryEntryResponse> CreateAsync(
        CreateDiaryEntryRequest request,
        CancellationToken cancellationToken);

    Task<List<DiaryEntryResponse>> GetAllAsync(
    CancellationToken cancellationToken);

    Task<DiaryEntryResponse?> GetByIdAsync(
        int diaryEntryId,
        CancellationToken cancellationToken);

    Task<DiaryEntryResponse?> UpdateAsync(
        int diaryEntryId,
        UpdateDiaryEntryRequest request,
        CancellationToken cancellationToken);

    Task<bool> DeleteAsync(
    int diaryEntryId,
    CancellationToken cancellationToken);
}
