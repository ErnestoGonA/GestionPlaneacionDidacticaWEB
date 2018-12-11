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
        public async Task<List<eva_planeacion>> FicGetListTemas()
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
    }
}
