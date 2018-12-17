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
    public class CriteriosController : Controller
    { 
        FicSrvCriterios FicSrvCriterios;

        List<eva_planeacion_criterios_evalua> FicListaCriterios;
        eva_planeacion_criterios_evalua Criterio;

        public CriteriosController()
        {
            FicSrvCriterios = new FicSrvCriterios();
        }

        public IActionResult FicViCriteriosList(short IdAsignatura, int IdPlaneacion, short IdTema, short IdCompetencia)
        {
            try
            {
                ViewBag.IdAsignatura = IdAsignatura;
                ViewBag.IdPlaneacion = IdPlaneacion;
                ViewBag.IdTema = IdTema;
                ViewBag.IdCompetencia = IdCompetencia;

                FicListaCriterios = FicSrvCriterios.GetListCriterios(IdAsignatura, IdPlaneacion,IdTema ,IdCompetencia).Result;
                ViewBag.Title = "Catalogo de Criteiros";
                return View(FicListaCriterios);

            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public IActionResult FicViCriteriosCreate(short asignatura, short planeacion, short tema, short competencia)
        {
            Criterio = new eva_planeacion_criterios_evalua();
            Criterio.IdPlaneacion = planeacion;
            Criterio.IdAsignatura = asignatura;
            Criterio.IdTema = tema;
            Criterio.IdCompetencia = competencia;
            return View(Criterio);
        }

        [HttpPost]
        public ActionResult FicViCriteriosCreate(eva_planeacion_criterios_evalua FicCompetencia)
        {             
            
            FicCompetencia.FechaReg = DateTime.Now;
            FicCompetencia.FechaUltMod = DateTime.Now;
            FicCompetencia.UsuarioReg = "Ernesto";
            FicCompetencia.UsuarioUltMod = "Ernesto";
            FicCompetencia.Activo = "S";
            FicCompetencia.Borrado = "N";
            FicSrvCriterios.CreateCriterio(FicCompetencia).Wait();
            return RedirectToAction("FicViCriteriosList", new { FicCompetencia.IdPlaneacion, FicCompetencia.IdAsignatura, FicCompetencia.IdTema, FicCompetencia.IdCompetencia });
        }

        public IActionResult FicViCriteriosDetail(eva_planeacion_criterios_evalua item)
        {
            try
            {
                ViewBag.IdAsignatura = item.IdAsignatura;
                ViewBag.IdPlaneacion = item.IdPlaneacion;
                ViewBag.IdTema = item.IdTema;
                ViewBag.IdCompetencia = item.IdCompetencia;
                ViewBag.Title = "Detalle Tema";
                return View(item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViCriteriosEdit(short IdAsignatura, int IdPlaneacion, short IdTema,short IdCompetencia, short IdCriterio)
        {
            try
            {
                ViewBag.IdAsignatura = IdAsignatura;
                ViewBag.IdPlaneacion = IdPlaneacion;
                ViewBag.IdTema = IdTema;
                ViewBag.IdCompetencia = IdCompetencia;
                eva_planeacion_criterios_evalua tema = FicSrvCriterios.GetCriterio(IdAsignatura, IdPlaneacion, IdTema, IdCompetencia, IdCriterio).Result;
                ViewBag.Title = "Actualizar criterio";
                return View(tema);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult FicViCriteriosEdit(eva_planeacion_criterios_evalua Tema)
        {
            FicSrvCriterios.PUTCriterio(Tema).Wait();
            return RedirectToAction("FicViCriteriosList", new { Tema.IdAsignatura, Tema.IdPlaneacion,Tema.IdTema, Tema.IdCompetencia });
        }



        public ActionResult FicViCriteriosDelete(eva_planeacion_criterios_evalua tema)
        {

            if (tema != null)
            {
                FicSrvCriterios.DeleteCriterio(tema).Wait();
                return RedirectToAction("FicViCriteriosList", new { tema.IdAsignatura, tema.IdPlaneacion,tema.IdTema, tema.IdCompetencia });
            }
            return null;
        }

    }
}