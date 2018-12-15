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
    public class FicSrvSubtemas
    {
        HttpClient client;

        public FicSrvSubtemas()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri("http://localhost:53483/");
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<eva_planeacion_subtemas>> FicGetListSubtemas(eva_planeacion_temas tema)
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Planeacion/"+tema.IdPlaneacion+"/Temas/"+ tema.IdTema+ "/Subtemas/"+ tema.IdAsignatura + "");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_planeacion_subtemas>>(FicRespuesta);
            }
            return new List<eva_planeacion_subtemas>();
        }

        public async Task<eva_planeacion_subtemas> FicGetSubtema(int IdPlaneacion, short IdTema,short IdAsignatura, short IdSubtema)
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Planeacion/"+ IdPlaneacion + "/" + IdAsignatura + "/" + IdTema + "/" + IdSubtema + "");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<eva_planeacion_subtemas>(FicRespuesta);
            }
            //return null;
            return new eva_planeacion_subtemas();
        }

        public async Task<eva_planeacion_subtemas> FicSubtemaCreate(eva_planeacion_subtemas Subtema)
        {

            var json = JsonConvert.SerializeObject(Subtema);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPost = await client.PostAsync("api/Planeacion/Temas/Subtema", content);
            if (respuestaPost.IsSuccessStatusCode)
            {
                return Subtema;
            }
            return null;
        }

        public async Task<eva_planeacion_subtemas> FicSubtemasUpdate(eva_planeacion_subtemas Subtema)
        {
            Subtema.FechaUltMod = DateTime.Now;
            Subtema.UsuarioUltMod = "Bryan";

            var json = JsonConvert.SerializeObject(Subtema);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPut = await client.PutAsync("api/Planeacion/Temas/Subtema", content);
            if (respuestaPut.IsSuccessStatusCode)
            {
                return Subtema;
            }
            return null;
        }

        public async Task<string> FicSubtemasDelete(short IdSubtema)
        {

            var respuestaDelete = await client.DeleteAsync("api/Planeacion/Temas/Subtemas/" + IdSubtema);
            if (respuestaDelete.IsSuccessStatusCode)
            {
                return "OK";
            }
            return "ERROR";
        }
    }
}
