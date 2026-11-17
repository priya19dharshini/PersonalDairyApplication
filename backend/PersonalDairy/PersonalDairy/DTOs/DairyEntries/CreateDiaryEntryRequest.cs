
using System.ComponentModel.DataAnnotations;

namespace PersonalDiary.DTOs.DiaryEntries;

public class CreateDiaryEntryRequest
{
    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(5000)]
    public string Content { get; set; } = string.Empty;

    [Required]
    public DateTime? EntryDate { get; set; }
}
