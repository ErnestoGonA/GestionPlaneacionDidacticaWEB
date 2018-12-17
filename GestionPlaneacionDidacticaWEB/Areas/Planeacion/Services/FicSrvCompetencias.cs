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
    public class FicSrvCompetencias
    {
        
        HttpClient client;

        public FicSrvCompetencias()
        {
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri("http://localhost:53483/");
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<eva_planeacion_temas_competencias>> FicGetListCompetencias(short IdAsignatura, int IdPlaneacion,short IdTema)
        {
        HttpResponseMessage FicResponse = await this.client.GetAsync("api/Asignatura/" + IdAsignatura + "/Planeacion/" + IdPlaneacion + "/Temas/"+IdTema+ "/Competencias");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<eva_planeacion_temas_competencias>>(FicRespuesta);
            }
            //return null;
            return new List<eva_planeacion_temas_competencias>();
        }
    }
}
