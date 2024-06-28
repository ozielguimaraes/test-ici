using Microsoft.AspNetCore.Mvc;

namespace TesteICI.Presentation.Areas.Administracao.Controllers;

[Area("administracao")]
public class HomeController : BaseController
{
    public IActionResult Index()
    {
        return View();
    }
}
