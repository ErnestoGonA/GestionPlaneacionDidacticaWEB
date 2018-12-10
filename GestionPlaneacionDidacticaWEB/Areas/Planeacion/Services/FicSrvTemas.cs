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
    public class FicSrvTemas
    {
        HttpClient client;

        public FicSrvTemas()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri("http://localhost:53483/");
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<eva_planeacion_temas>> FicGetListTemas()
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Planeacion/1/Temas");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_planeacion_temas>>(FicRespuesta);
            }
            //return null;
            return new List<eva_planeacion_temas>();
        }

        public async Task<eva_planeacion_temas> FicGetTema(int IdPlaneacion,short IdTema)
        {
            HttpResponseMessage FicResponse = await this.client.GetAsync("api/Planeacion/"+IdPlaneacion+"/Temas/"+IdTema);
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
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

    }
}
