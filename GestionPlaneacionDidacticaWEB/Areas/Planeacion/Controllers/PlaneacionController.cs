using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionPlaneacionDidacticaWEB.Areas.Planeacion.Services;
using GestionPlaneacionDidacticaWEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionPlaneacionDidacticaWEB.Areas.Planeacion.Controllers
{
    [Area("Planeacion")]
    public class PlaneacionController : Controller
    {
        FicSrvPlaneacion FicSrvPlaneacion;
        List<eva_planeacion> FicListaPlaneacion;
        eva_planeacion planeacion;
        public PlaneacionController()
        {
            FicSrvPlaneacion = new FicSrvPlaneacion();
        }
        public IActionResult FicViPlaneacionList()
        {
            try
            {
                FicListaPlaneacion = FicSrvPlaneacion.FicGetListTemas().Result;
                ViewBag.Title = "Catalogo de planeaciones";
                return View(FicListaPlaneacion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViPlaneacionCreate()
        {
            var planeacion = new eva_planeacion();
            planeacion.IdPlaneacion = 1;
            planeacion.IdAsignatura = 1;
            planeacion.IdPeriodo = 1;

            return View(planeacion);
        }

        [HttpPost]
        public ActionResult FicViPlaneacionCreate(eva_planeacion planeacion)
        {
            planeacion.FechaReg = DateTime.Now;
            planeacion.FechaUltMod = DateTime.Now;
            planeacion.UsuarioReg = "PEDRO";
            planeacion.UsuarioMod = "PEDRO";
            planeacion.Activo = "S";
            planeacion.Borrado = "N";
            planeacion.IdPlaneacion = 3;
            planeacion.IdAsignatura = 1;
            planeacion.IdPeriodo = 1;
            FicSrvPlaneacion.FicPlaneacionCreate(planeacion).Wait();
            return RedirectToAction("FicViPlaneacionList");
        }
    }
}