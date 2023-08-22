using Microsoft.AspNetCore.Mvc;

namespace BashScriptRunner.Controllers;

[ApiController]
[Route("api/pipelines")]
public class PipelineController : ControllerBase
{
    [HttpPost]
    [Route("start")]
    public IActionResult Start([FromBody] PipelineDescriptor pipelineDescriptor)
    {
        try
        {
            pipelineDescriptor.Run();
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }
}