
using System.Collections.Generic;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BashScriptRunner.Controllers
{
    [ApiController]
    [Route("api")]
    public class BashScriptController : ControllerBase
    {
        private Channel<ScriptTask> Channel { get; }
        private Channel<ScriptState> ScriptStateChannel { get; set; }

        public BashScriptController(Channel<ScriptTask> channel, Channel<ScriptState> scriptStateChannel)
        {
            Channel = channel;
            ScriptStateChannel = scriptStateChannel;
        }

        [HttpPost("start")]
        public IActionResult StartBashScriptBackgroundService()
        {
            Channel.Writer.TryWrite(new ScriptTask());
            return Ok();
        }

        [HttpGet("state")]
        public IActionResult GetScriptState()
        {
            var success = ScriptStateChannel.Reader.TryRead(out var scriptState);
            if (success) {
                return Ok(scriptState);
            } else {
                return NotFound();
            }
        }
    }
}
