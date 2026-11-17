
using PersonalDiary.DTOs.DiaryEntries;
using PersonalDiary.Models;
using PersonalDiary.Repositories.Interfaces;
using PersonalDiary.Services.Interfaces;

namespace PersonalDiary.Services;

public class DiaryEntryService : IDiaryEntryService
{
    private readonly IDiaryEntryRepository _diaryEntryRepository;

    public DiaryEntryService(
        IDiaryEntryRepository diaryEntryRepository)
    {
        _diaryEntryRepository = diaryEntryRepository;
    }

    public async Task<DiaryEntryResponse> CreateAsync(
        CreateDiaryEntryRequest request,
        CancellationToken cancellationToken)
    {
        // 1. Business validation
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ArgumentException(
                "Diary title cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(request.Content))
        {
            throw new ArgumentException(
                "Diary content cannot be empty.");
        }

        // 2. Convert DTO into Model
        var diaryEntry = new DiaryEntry
        {
            Title = request.Title.Trim(),
            Content = request.Content.Trim(),
            EntryDate = request.EntryDate!.Value
        };

        // 3. Call Repository
        var savedEntry = await _diaryEntryRepository.AddAsync(
            diaryEntry,
            cancellationToken);

        // 4. Convert Model into Response DTO
        var response = new DiaryEntryResponse
        {
            DiaryEntryId = savedEntry.DiaryEntryId,
            Title = savedEntry.Title,
            Content = savedEntry.Content,
            EntryDate = savedEntry.EntryDate
        };

        return response;
    }

    public async Task<List<DiaryEntryResponse>> GetAllAsync(
    CancellationToken cancellationToken)
    {
        var diaryEntries = await _diaryEntryRepository.GetAllAsync(
            cancellationToken);

        return diaryEntries.Select(entry => new DiaryEntryResponse
        {
            DiaryEntryId = entry.DiaryEntryId,
            Title = entry.Title,
            Content = entry.Content,
            EntryDate = entry.EntryDate
        }).ToList();
    }
}
