
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Channels;
using BashScriptRunner.HostedServices;
using Microsoft.AspNetCore.Mvc;

namespace BashScriptRunner.Controllers;

[ApiController]
[Route("api")]
public class BashScriptController : ControllerBase
{
    private readonly ScriptState scriptState;
    private Channel<ScriptTask> scriptTaskchannel { get; }

    public BashScriptController(ScriptState scriptState, Channel<ScriptTask> scriptTaskchannel)
    {
        this.scriptTaskchannel = scriptTaskchannel;
        this.scriptState = scriptState;
    }

    [HttpPost]
    [Route("start")]
    public IActionResult StartBashScriptBackgroundService()
    {
        scriptTaskchannel.Writer.TryWrite(new ScriptTask());
        return Ok();
    }

    [HttpGet]
    [Route("state")]
    public IActionResult GetScriptState()
    {
        return Ok(scriptState);
    }
}
