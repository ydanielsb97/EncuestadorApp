﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EncuestadorApp.Data.Models
{
    public class Encuesta
    {
        public int ID { get; set; }
        public string Titulo { get; set; }
        public string Creador_ID { get; set; }
        public string Creador_Nombre { get; set; }
        public List<Pregunta> Preguntas { get; set; }
    }

    public class Pregunta
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int Encuesta_ID { get; set; }
        [ForeignKey("Encuesta_ID")]
        public Encuesta GetEncuesta { get; set; }
        public List<Respuesta> Respuestas { get; set; }

    }

    public class Respuesta 
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int Pregunta_ID { get; set; }
        [ForeignKey("Pregunta_ID")]
        public Pregunta GetPregunta { get; set; }
    }



}