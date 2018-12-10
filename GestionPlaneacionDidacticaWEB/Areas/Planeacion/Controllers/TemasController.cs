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
        public ActionResult FicViTemasCreate(eva_planeacion_temas FicTema)
        {
            FicTema.FechaReg = DateTime.Now;
            FicTema.FechaUltMod = DateTime.Now;
            FicTema.UsuarioReg = "ERNESTO";
            FicTema.UsuarioMod = "ERNESTO";
            FicTema.Activo = "S";
            FicTema.Borrado = "N";
            FicSrvTemas.FicTemasCreate(FicTema).Wait();
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
                throw e;
            }
        }

        public IActionResult FicViTemasEdit(int IdPlaneacion,short IdTema)
        {
            try
            {
                Tema = FicSrvTemas.FicGetTema(IdPlaneacion, IdTema).Result;
                ViewBag.Title = "Actualizar Tema";
                return View(Tema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult FicViTemasEdit(eva_planeacion_temas Tema)
        {
            FicSrvTemas.FicTemasUpdate(Tema).Wait();
            return RedirectToAction("FicViTemasList");
        }

        

        public ActionResult FicViTemasDelete(short IdTema)
        {
            
            if (IdTema != null)
            {
                FicSrvTemas.FicTemasDelete(IdTema).Wait();
                return RedirectToAction("FicviTemasList");   
            }
            return null;
        }

    }
}