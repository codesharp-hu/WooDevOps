using Microsoft.AspNetCore.Mvc;
using WooDevOps.Web.Service;

namespace WooDevOps.Web.Controllers;

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
}
