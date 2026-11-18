
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

    // Read by ID
    public async Task<DiaryEntryResponse?> GetByIdAsync(
        int diaryEntryId,
        CancellationToken cancellationToken)
    {
        if (diaryEntryId <= 0)
        {
            return null;
        }

        var diaryEntry = await _diaryEntryRepository.GetByIdAsync(
            diaryEntryId,
            cancellationToken);

        if (diaryEntry == null)
        {
            return null;
        }

        return MapToResponse(diaryEntry);
    }

    // UPDATE
    public async Task<DiaryEntryResponse?> UpdateAsync(
        int diaryEntryId,
        UpdateDiaryEntryRequest request,
        CancellationToken cancellationToken)
    {
        if (diaryEntryId <= 0)
        {
            return null;
        }

        // 1. Find existing diary entry
        var diaryEntry = await _diaryEntryRepository.GetByIdAsync(
            diaryEntryId,
            cancellationToken);

        // 2. Check whether it exists
        if (diaryEntry == null)
        {
            return null;
        }

        // 3. Update existing properties
        diaryEntry.Title = request.Title.Trim();

        diaryEntry.Content = request.Content.Trim();

        diaryEntry.EntryDate = request.EntryDate!.Value;

        // 4. Save changes
        await _diaryEntryRepository.UpdateAsync(
            diaryEntry,
            cancellationToken);

        // 5. Return updated DTO
        return MapToResponse(diaryEntry);
    }

    // Entity to DTO mapping
    private static DiaryEntryResponse MapToResponse(
        DiaryEntry diaryEntry)
    {
        return new DiaryEntryResponse
        {
            DiaryEntryId = diaryEntry.DiaryEntryId,
            Title = diaryEntry.Title,
            Content = diaryEntry.Content,
            EntryDate = diaryEntry.EntryDate
        };
    }


    public async Task<bool> DeleteAsync(
        int diaryEntryId,
        CancellationToken cancellationToken)
    {
        // Validate ID
        if (diaryEntryId <= 0)
        {
            return false;
        }

        // Delegate deletion to repository
        return await _diaryEntryRepository.DeleteAsync(
            diaryEntryId,
            cancellationToken);
    }

}
