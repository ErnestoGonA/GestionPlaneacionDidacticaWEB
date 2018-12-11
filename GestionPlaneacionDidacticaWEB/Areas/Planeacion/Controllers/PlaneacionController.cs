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
    }
}