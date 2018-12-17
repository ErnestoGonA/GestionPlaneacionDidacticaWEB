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

        public IActionResult FicViCriteriosList(short asignatura, int planeacion, short tema, short competencia)
        {
            try
            {
                ViewBag.IdAsignatura = asignatura;
                ViewBag.IdPlaneacion = planeacion;
                ViewBag.IdTema = tema;
                ViewBag.IdCompetencia = competencia;

                FicListaCriterios = FicSrvCriterios.GetListCriterios(asignatura,planeacion, tema,competencia).Result;
                ViewBag.Title = "Catalogo de Criteiros";
                return View(FicListaCriterios);

            }
            catch(Exception e)
            {
                throw e;
            }

        }
    }
}