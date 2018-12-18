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

        public IActionResult FicViTemasList(short IdAsignatura, int IdPlaneacion)
        {
            try
            {


                ViewBag.IdAsignatura = IdAsignatura;
                ViewBag.IdPlaneacion = IdPlaneacion;
                FicListaTemas = FicSrvTemas.FicGetListTemas(IdAsignatura,IdPlaneacion).Result;
                ViewBag.Title = "Catalogo de temas";
                return View(FicListaTemas);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViTemasCreate(short IdAsignatura, int IdPlaneacion)
        {
            var Tema = new eva_planeacion_temas();
            Tema.IdAsignatura = IdAsignatura;
            Tema.IdPlaneacion = IdPlaneacion;
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
            return RedirectToAction("FicViTemasList", new { FicTema.IdAsignatura,FicTema.IdPlaneacion});
        }

        public IActionResult FicViTemasDetail(eva_planeacion_temas item)
        {
            try
            {
                ViewBag.IdAsignatura = item.IdAsignatura;
                ViewBag.IdPlaneacion = item.IdPlaneacion;
                ViewBag.Title = "Detalle Tema";
                return View(item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViTemasEdit(short IdAsignatura, int IdPlaneacion, short IdTema)
        {
            try
            {
                ViewBag.IdAsignatura = IdAsignatura;
                ViewBag.IdPlaneacion = IdPlaneacion;
                eva_planeacion_temas tema = FicSrvTemas.FicGetTema(IdAsignatura, IdPlaneacion, IdTema).Result;
                ViewBag.Title = "Actualizar Tema";
                return View(tema);
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
            return RedirectToAction("FicViTemasList", new { Tema.IdAsignatura, Tema.IdPlaneacion });
        }

        

        public ActionResult FicViTemasDelete(eva_planeacion_temas tema)
        {
            
            if (tema != null)
            {
                FicSrvTemas.FicTemasDelete(tema.IdAsignatura, tema.IdPlaneacion, tema.IdTema).Wait();
                return RedirectToAction("FicViTemasList", new { tema.IdAsignatura, tema.IdPlaneacion });
            }
            return null;
        }

        public IActionResult FicViSubtemasList(int IdPlaneacion, short IdTema, short IdAsignatura, string DesTema)
        {
            try
            {
                return RedirectToAction("FicViSubtemasList", "Subtemas", new { IdPlaneacion,IdTema,IdAsignatura ,DesTema});
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
        public IActionResult FicViCompetenciasList(int IdPlaneacion, short IdTema, short IdAsignatura, string DesTema)
        {
            try
            {
                return RedirectToAction("FicViCompetenciasList", "Competencias", new {IdAsignatura,IdPlaneacion, IdTema, DesTema });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}