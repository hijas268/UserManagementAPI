using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")] // only Admins can access
public class AuditTrailController : ControllerBase
{
    private readonly IAuditTrailService _auditTrailService;

    public AuditTrailController(IAuditTrailService auditTrailService)
    {
        _auditTrailService = auditTrailService;
    }

    [HttpGet]
    public async Task<IActionResult> GetLogs([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var (logs, totalCount) = await _auditTrailService.GetLogsAsync(search, page, pageSize);
        return Ok(new { data = logs, totalCount });
    }
}
