using System.Threading.Channels;
using BashScriptRunner.Service;
using Microsoft.AspNetCore.Mvc;

namespace BashScriptRunner.Controllers;

[ApiController]
[Route("api/pipelines")]
public class PipelineController : ControllerBase
{
    private readonly Channel<Pipeline> channel;
    private PipelineService pipelineService;

    public PipelineController(Channel<Pipeline> channel, PipelineService pipelineService)
    {
        this.channel = channel;
        this.pipelineService = pipelineService;
    }

    [HttpPost]
    [Route("{pipelineId}/start")]
    public IActionResult RunPipeline(int pipelineId)
    {
        pipelineService.RunPipeline(pipelineId);
        return Ok();
    }
}