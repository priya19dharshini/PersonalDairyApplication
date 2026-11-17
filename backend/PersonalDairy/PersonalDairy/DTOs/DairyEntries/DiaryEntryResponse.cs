
namespace PersonalDiary.DTOs.DiaryEntries;

public class DiaryEntryResponse
{
    public int DiaryEntryId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTime EntryDate { get; set; }
}
