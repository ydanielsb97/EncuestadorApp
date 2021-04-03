using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EncuestadorApp.Data.Models
{
    public class Encuesta
    {
        [Key]
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Creador_ID { get; set; }
        public string Creador_Nombre { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public List<Pregunta> Preguntas { get; set; }
        public List<Respuesta_Por_Usuario> Respuestas { get; set; }
    }

    public class Pregunta
    {
        [Key]
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int Encuesta_ID { get; set; }
        [ForeignKey("Encuesta_ID")]
        public Encuesta GetEncuesta { get; set; }

    }

    public class Respuesta_Por_Usuario
    {
        [Key]
        public int ID { get; set; }
        public string Nombre_Encuestado { get; set; }
        public DateTime Fecha_Completada { get; set; }
        public int Encuesta_ID { get; set; }

        [ForeignKey("Encuesta_ID")]
        public Encuesta GetEncuesta { get; set; }
        public List<Respuesta> Get_Respuestas { get; set; }

    }

    public class Respuesta
    {
        [Key]
        public int ID { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta_Text { get; set; }
        public int Respuestas_ID { get; set; }
        [ForeignKey("Respuestas_ID")]
        public Respuesta_Por_Usuario GetRespuesta { get; set; }

    }

    public class Pregunta_Form
    {
        public int ID { get; set; }
        public string Pregunta { get; set; }
    }

    

}
