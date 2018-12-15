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
    public class ApoyosController : Controller
    {
        FicSrvApoyos FicSrvApoyos;

        List<eva_planeacion_apoyos> FicListaApoyos;
        eva_planeacion_apoyos Apoyo;

        public ApoyosController()
        {
            FicSrvApoyos = new FicSrvApoyos();
        }

        public IActionResult FicViApoyosList(eva_planeacion planeacion)
        {
            try
            {
                FicListaApoyos = FicSrvApoyos.FicGetListApoyos(planeacion).Result;
                ViewBag.Title = "Catalogo de apoyos";
                return View(FicListaApoyos);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViApoyosCreate()
        {
            var Apoyo = new eva_planeacion_apoyos();
            Apoyo.IdPlaneacion = 1;
            Apoyo.IdAsignatura = 1;
            return View(Apoyo);
        }

        [HttpPost]
        public ActionResult FicViApoyosCreate(eva_planeacion_apoyos FicApoyo)
        {
            FicApoyo.FechaReg = DateTime.Now;
            FicApoyo.FechaUltMod = DateTime.Now;
            FicApoyo.UsuarioReg = "Reyes";
            FicApoyo.UsuarioUltMod = "Reyes";
            FicApoyo.Activo = "S";
            FicApoyo.Borrado = "N";
            FicSrvApoyos.FicApoyoCreate(FicApoyo).Wait();
            return RedirectToAction("FicViApoyosList");
        }

        public IActionResult FicViApoyosDetail(eva_planeacion_apoyos item)
        {
            try
            {
                ViewBag.Title = "Detalle Apoyo";
                return View(item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}