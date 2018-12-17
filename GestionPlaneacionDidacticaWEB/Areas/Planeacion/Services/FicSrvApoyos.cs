using System;
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
    public class FicSrvApoyos
    {
        HttpClient client;

        public FicSrvApoyos()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri("http://localhost:53483/");
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<eva_planeacion_apoyos>> FicGetListApoyos(short IdAsignatura, short IdPlaneacion)
        {

            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Asignatura/" + IdAsignatura + "/Planeacion/" + IdPlaneacion + "/Apoyos");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_planeacion_apoyos>>(FicRespuesta);
            }
            //return null;
            return new List<eva_planeacion_apoyos>();
        }

        public async Task<eva_planeacion_apoyos> FicGetApoyo(short IdAsignatura, short IdPlaneacion, short IdApoyo)
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Asignatura/" + IdAsignatura + "/Planeacion/" + IdPlaneacion + "/Apoyos/" + IdApoyo);
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<eva_planeacion_apoyos>(FicRespuesta);
            }
            //return null;
            return new eva_planeacion_apoyos();
        }

        public async Task<eva_planeacion_apoyos> FicApoyoCreate(eva_planeacion_apoyos Apoyo)
        {

            var json = JsonConvert.SerializeObject(Apoyo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPost = await client.PostAsync("api/Planeacion/Apoyos", content);
            if (respuestaPost.IsSuccessStatusCode)
            {
                return Apoyo;
            }
            return null;
        }

        public async Task<eva_planeacion_apoyos> FicApoyosUpdate(eva_planeacion_apoyos Apoyo)
        {
            Apoyo.FechaUltMod = DateTime.Now;
            Apoyo.UsuarioUltMod = "Reyes";

            var json = JsonConvert.SerializeObject(Apoyo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPut = await client.PutAsync("api/planeacion/Apoyos", content);
            if (respuestaPut.IsSuccessStatusCode)
            {
                return Apoyo;
            }
            return null;
        }

        public async Task<string> FicApoyosDelete(short IdAsignatura, short IdPlaneacion, short IdApoyo)
        {

            var respuestaDelete = await client.DeleteAsync("api/asignatura/" + IdAsignatura + "/planeacion/" + IdPlaneacion + "/Apoyos/" + IdApoyo);
            if (respuestaDelete.IsSuccessStatusCode)
            {
                return "OK";
            }
            return "ERROR";
        }


        public async Task<List<eva_planeacion_apoyos>> FicGetListPlaneacionApoyos(eva_planeacion planeacion)
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Asignatura/" + planeacion.IdAsignatura + "/Planeacion/" + planeacion.IdPlaneacion + "/Apoyos");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_planeacion_apoyos>>(FicRespuesta);
            }
            //return null;
            return new List<eva_planeacion_apoyos>();
        }

    }
}
