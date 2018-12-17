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
                ViewBag.IdPlaneacion = IdPlaneacion;
                ViewBag.IdAsignatura = IdAsignatura;
                ViewBag.IdTema = IdTema;
                FicListaCompetencias = FicSrvCompetencias.FicGetListCompetencias(IdAsignatura,IdPlaneacion, IdTema).Result;
                ViewBag.Title = "Catalogo de Competencias";
                return View(FicListaCompetencias);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViCompetenciasCreate(short asignatura, short planeacion, short tema)
        {
            var Competencia = new eva_planeacion_temas_competencias();
            ViewBag.Competencias = new SelectList(new List<SelectListItem>(), "Value", "Text");
            Competencia.IdPlaneacion = planeacion;
            Competencia.IdAsignatura = asignatura;
            Competencia.IdTema = tema;
            return View(Competencia);
        }

        [HttpPost]
        public ActionResult FicViCompetenciasCreate(eva_planeacion_temas_competencias FicCompetencia)
        {
            FicCompetencia.IdCompetencia = Convert.ToInt16(Request.Form["Competencias"].ToString());
            Competencia = FicSrvCompetencias.GetCompetencia(FicCompetencia.IdAsignatura, FicCompetencia.IdPlaneacion, FicCompetencia.IdTema, FicCompetencia.IdCompetencia).Result;
            if (Competencia.IdCompetencia == -1)
            {
                FicCompetencia.IdCompetencia = Convert.ToInt16(Request.Form["Competencias"].ToString());
                FicCompetencia.FechaReg = DateTime.Now;
                FicCompetencia.FechaUltMod = DateTime.Now;
                FicCompetencia.UsuarioReg = "Ernesto";
                FicCompetencia.UsuarioUltMod = "Ernesto";
                FicCompetencia.Activo = "S";
                FicCompetencia.Borrado = "N";
                FicSrvCompetencias.CreateCompetencia(FicCompetencia).Wait();
                return RedirectToAction("FicViCompetenciasList", new { FicCompetencia.IdPlaneacion, FicCompetencia.IdAsignatura, FicCompetencia.IdTema });
            }
            return RedirectToAction("FicViCompetenciasList", new { FicCompetencia.IdPlaneacion, FicCompetencia.IdAsignatura, FicCompetencia.IdTema });
        }

        public IActionResult FicViCompetenciasDetail(eva_planeacion_temas_competencias item)
        {
            try
            {
                ViewBag.IdPlaneacion = item.IdPlaneacion;
                ViewBag.IdAsignatura = item.IdAsignatura;
                ViewBag.IdTema = item.IdTema;
                ViewBag.Title = "Detalle Fuente";
                return View(item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViCompetenciasEdit(short IdAsignatura,int IdPlaneacion, short IdTema, short IdCompetencia)
        {
            try
            {
                var Competencia = FicSrvCompetencias.GetEvaPlaneacionTemasCompetencia(IdAsignatura, IdPlaneacion, IdTema, IdCompetencia).Result;
                ViewBag.IdPlaneacion = IdPlaneacion;
                ViewBag.IdAsignatura = IdAsignatura;
                ViewBag.IdTema = IdTema;
                ViewBag.IdCompetencia = IdCompetencia;
                ViewBag.Competencias = new SelectList(new List<SelectListItem>(), "Value", "Text");
                return View(Competencia);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult FicViCompetenciasEdit(eva_planeacion_temas_competencias FicCompetencia)
        {
            try
            {                 
                FicSrvCompetencias.UpdateCompetencia(FicCompetencia).Wait();
                return RedirectToAction("FicViCompetenciasList", new { FicCompetencia.IdPlaneacion, FicCompetencia.IdAsignatura, FicCompetencia.IdTema });
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}