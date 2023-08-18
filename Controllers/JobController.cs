using System.Threading.Channels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BashScriptRunner.Controllers;

[ApiController]
[Route("api/jobs")]
public class JobController : ControllerBase
{
    private Channel<JobDescriptor> channel { get; }

    public JobController(Channel<JobDescriptor> channel)
    {
        this.channel = channel;
    }

    [HttpPost]
    [Route("start")]
    public async Task<IActionResult> StartJob([FromBody] JobDescriptor jobDescriptor)
    {
        try
        {
            await channel.Writer.WriteAsync(jobDescriptor);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
