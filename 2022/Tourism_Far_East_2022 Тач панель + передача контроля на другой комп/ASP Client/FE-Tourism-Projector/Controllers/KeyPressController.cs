using Microsoft.AspNetCore.Mvc;
using WindowsInput;
using WindowsInput.Native;

namespace FE_Tourism_Projector.Controllers;

[ApiController]
[Route("[controller]")]
public class KeyPressController : ControllerBase
{
    private readonly ILogger<KeyPressController> _logger;
    private readonly InputSimulator _inputSimulator;

    public KeyPressController(ILogger<KeyPressController> logger)
    {
        _logger = logger;
        _inputSimulator = new InputSimulator();
    }

    [HttpGet(Name = "ButtonPressed")]
#pragma warning disable CS1998
    public async Task<IActionResult> Get([FromQuery] string keycode)
#pragma warning restore CS1998
    {
        _logger.Log(LogLevel.Information, keycode, keycode);
        int intValue = Convert.ToInt32(keycode , 16);
        VirtualKeyCode keyCode = (VirtualKeyCode)intValue;
        // await Task.Delay(TimeSpan.FromSeconds(3));
        _inputSimulator.Keyboard.KeyDown(keyCode).Sleep(1).KeyUp(keyCode);
        return Ok(keyCode.ToString());
    }
}