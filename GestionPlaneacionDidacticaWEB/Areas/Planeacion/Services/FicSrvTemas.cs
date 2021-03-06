﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using GestionPlaneacionDidacticaWEB.Models;
using Newtonsoft.Json;

namespace GestionPlaneacionDidacticaWEB.Areas.Planeacion.Services
{
    public class FicSrvTemas
    {
        HttpClient client;

        public FicSrvTemas()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri("http://localhost:53483/");
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<eva_planeacion_temas>> FicGetListTemas(short IdAsignatura,int IdPlaneacion)
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Asignatura/"+IdAsignatura+"/Planeacion/"+IdPlaneacion+"/Temas");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_planeacion_temas>>(FicRespuesta);
            }
            //return null;
            return new List<eva_planeacion_temas>();
        }

        public async Task<eva_planeacion_temas> FicGetTema(short IdAsignatura,int IdPlaneacion,short IdTema)
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Asignatura/"+IdAsignatura+"/Planeacion/"+IdPlaneacion+"/Temas/"+IdTema);
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("\n\n\n\n\n\n\n\n\n\n\n" + FicRespuesta);
                return JsonConvert.DeserializeObject<eva_planeacion_temas>(FicRespuesta);
            }
            //return null;
            return new eva_planeacion_temas();
        }

        public async Task<eva_planeacion_temas> FicTemasCreate(eva_planeacion_temas Tema)
        {
           
            var json = JsonConvert.SerializeObject(Tema);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPost = await client.PostAsync("api/planeacion/Temas", content);
            if (respuestaPost.IsSuccessStatusCode)
            {
                return Tema;
            }
            return null;
        }

        public async Task<eva_planeacion_temas> FicTemasUpdate(eva_planeacion_temas Tema)
        {
            Tema.FechaUltMod = DateTime.Now;
            Tema.UsuarioMod = "ERNESTO";

            var json = JsonConvert.SerializeObject(Tema);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPut = await client.PutAsync("api/planeacion/Temas", content);
            if (respuestaPut.IsSuccessStatusCode)
            {
                return Tema;
            }
            return null;
        }

        public async Task<string> FicTemasDelete(short IdAsignatura,int IdPlaneacion,short IdTema)
        {

            var respuestaDelete = await client.DeleteAsync("api/asignatura/"+IdAsignatura+"/planeacion/"+IdPlaneacion+"/Temas/"+IdTema);
            if (respuestaDelete.IsSuccessStatusCode)
            {
                return "OK";
            }
            return "ERROR";
        }

        public async Task<List<eva_planeacion_temas>> FicGetListPlaneacionTemas(eva_planeacion planeacion)
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Asignatura/"+planeacion.IdAsignatura+"/Planeacion/"+planeacion.IdPlaneacion+"/Temas");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_planeacion_temas>>(FicRespuesta);
            }
            //return null;
            return new List<eva_planeacion_temas>();
        }
    }
}
