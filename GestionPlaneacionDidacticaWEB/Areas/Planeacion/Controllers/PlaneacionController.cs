using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionPlaneacionDidacticaWEB.Areas.Planeacion.Services;
using GestionPlaneacionDidacticaWEB.Models;
using Microsoft.AspNetCore.Mvc;
using GestionPlaneacionDidacticaWEB.Areas.Planeacion.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionPlaneacionDidacticaWEB.Areas.Planeacion.Controllers
{
    [Area("Planeacion")]
    public class PlaneacionController : Controller
    {
        FicSrvPlaneacion FicSrvPlaneacion;
        FicSrvTemas FicSrvTemas;
        List<eva_planeacion> FicListaPlaneacion;
        eva_planeacion planeacion;
        TemasController controller;
        FicSrvApoyos FicSrvApoyos;
        ApoyosController apoyoscontroller;

        public PlaneacionController()
        {
            FicSrvPlaneacion = new FicSrvPlaneacion();
            controller = new TemasController();
            apoyoscontroller = new ApoyosController();
        }
        public IActionResult FicViPlaneacionList()
        {
            try
            {
                ViewBag.Asi = new SelectList(new List<SelectListItem>(), "Value", "Text");
                ViewBag.Per = new SelectList(new List<SelectListItem>(), "Value", "Text"); 
                ViewBag.Us = new SelectList(new List<SelectListItem>(), "Value", "Text");
                FicListaPlaneacion = FicSrvPlaneacion.FicGetListPlaneacion().Result;
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
            planeacion.IdAsignatura = 1;//Convert.ToInt16(Request.Form["Asi"].ToString());
            planeacion.IdPeriodo = 1;//Convert.ToInt16(Request.Form["Asi"].ToString());

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
            planeacion.IdPlaneacion = 4;
            planeacion.IdAsignatura = 1;
            planeacion.IdPeriodo = 1;
            FicSrvPlaneacion.FicPlaneacionCreate(planeacion).Wait();
            return RedirectToAction("FicViPlaneacionList");
        }
        public IActionResult FicViPlaneacionDetail(eva_planeacion item)
        {
            try
            {
                ViewBag.Title = "Detalle Planeación";
                return View(item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IActionResult FicViPlaneacionEdit(int IdPlaneacion)
        {
            try
            {
                planeacion = FicSrvPlaneacion.FicGetPlaneacion(IdPlaneacion).Result;
                ViewBag.Title = "Actualizar planeación";
                return View(planeacion);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //[HttpPut]
        public ActionResult FicViPlaneacionEditPut(eva_planeacion planeacion)
        {
            FicSrvPlaneacion.FicPlaneacionUpdate(planeacion).Wait();
            return RedirectToAction("FicViPlaneacionList");
        }

        public ActionResult FicViPlaneacionDelete(int IdPlaneacion)
        {
            FicSrvPlaneacion.FicPlaneacionDelete(IdPlaneacion).Wait();
            return RedirectToAction("FicViPlaneacionList");
        }

        public IActionResult FicViTemasList(eva_planeacion planeacion)
        {
            try
            {
                return RedirectToAction("FicViTemasList", "Temas", new { planeacion.IdAsignatura,planeacion.IdPlaneacion});
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViFuentesList(eva_planeacion planeacion)
        {
            try
            {
                return RedirectToAction("FicViFuentesList", "Fuentes", new { planeacion.IdPlaneacion, planeacion.IdAsignatura });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}