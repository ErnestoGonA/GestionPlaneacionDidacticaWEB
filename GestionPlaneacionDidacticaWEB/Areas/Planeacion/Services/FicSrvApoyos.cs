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

        public async Task<List<eva_planeacion_apoyos>> FicGetListApoyos(short IdPlaneacion, short IdAsignatura)
        {

            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Planeacion/" + IdPlaneacion + "/Apoyos/" + IdAsignatura);
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_planeacion_apoyos>>(FicRespuesta);
            }
            //return null;
            return new List<eva_planeacion_apoyos>();
        }

        public async Task<eva_planeacion_apoyos> FicGetApoyo(short IdPlaneacion, short IdAsignatura, short IdApoyo)
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
            var respuestaPost = await client.PostAsync("api/NewApoyo/", content);
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
            var respuestaPut = await client.PutAsync("api/UpdateApoyo/", content);
            if (respuestaPut.IsSuccessStatusCode)
            {
                return Apoyo;
            }
            return null;
        }

        public async Task<string> FicApoyosDelete(short IdApoyo, short IdAsignatura, short IdPlaneacion)
        {

            var respuestaDelete = await client.DeleteAsync("api/DeleteFuente/" + IdApoyo + "/" + IdAsignatura + "/" + IdPlaneacion);
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
