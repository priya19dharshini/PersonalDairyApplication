
using PersonalDiary.DTOs.DiaryEntries;

namespace PersonalDiary.Services.Interfaces;

public interface IDiaryEntryService
{
    Task<DiaryEntryResponse> CreateAsync(
        CreateDiaryEntryRequest request,
        CancellationToken cancellationToken);

    Task<List<DiaryEntryResponse>> GetAllAsync(
    CancellationToken cancellationToken);
}
