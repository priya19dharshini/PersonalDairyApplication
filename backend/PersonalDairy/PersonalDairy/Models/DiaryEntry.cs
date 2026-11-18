
using System.ComponentModel.DataAnnotations;

namespace PersonalDiary.Models;

public class DiaryEntry
{
    public int DiaryEntryId { get; set; }

    [Required]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(5000)]
    public string Content { get; set; } = string.Empty;

    [Required]
    public DateTime EntryDate { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;
}
