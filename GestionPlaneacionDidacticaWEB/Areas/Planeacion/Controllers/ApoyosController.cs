using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult FicViApoyosList(short IdPlaneacion, short IdAsignatura)
        {
            try
            {
                ViewBag.IdPlaneacion = IdPlaneacion;
                ViewBag.IdAsignatura = IdAsignatura;
                FicListaApoyos = FicSrvApoyos.FicGetListApoyos((short)IdPlaneacion, IdAsignatura).Result;
                ViewBag.Title = "Catalogo de Apyos";
                return View(FicListaApoyos);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViApoyosCreate(short asignatura, short planeacion)
        {
            var Apoyo = new eva_planeacion_apoyos();
            ViewBag.Apoyos = new SelectList(new List<SelectListItem>(), "Value", "Text");
            Apoyo.IdAsignatura = asignatura;
            Apoyo.IdPlaneacion = planeacion;
            ViewBag.IdPlaneacion = planeacion;
            ViewBag.IdAsignatura = asignatura;
            System.Diagnostics.Debug.WriteLine("\n\n\n\n\n\n\n\n\n\n\nAPOYOCREATE1" + Apoyo.IdApoyoDidactico + asignatura + planeacion);
            return View(Apoyo);
        }

        [HttpPost]
        public ActionResult FicViApoyosCreate(eva_planeacion_apoyos FicApoyo)
        {
            //FicApoyo.IdApoyoDidactico = Convert.ToInt16(Request.Form["Apoyos"].ToString());
            //Apoyo = FicSrvApoyos.FicGetApoyo(Convert.ToInt16(Request.Form["Apoyos"].ToString()), FicApoyo.IdAsignatura, FicApoyo.IdApoyoDidactico).Result;
            //if (Apoyo.IdApoyoDidactico == -1)
            //System.Diagnostics.Debug.WriteLine("\n\n\n\n\n\n\n\n\n\n\nAPOYO2" + Apoyo.IdApoyoDidactico + FicApoyo.IdAsignatura + FicApoyo.IdApoyoDidactico);
            FicApoyo.IdApoyoDidactico = Convert.ToInt16(Request.Form["Apoyos"].ToString());
            FicApoyo.FechaReg = DateTime.Now;
            FicApoyo.FechaUltMod = DateTime.Now;
            FicApoyo.UsuarioReg = "Reyes";
            FicApoyo.UsuarioMod = "Reyes";
            FicApoyo.Activo = "S";
            FicApoyo.Borrado = "N";
            FicSrvApoyos.FicApoyoCreate(FicApoyo).Wait();
            return RedirectToAction("FicViApoyosList", new { FicApoyo.IdPlaneacion, FicApoyo.IdAsignatura });
        }

        public IActionResult FicViApoyosDetail(eva_planeacion_apoyos item)
        {
            try
            {
                ViewBag.IdAsignatura = item.IdAsignatura;
                ViewBag.IdPlaneacion = item.IdPlaneacion;
                ViewBag.Title = "Detalle Apoyo";
                return View(item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IActionResult FicViApoyosEdit(eva_planeacion_apoyos Apoyo)
        {
            try
            {
                //ViewBag.IdAsignatura = IdAsignatura;
                //ViewBag.IdPlaneacion = IdPlaneacion;
                //eva_planeacion_apoyos apoyo = FicSrvApoyos.FicGetApoyo(IdAsignatura, IdPlaneacion, IdApoyo).Result;
                //Apoyo = FicSrvApoyos.FicGetApoyo(IdPlaneacion, IdAsignatura, IdApoyo).Result;
                ViewBag.Title = "Actualizar Apoyo";
                ViewBag.IdAsignatura = Apoyo.IdAsignatura;
                ViewBag.IdPlaneacion = Apoyo.IdPlaneacion;
                return View(Apoyo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public ActionResult FicViApoyosEditar(eva_planeacion_apoyos Apoyo)
        {
            FicSrvApoyos.FicApoyosUpdate(Apoyo).Wait();
            return RedirectToAction("FicViApoyosList", new { Apoyo.IdAsignatura, Apoyo.IdPlaneacion });
        }

        public ActionResult FicViApoyosDelete(eva_planeacion_apoyos apoyo)
        {

            if (apoyo != null)
            {
                FicSrvApoyos.FicApoyosDelete(apoyo.IdApoyoDidactico, apoyo.IdAsignatura, (short)apoyo.IdPlaneacion).Wait();
                return RedirectToAction("FicViApoyosList", new { apoyo.IdPlaneacion, apoyo.IdAsignatura });
            }
            return null;
        }

    }
}