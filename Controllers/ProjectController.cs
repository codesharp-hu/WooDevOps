using Microsoft.AspNetCore.Mvc;
using BashScriptRunner.Service;

namespace BashScriptRunner.Controllers;

[ApiController]
[Route("api")]
public class ProjectController : ControllerBase
{
    private ProjectService projectService;

    public ProjectController(ProjectService projectService)
    {
        this.projectService = projectService;
    }

    [HttpGet]
    [Route("projects")]
    public IActionResult ListProjects()
    {
        var projects = projectService.ListProjects();
        return Ok(projects);
    }
}
