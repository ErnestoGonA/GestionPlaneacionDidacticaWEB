using GestionPlaneacionDidacticaWEB.Areas.Planeacion.Services;
using GestionPlaneacionDidacticaWEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPlaneacionDidacticaWEB.Areas.Planeacion.Controllers
{
    [Area("Planeacion")]
    public class FuentesController : Controller
    {
        FicSrvFuentes FicSrvFuentes;

        List<eva_planeacion_fuentes> FicListaFuentes;
        eva_planeacion_fuentes Fuente;

        public FuentesController()
        {
            FicSrvFuentes = new FicSrvFuentes();
        }


        public IActionResult FicViFuentesList(int IdPlaneacion, short IdAsignatura)
        {
            try
            {
                ViewBag.IdPlaneacion = IdPlaneacion;
                ViewBag.IdAsignatura = IdAsignatura;
                FicListaFuentes = FicSrvFuentes.FicGetListFuentes((short)IdPlaneacion,IdAsignatura).Result;
                ViewBag.Title = "Catalogo de Fuentes";
                return View(FicListaFuentes);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViFuentesCreate(short asignatura, short planeacion)
        {
            var Fuente = new eva_planeacion_fuentes();
            ViewBag.Fuentes = new SelectList(new List<SelectListItem>(), "Value", "Text");
            Fuente.IdPlaneacion = planeacion;
            Fuente.IdAsignatura = asignatura;
            return View(Fuente);
        }

        [HttpPost]
        public ActionResult FicViFuentesCreate(eva_planeacion_fuentes FicFuente)
        {
           FicFuente.IdFuente = Convert.ToInt16(Request.Form["Fuentes"].ToString());
           Fuente = FicSrvFuentes.FicGetFuente((short)FicFuente.IdPlaneacion, FicFuente.IdAsignatura, FicFuente.IdFuente).Result;
            if (Fuente.IdFuente == -1) {
                FicFuente.IdFuente = Convert.ToInt16(Request.Form["Fuentes"].ToString());
                FicFuente.FechaReg = DateTime.Now;
                FicFuente.FechaUltMod = DateTime.Now;
                FicFuente.UsuarioReg = "Bryan";
                FicFuente.UsuarioUltMod = "Bryan";
                FicFuente.Activo = "S";
                FicFuente.Borrado = "N";
                FicSrvFuentes.FicFuentesCreate(FicFuente).Wait();
                return RedirectToAction("FicViFuentesList", new { FicFuente.IdPlaneacion, FicFuente.IdAsignatura });
            }
            return RedirectToAction("FicViFuentesList", new { FicFuente.IdPlaneacion, FicFuente.IdAsignatura }); 
        }

        public IActionResult FicViFuentesDetail(eva_planeacion_fuentes item)
        {
            try
            {
                ViewBag.Title = "Detalle Fuente";
                return View(item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViFuentesEdit(short idPlaneacion, short idAsignatura,short idFuente)
        {
            try
            {
                Fuente = FicSrvFuentes.FicGetFuente(idPlaneacion, idAsignatura, idFuente).Result;
                ViewBag.Title = "Actualizar Fuente";
                return View(Fuente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult FicViFuentesEdit(eva_planeacion_fuentes FicFuente)
        {
            FicSrvFuentes.FicFuentesUpdate(FicFuente).Wait();
            return RedirectToAction("FicViFuentesList", new { FicFuente.IdPlaneacion, FicFuente.IdAsignatura });
        }



        public ActionResult FicViFuentesDelete(eva_planeacion_fuentes FicFuente)
        {

            if (FicFuente != null)
            {
                FicSrvFuentes.FicFuentesDelete(FicFuente.IdFuente, FicFuente.IdAsignatura, (short)FicFuente.IdPlaneacion).Wait();
                return RedirectToAction("FicviFuentesList", new { FicFuente.IdPlaneacion, FicFuente.IdAsignatura });
            }
            return null;
        }
       

    }
}
