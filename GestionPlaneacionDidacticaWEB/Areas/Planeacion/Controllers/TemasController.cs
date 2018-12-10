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

        public IActionResult FicViTemasCreate()
        {
            var Tema = new eva_planeacion_temas();
            Tema.IdPlaneacion = 1;
            Tema.IdAsignatura = 1;
            return View(Tema);
        }

        [HttpPost]
        public ActionResult FicViTemasCreate(eva_planeacion_temas Tema)
        {
            FicSrvTemas.FicTemasCreate(Tema).Wait();
            return RedirectToAction("FicViTemasList");          
        }

        public IActionResult FicViTemasDetail(eva_planeacion_temas item)
        {
            try
            {
                ViewBag.Title = "Detalle Tema";
                return View(item);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IActionResult FicViTemasUpdate()
        {
            return View();

        }

        public IActionResult FicViTemasDelete()
        {
            return View();

        }

    }
}