using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using GestionPlaneacionDidacticaWEB.Areas.Planeacion.Services;
using GestionPlaneacionDidacticaWEB.Models;

namespace GestionPlaneacionDidacticaWEB.Areas.Planeacion.Controllers
{
    [Area("Planeacion")]
    public class TemasController : Controller
    {
        FicSrvTemas FicSrvTemas;

        List<eva_planeacion_temas> FicListaTemas;
        eva_planeacion_temas Tema;

        public TemasController()
        {
            FicSrvTemas = new FicSrvTemas();
        }

        public IActionResult FicViTemasList()
        {
            try
            {
                FicListaTemas = FicSrvTemas.FicGetListTemas().Result;
                ViewBag.Title = "Catalogo de temas";
                return View(FicListaTemas);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}