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
    public class FicSrvCriterios
    {
        HttpClient client;

        public FicSrvCriterios()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri("http://localhost:53483/");
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<eva_planeacion_criterios_evalua>> GetListCriterios(short IdAsignatura, int IdPlaneacion, short IdTema,short IdCompetencia)
        {
            HttpResponseMessage FicResponse = 
                await this.client
                .GetAsync("api/Asignatura/" + IdAsignatura + "/Planeacion/" + IdPlaneacion + "/Temas/" + IdTema + "/Competencias/"+IdCompetencia+"/criterios");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_planeacion_criterios_evalua>>(FicRespuesta);
            }
            return new List<eva_planeacion_criterios_evalua>();
        }

        public async Task<eva_planeacion_criterios_evalua> GetCriterio(short IdAsignatura, int IdPlaneacion, short IdTema, short IdCompetencia,short IdCriterio)
        {
            HttpResponseMessage FicResponse =
                await this.client
                .GetAsync("api/Asignatura/" + IdAsignatura + "/Planeacion/" + IdPlaneacion + "/Temas/" + IdTema + "/Competencias/" + IdCompetencia + "/criterios/"+IdCriterio);
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<eva_planeacion_criterios_evalua>(FicRespuesta);
            }
            return new eva_planeacion_criterios_evalua();
        }

        public async Task<eva_planeacion_criterios_evalua> CreateCriterio(eva_planeacion_criterios_evalua Tema)
        {
            var json = JsonConvert.SerializeObject(Tema);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPost = await client.PostAsync("api/criterio", content);
            if (respuestaPost.IsSuccessStatusCode)
            {
                return Tema;
            }
            return null;
        }

        public async Task<string> DeleteCriterio(eva_planeacion_criterios_evalua criterio)
        {

            var respuestaDelete = await client.DeleteAsync("api/asignatura/" + criterio.IdAsignatura + "/planeacion/" + criterio.IdPlaneacion + "/Temas/" + criterio.IdTema+"/competencias/"+ criterio.IdCompetencia+"/criterios/"+ criterio.IdCriterio);
            if (respuestaDelete.IsSuccessStatusCode)
            {
                return "OK";
            }
            return "ERROR";
        }

        public async Task<eva_planeacion_criterios_evalua> PUTCriterio(eva_planeacion_criterios_evalua Tema)
        {
            var json = JsonConvert.SerializeObject(Tema);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPost = await client.PutAsync("api/criterio", content);
            if (respuestaPost.IsSuccessStatusCode)
            {
                return Tema;
            }
            return null;
        }
    }
}
