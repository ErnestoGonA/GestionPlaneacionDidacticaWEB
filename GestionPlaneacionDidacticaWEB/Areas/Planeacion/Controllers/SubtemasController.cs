using GestionPlaneacionDidacticaWEB.Areas.Planeacion.Services;
using GestionPlaneacionDidacticaWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPlaneacionDidacticaWEB.Areas.Planeacion.Controllers
{
    [Area("Planeacion")]
    public class SubtemasController : Controller
    {
        FicSrvSubtemas FicSrvSubtemas;

        List<eva_planeacion_subtemas> FicListaSubtemas;
        eva_planeacion_subtemas Subtemas;

        public SubtemasController()
        {
            FicSrvSubtemas = new FicSrvSubtemas();
        }


        public IActionResult FicViSubtemasList(short IdPlaneacion, short IdTema, short IdAsignatura)
        {
            try
            {
                ViewBag.IdPlaneacion = IdPlaneacion;
                ViewBag.IdAsignatura = IdAsignatura;
                ViewBag.IdTema = IdTema;
                FicListaSubtemas = FicSrvSubtemas.FicGetListSubtemas(IdPlaneacion, IdTema,IdAsignatura).Result;
                ViewBag.Title = "Catalogo de Subtemas";
                return View(FicListaSubtemas);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
     
        public IActionResult FicViSubtemasCreate(short IdPlaneacion, short IdTema, short IdAsignatura)
        {
            var subtema = new eva_planeacion_subtemas();
            subtema.IdPlaneacion = IdPlaneacion;
            subtema.IdAsignatura = IdAsignatura;
            subtema.IdTema = IdTema;
            ViewBag.IdPlaneacion = subtema.IdPlaneacion;
            ViewBag.IdAsignatura = subtema.IdAsignatura;
            ViewBag.IdTema = subtema.IdTema;
            return View(subtema);
        }

        [HttpPost]
        public ActionResult FicViSubtemasCreate(eva_planeacion_subtemas Subtema)
        {
            Subtema.FechaReg = DateTime.Now;
            Subtema.FechaUltMod = DateTime.Now;
            Subtema.UsuarioReg = "Bryan";
            Subtema.UsuarioUltMod = "Bryan";
            Subtema.Activo = "S";
            Subtema.Borrado = "N";
            FicSrvSubtemas.FicSubtemaCreate(Subtema).Wait();
            return RedirectToAction("FicViSubtemasList", new { Subtema.IdPlaneacion, Subtema.IdTema, Subtema.IdAsignatura });
        }

        public IActionResult FicViSubtemasDetail(eva_planeacion_subtemas item)
        {
            try
            {
                ViewBag.IdPlaneacion = item.IdPlaneacion;
                ViewBag.IdAsignatura = item.IdAsignatura;
                ViewBag.IdTema = item.IdTema;
                ViewBag.Title = "Detalle Subtema";
                return View(item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViSubtemasEdit(short IdPlaneacion, short IdTema,short IdSubtema, short IdAsignatura)
        {
            try
            {
                ViewBag.IdPlaneacion = IdPlaneacion;
                ViewBag.IdAsignatura = IdAsignatura;
                ViewBag.IdTema = IdTema;
                Subtemas = FicSrvSubtemas.FicGetSubtema(IdPlaneacion, IdTema,IdAsignatura,IdSubtema).Result;
                ViewBag.Title = "Actualizar Subtema";
                return View(Subtemas);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult FicViSubtemasEdit(eva_planeacion_subtemas Subtema)
        {
            FicSrvSubtemas.FicSubtemasUpdate(Subtema).Wait();
            return RedirectToAction("FicViSubtemasList", new { Subtema.IdPlaneacion,Subtema.IdTema, Subtema.IdAsignatura });
        }



        public ActionResult FicViSubtemasDelete(eva_planeacion_subtemas Subtema)
        {

            if (Subtema != null)
            {
                FicSrvSubtemas.FicSubtemasDelete(Subtema.IdSubtema).Wait();
                return RedirectToAction("FicviSubtemasList", new { Subtema.IdPlaneacion, Subtema.IdTema, Subtema.IdAsignatura });
            }
            return null;
        }




    }//class
}
