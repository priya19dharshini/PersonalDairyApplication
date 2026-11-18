using Microsoft.AspNetCore.Identity;

namespace PersonalDiary.Models;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public ICollection<DiaryEntry> DiaryEntries { get; set; }
        = new List<DiaryEntry>();
}