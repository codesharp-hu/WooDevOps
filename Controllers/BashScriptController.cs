
using System.Threading.Channels;
using Microsoft.AspNetCore.Mvc;

namespace BashScriptRunner.Controllers
{
    [ApiController]
    [Route("api")]
    public class BashScriptController : ControllerBase
    {
        private Channel<ScriptTask> Channel { get; }

        public BashScriptController(Channel<ScriptTask> channel)
        {
            Channel = channel;
        }

        [HttpPost("start")]
        public IActionResult StartBashScriptBackgroundService()
        {
            Channel.Writer.TryWrite(new ScriptTask());
            return Ok();
        }
    }
}
