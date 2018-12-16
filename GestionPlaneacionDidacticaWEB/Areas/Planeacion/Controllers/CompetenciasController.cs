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
    public class CompetenciasController : Controller
    {
        FicSrvCompetencias FicSrvCompetencias;

        List<eva_planeacion_temas_competencias> FicListaCompetencias;
        eva_planeacion_temas_competencias Competencia;

        public CompetenciasController()
        {
            FicSrvCompetencias = new FicSrvCompetencias();
        }



        public IActionResult FicViCompetenciasList(short IdAsignatura, int IdPlaneacion, short IdTema)
        {
            try
            {
                FicListaCompetencias = FicSrvCompetencias.FicGetListCompetencias(IdAsignatura,IdPlaneacion, IdTema).Result;
                ViewBag.Title = "Catalogo de Competencias";
                return View(FicListaCompetencias);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}