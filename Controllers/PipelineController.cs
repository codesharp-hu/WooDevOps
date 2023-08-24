using System.Threading.Channels;
using Microsoft.AspNetCore.Mvc;

namespace BashScriptRunner.Controllers;

[ApiController]
[Route("api/pipelines")]
public class PipelineController : ControllerBase
{
    private readonly Channel<Pipeline> channel;

    public PipelineController(Channel<Pipeline> channel)
    {
        this.channel = channel;
    }

    [HttpPost]
    [Route("start")]
    public IActionResult Start([FromBody] PipelineDescriptor pipelineDescriptor)
    {
        try
        {
            // TODO: pipeline-t létrehozni
            var pipeline = new Pipeline
            {
                Name = pipelineDescriptor.Name
            };
            channel.Writer.TryWrite(pipeline);
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}