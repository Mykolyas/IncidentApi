using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class IncidentController : ControllerBase
{
    private readonly IIncidentService _incidentService;

    public IncidentController(IIncidentService incidentService)
    {
        _incidentService = incidentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IncidentRequestDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var incidentName = await _incidentService.CreateIncidentAsync(request);
            return Ok(new { incidentName });
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Account not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Server error: " + ex.Message);
        }
    }
}
