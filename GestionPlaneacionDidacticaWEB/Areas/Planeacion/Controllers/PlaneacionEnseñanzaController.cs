using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionPlaneacionDidacticaWEB.Areas.Planeacion.Services;
using GestionPlaneacionDidacticaWEB.AlterMod;
using Microsoft.AspNetCore.Mvc;
using GestionPlaneacionDidacticaWEB.Areas.Planeacion.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlaneacionDidacticaWEB.Areas.Planeacion.Controllers
{
    [Area("Planeacion")]
    public class PlaneacionEnseñanzaController : Controller
    {
        FicSrvPlaneacionEnsenseñanza FicService;
        List<extended_eva_planeacion_enseñanza> FicListaEPE;
        extended_eva_planeacion_enseñanza edi;

        public PlaneacionEnseñanzaController()
        {
            FicService = new FicSrvPlaneacionEnsenseñanza();
        }

        //Lista -------------------------------------------
        public IActionResult FicViPlaneacionEnseñanzaList(short IdAsignatura, int IdPlaneacion, short IdTema, int IdCompetencia)
        {
            try
            {
                ViewBag.AE = new SelectList(new List<SelectListItem>(), "Value", "Text");
                ViewBag.Per = new SelectList(new List<SelectListItem>(), "Value", "Text");
                FicListaEPE = FicService.FicGetListPlaneacionEnseñanza(IdAsignatura, IdPlaneacion, IdTema, IdCompetencia).Result;
                ViewBag.Title = "Catalogo de alumnos";
                return View(FicListaEPE);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //Lista -------------------------------------------
        public IActionResult FicViPlaneacionEnseñanzaList2()
        {
            try
            {
                FicListaEPE = FicService.FicGetListPlaneacionEnseñanza2().Result;
                ViewBag.Title = "Catalogo de alumnos";
                return View(FicListaEPE);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Detail -------------------------------------------
        public IActionResult FicViPlaneacionEnseñanzaDetail(short IdAsignatura, int IdPlaneacion, short IdTema, int IdCompetencia, int IdActividadEnseñanza)
        {
            Console.WriteLine("IdAsignatura: " + IdAsignatura);
            Console.WriteLine("IdPlaneacion: " + IdPlaneacion);
            Console.WriteLine("IdTema: " + IdTema);
            Console.WriteLine("IdCompetencia: " + IdCompetencia);
            Console.WriteLine("IdActividadEnseñanza: " + IdActividadEnseñanza);
            try
            {
                extended_eva_planeacion_enseñanza FicLista = FicService.FicGetDetailPlaneacionEnseñanza(IdAsignatura, IdPlaneacion, IdTema, IdCompetencia, IdActividadEnseñanza).Result;
                ViewBag.Title = "Detalle de alumnos";

                return View(FicLista);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //Delete----------------------------------------- 
        public ActionResult FicViPlaneacionEnseñanzaDelete(short IdAsignatura, int IdPlaneacion, short IdTema, int IdCompetencia, int IdActividadEnseñanza)
        {
            if (IdAsignatura != 0)
            {
                FicService.FicDeletePlaneacionEnseñanza(IdAsignatura,IdPlaneacion,IdTema,IdCompetencia,IdActividadEnseñanza).Wait();
                return RedirectToAction("FicViAlumnoCarreraList");
            }
            return null;
        }

        //Edit
        public IActionResult FicViPlaneacionEnseñanzaUpdate(short IdAsignatura, int IdPlaneacion, short IdTema, int IdCompetencia, int IdActividadEnseñanza, int IdActividadEnseñanzanew)
        {
            try
            {
                FicService = new FicSrvPlaneacionEnsenseñanza();
                edi = FicService.FicGetDetailPlaneacionEnseñanza(IdAsignatura, IdPlaneacion, IdTema, IdCompetencia, IdActividadEnseñanza).Result;
                ViewBag.Title = "Editar Edificio";
                return View(edi);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult FicViPlaneacionEnseñanzaUpdate(short IdAsignatura, int IdPlaneacion, short IdTema, int IdCompetencia, int IdActividadEnseñanza, extended_eva_planeacion_enseñanza epe)
        {
            FicService.FicUpdatePlaneacionEnseñanza(IdAsignatura, IdPlaneacion, IdTema, IdCompetencia, IdActividadEnseñanza,epe).Wait();
            return RedirectToAction("FicViPlaneacionEnseñanzaList");
        }

        //Create----------------------------------------- 
        
        public ActionResult FicViPlaneacionEnseñanzaCreate(string DesCompetencia, string DesTema, DateTime FechaProgramada, DateTime FechaRealizada, DateTime FechaReg, DateTime FechaUltMod, int IdActividadEnseñanza,string ReferenciaNorma,string UsuarioMod, string UsuarioReg, extended_eva_planeacion_enseñanza ed)
        {
            FicService = new FicSrvPlaneacionEnsenseñanza();
            FicService.FicAlumnoCarreraCreate(DesCompetencia, DesTema, FechaProgramada, FechaRealizada, FechaReg, FechaUltMod, IdActividadEnseñanza, ReferenciaNorma,UsuarioMod,UsuarioReg,ed).Wait();
            return RedirectToAction("FicViPlaneacionEnseñanzaList2");
        }
    }
}
