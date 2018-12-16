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
    public class FicSrvFuentes
    {
        HttpClient client;

        public FicSrvFuentes()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri("http://localhost:53483/");
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<eva_planeacion_fuentes>> FicGetListFuentes(short idPlaneacion, short idAsignatura)
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Planeacion/"+idPlaneacion+"/Fuentes/"+idAsignatura);
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_planeacion_fuentes>>(FicRespuesta);
            }
            return new List<eva_planeacion_fuentes>();
        }

        public async Task<eva_planeacion_fuentes> FicGetFuente(short IdPlaneacion, short IdAsignatura, short IdFuente)
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Planeacion/" + IdPlaneacion + "/Fuentes/" + IdAsignatura + "/" + IdFuente);
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<eva_planeacion_fuentes>(FicRespuesta);
            }
            //return null;
            return new eva_planeacion_fuentes();
        }

        public async Task<eva_planeacion_fuentes> FicFuentesCreate(eva_planeacion_fuentes Fuentes)
        {
            var json = JsonConvert.SerializeObject(Fuentes);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPost = await client.PostAsync("api/NewFuente/", content);
            if (respuestaPost.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine("\n\n\n\n\n\n\n\n\n\n\n2" + respuestaPost.Content);

                return Fuentes;
            }
            return null;
        }

        public async Task<eva_planeacion_fuentes> FicFuentesUpdate(eva_planeacion_fuentes Fuentes)
        {
            Fuentes.FechaUltMod = DateTime.Now;
            Fuentes.UsuarioUltMod = "Bryan";
       
            var json = JsonConvert.SerializeObject(Fuentes);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPut = await client.PutAsync("api/UpdateFuente/", content);
            if (respuestaPut.IsSuccessStatusCode)
            {
                return Fuentes;
            }
            return null;
        }

        public async Task<string> FicFuentesDelete(short idFuente, short idAsignatura, short idPlaneacion)
        {

            var respuestaDelete = await client.DeleteAsync("api/DeleteFuente/" + idFuente+"/"+idAsignatura+"/"+idPlaneacion);
            if (respuestaDelete.IsSuccessStatusCode)
            {
                return "OK";
            }
            return "ERROR";
        }







    }//class
}
