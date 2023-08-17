using System.Threading.Channels;
using Microsoft.AspNetCore.Mvc;

namespace BashScriptRunner.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobController : ControllerBase
{
    private readonly PipelineState pipelineState;
    private Channel<JobTask> channel { get; }

    public JobController(PipelineState pipelineState, Channel<JobTask> channel)
    {
        this.channel = channel;
        this.pipelineState = pipelineState;
    }

    [HttpPost]
    [Route("start")]
    public IActionResult StartJob()
    {
        channel.Writer.TryWrite(new JobTask());
        return Ok();
    }

    [HttpGet]
    [Route("state")]
    public IActionResult GetPipelineState()
    {
        return Ok(pipelineState);
    }
}
