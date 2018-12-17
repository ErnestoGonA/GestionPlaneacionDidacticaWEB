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

    }
}