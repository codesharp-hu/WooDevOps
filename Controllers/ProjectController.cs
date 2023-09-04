using Microsoft.AspNetCore.Mvc;
using BashScriptRunner.Service;

namespace BashScriptRunner.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectController : ControllerBase
{
    private ProjectService projectService;

    public ProjectController(ProjectService projectService)
    {
        this.projectService = projectService;
    }

    [HttpGet]
    [Route("")]
    public IActionResult ListProjects()
    {
        var projects = projectService.ListProjects();
        return Ok(projects);
    }
    
    [HttpGet]
    [Route("{projectId}/runs")]
    public IActionResult GetProjectRuns(int projectId)
    {
        var runs = projectService.GetProjectRuns(projectId);
        return Ok(runs);
    }

    [HttpPost]
    [Route("{projectId}/start")]
    public IActionResult RunProject(int projectId)
    {
        projectService.RunProject(projectId);
        return Ok();
    }
}
