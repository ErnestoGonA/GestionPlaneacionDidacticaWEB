using GestionPlaneacionDidacticaWEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlaneacionDidacticaWEB.Areas.Planeacion.Services
{
    public class FicSrvPlaneacion
    {

        HttpClient client;
        public FicSrvPlaneacion()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri("http://localhost:53483/");
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<eva_planeacion>> FicGetListPlaneacion()
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Planeaciones");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_planeacion>>(FicRespuesta);
            }
            //return null;
            return new List<eva_planeacion>();
        }

        public async Task<eva_planeacion> FicPlaneacionCreate(eva_planeacion planeacion)
        {

            var json = JsonConvert.SerializeObject(planeacion);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPost = await client.PostAsync("api/Planeaciones/NewPlaneacion/", content);
            if (respuestaPost.IsSuccessStatusCode)
            {
                return planeacion;
            }
            return null;
        }
        public async Task<eva_planeacion> FicGetPlaneacion(int IdPlaneacion)
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Planeaciones/" + IdPlaneacion);
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<eva_planeacion>(FicRespuesta);
            }
            //return null;
            return new eva_planeacion();
        }
        public async Task<eva_planeacion> FicPlaneacionUpdate(eva_planeacion planeacion)
        {
            planeacion.FechaUltMod = DateTime.Now;
            planeacion.UsuarioMod = "PEDRO";

            var json = JsonConvert.SerializeObject(planeacion);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPut = await client.PutAsync("api/Planeaciones/UpdatePlaneacion/" + planeacion.IdPlaneacion, content);
            if (respuestaPut.IsSuccessStatusCode)
            {
                return planeacion;
            }
            return null;
        }

        public async Task<string> FicPlaneacionDelete(int IdPlaneacion)
        {

            var respuestaDelete = await client.DeleteAsync("api/Planeaciones/DeletePlaneacion/" + IdPlaneacion);
            if (respuestaDelete.IsSuccessStatusCode)
            {
                return "OK";
            }
            return "ERROR";
        }
    }
}
