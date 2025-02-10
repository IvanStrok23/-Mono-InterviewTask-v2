using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MonoTask.UI.WebApi.Controllers
{
    [Authorize(Policy = "IsClient")]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Test()
        {
            await Task.Delay(2000);
            return Ok();
        }
    }
}
