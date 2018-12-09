using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GestionPlaneacionDidacticaWEB.Areas.Planeacion.Controllers
{
    public class CriteriosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}