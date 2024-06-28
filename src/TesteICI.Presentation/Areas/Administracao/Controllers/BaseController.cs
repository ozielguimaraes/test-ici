using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TesteICI.Presentation.Areas.Administracao.Controllers;

[Authorize]
public abstract class BaseController : Controller
{
}
