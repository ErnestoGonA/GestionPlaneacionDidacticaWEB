using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using GestionPlaneacionDidacticaWEB.Models;

namespace GestionPlaneacionDidacticaWEB.Areas.Planeacion.Services
{
    public class FicSrvPlaneacionEnsenseñanza
    {
        HttpClient FiClient;
        public FicSrvPlaneacionEnsenseñanza()
        {
            this.FiClient = new HttpClient();
            this.FiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //List All 
        public async Task<List<extended_eva_planeacion_enseñanza>> FicGetListPlaneacionEnseñanza()
        {
            HttpResponseMessage FicResponse = await this.FiClient.GetAsync("http://localhost:53483/api/PlaneacionEnseñanzaExtended");
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<extended_eva_planeacion_enseñanza >>(FicRespuesta);

            }
            return new List<extended_eva_planeacion_enseñanza>();
        }
        //Detail
        public async Task<extended_eva_planeacion_enseñanza> FicGetDetailPlaneacionEnseñanza(short IdAsignatura, int IdPlaneacion, short IdTema, int IdCompetencia, int IdActividadEnseñanza)
        {
            HttpResponseMessage FicResponse = await this.FiClient.GetAsync("http://localhost:53483/api/OnePlaneacionEnseñanzaExtended/" + IdAsignatura+"/"+ IdPlaneacion+"/"+ IdTema+"/"+ IdCompetencia+"/"+ IdActividadEnseñanza);
            if (FicResponse.IsSuccessStatusCode)
            {
                var FicRespuesta = await FicResponse.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<extended_eva_planeacion_enseñanza>(FicRespuesta);
            }
            return new extended_eva_planeacion_enseñanza();
        }

        //Delete -------------------------------------------------------------       
        public async Task<string> FicDeletePlaneacionEnseñanza(short IdAsignatura, int IdPlaneacion, short IdTema, int IdCompetencia, int IdActividadEnseñanza)
        {
            HttpResponseMessage FicRespuesta = await this.FiClient.DeleteAsync("http://localhost:53483/api/DeletePlaneacionEnseñanza/"+IdAsignatura+"/"+IdPlaneacion+"/"+IdTema+"/"+IdCompetencia+"/"+IdActividadEnseñanza );
            if (FicRespuesta.IsSuccessStatusCode)
            {
                return "OK";
            }
            return "ERROR";
        }

        //Update
        public async Task<extended_eva_planeacion_enseñanza> FicUpdatePlaneacionEnseñanza(short IdAsignatura, int IdPlaneacion, short IdTema, int IdCompetencia, int IdActividadEnseñanza, extended_eva_planeacion_enseñanza epe)
        {
            epe.Activo = "S";
            epe.Borrado = "N";
            epe.UsuarioReg = "root";
            epe.FechaUltMod = DateTime.Now;

            

            Console.WriteLine("IdActividadEnseñanza: "+ IdActividadEnseñanza);

            var json = JsonConvert.SerializeObject(epe);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var respuestaPut = await FiClient.PutAsync("http://localhost:53483/api/UpdatePlaneacionEnseñanza/"+IdAsignatura + "/" + IdPlaneacion + "/" + IdTema + "/" + IdCompetencia + "/" + IdActividadEnseñanza, content);
            if (respuestaPut.IsSuccessStatusCode)
            {
                return epe;
            }
            return null;
        }
    }
}
