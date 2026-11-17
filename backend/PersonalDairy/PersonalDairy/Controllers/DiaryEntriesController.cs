
using Microsoft.AspNetCore.Mvc;
using PersonalDiary.DTOs.DiaryEntries;
using PersonalDiary.Services.Interfaces;

namespace PersonalDiary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiaryEntriesController : ControllerBase
{
    private readonly IDiaryEntryService _diaryEntryService;

    public DiaryEntriesController(
        IDiaryEntryService diaryEntryService)
    {
        _diaryEntryService = diaryEntryService;
    }

    [HttpPost]
    public async Task<ActionResult<DiaryEntryResponse>> Create(
        [FromBody] CreateDiaryEntryRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _diaryEntryService.CreateAsync(
            request,
            cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            response);
    }

    [HttpGet]
    public async Task<ActionResult<List<DiaryEntryResponse>>> GetAll(
       CancellationToken cancellationToken)
    {
        var response = await _diaryEntryService.GetAllAsync(
            cancellationToken);

        return Ok(response);
    }

    // GET: api/DiaryEntries/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DiaryEntryResponse>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var response = await _diaryEntryService.GetByIdAsync(
            id,
            cancellationToken);

        if (response == null)
        {
            return NotFound(new
            {
                message = $"Diary entry with ID {id} was not found."
            });
        }

        return Ok(response);
    }
}
