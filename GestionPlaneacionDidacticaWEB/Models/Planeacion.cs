using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestionPlaneacionDidacticaWEB.Models
{
    public class eva_planeacion
    {
        [Key]
        [Required]
        public int IdPlaneacion { get; set; }
        public Int16 IdAsignatura { get; set; }
        public string ReferenciaNorma { get; set; }
        public string Revision { get; set; }
        public string Actual { get; set; }
        public string CompetenciaAsignatura { get; set; }
        public string AportacionPerfilEgreso { get; set; }
        public string IdPeriodo { get; set; }
        public DateTime FechaReg { get; set; }
        public string UsuarioReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public string UsuarioUltMod { get; set; }
        public string Borrado { get; set; }
        public string Activo { get; set; }
    }
    public class eva_planeacion_temas
    {
        [Key]
        [Required]
        public Int16 IdTema { get; set; }
        public Int16 IdAsignatura { get; set; }
        public int IdPlaneacion { get; set; }
        public string DesTema { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaReg { get; set; }
        public string UsuarioReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public string UsuarioUltMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }
    }
    public class eva_planeacion_subtemas
    {
        [Key]
        [Required]
        public Int16 IdSubtema { get; set; }
        public Int16 IdAsignatura { get; set; }
        public int IdPlaneacion { get; set; }
        public Int16 IdTema { get; set; }
        public string DesSubtema { get; set; }
        public DateTime FechaReg { get; set; }
        public string UsuarioReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public string UsuarioUltMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }
    }
    public class eva_planeacion_fuentes
    {
        public Int16 IdAsignatura { get; set; }
        public int IdPlaneacion { get; set; }
        public Int16 IdFuente { get; set; }
        public Int16 Prioridad { get; set; }
        public string UsuarioReg { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public string UsuarioUltMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }
    }
    public class eva_planeacion_apoyos
    {
        public Int16 IdAsignatura { get; set; }
        public int IdPlaneacion { get; set; }
        public Int16 IdApoyoDidactico { get; set; }
        public DateTime FechaReg { get; set; }
        public string UsuarioReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public string UsuarioUltMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }
    }
    public class eva_planeacion_temas_competencias
    {
        public Int16 IdAsignatura { get; set; }
        public int IdPlaneacion { get; set; }
        public Int16 IdTema { get; set; }
        public int IdCompetencia { get; set; }
        public string Observaciones { get; set; }
        public DateTime FechaReg { get; set; }
        public string UsuarioReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public string UsuarioUltMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }
    }
    public class eva_planeacion_aprendizaje
    {
        public Int16 IdAsignatura { get; set; }
        public int IdPlaneacion { get; set; }
        public Int16 IdTema { get; set; }
        public int IdCompetencia { get; set; }
        public int IdActividadAprendizaje { get; set; }
        public DateTime FechaReg { get; set; }
        public string UsuarioReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public string UsuarioUltMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }
    }
    public class eva_planeacion_enseñanza
    {
        public Int16 IdAsignaturas { get; set; }
        public int IdPlaneacion { get; set; }
        public Int16 IdTema { get; set; }
        public int IdCompetencia { get; set; }
        public int IdActividadEnseñanza { get; set; }
        public DateTime FechaReg { get; set; }
        public DateTime FechaProgramada { get; set; }
        public DateTime FechaRealizada { get; set; }
        public string UsuarioReg { get; set; }
        public DateTime FechaUltMod { get; set; }
        public string UsuarioUltMod { get; set; }
        public string Activo { get; set; }
        public string Borrado { get; set; }
    }
    public class eva_planeacion_criterios_evalua
    {

    }
}
