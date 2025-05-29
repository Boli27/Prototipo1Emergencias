using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/restringido")]
public class RestrictController : ControllerBase
{
    [HttpGet]
    public IActionResult SoloUsuariosLogueados()
    {
        return Ok("Estás autenticado");
    }
}
