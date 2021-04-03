using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncuestadorApp.Data.Models;
using EncuestadorApp.Data.Data;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace EncuestadorApp.Logic1.Controllers
{
    [Authorize]
    public class EncuestasController : Controller
    {
        private readonly ApplicationDbContext db;

        public EncuestasController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Visualizar_Encuesta()
        {
            return View();
        }

        public IActionResult Aplicar_Encuesta()
        {
            return View();
        }

        public JsonResult Crear_Encuesta(List<Pregunta_Form> Lista_Preguntas)
        {
            var Usuario_Loguedo = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var Cant_Encuestas = db.Encuestas.Count() + 1;

            var Model_Encuesta = new Encuesta
            {
                Creador_Nombre = $"{Usuario_Loguedo.Nombres} {Usuario_Loguedo.Apellidos}",
                Titulo = $"Encuesta #{Cant_Encuestas}",
                Fecha_Creacion = DateTime.Now,
                Creador_ID = Usuario_Loguedo.Id
            };

            db.Encuestas.Add(Model_Encuesta);
            db.SaveChanges();

            foreach (var item in Lista_Preguntas)
            {
                var pregunta = new Pregunta
                {
                    Encuesta_ID = Model_Encuesta.ID,
                    Descripcion = item.Pregunta
                };

                db.Preguntas.Add(pregunta);
                db.SaveChanges();
            }

            return Json(new { title = "Encuesta", text = "Encuesta creada exitósamente", icon = "success" });
        }
        public JsonResult Guardar_Edicion_Encuesta(int Encuesta_ID, List<Pregunta> Lista_Preguntas)
        {
            var Usuario_Loguedo = db.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var Todas_Las_Preguntas = db.Preguntas.Where(x => x.Encuesta_ID == Encuesta_ID).ToList();

            //var Cant_Encuestas = db.Encuestas.Count() + 1;

            //var Model_Encuesta = new Encuesta
            //{
            //    Creador_Nombre = $"{Usuario_Loguedo.Nombres} {Usuario_Loguedo.Apellidos}",
            //    Titulo = $"Encuesta #{Cant_Encuestas}",
            //    Fecha_Creacion = DateTime.Now,
            //    Creador_ID = Usuario_Loguedo.Id
            //};

            //db.Encuestas.Add(Model_Encuesta);
            //db.SaveChanges();

            foreach (var item in Todas_Las_Preguntas)
            {

                var existe = Lista_Preguntas.Exists(x => x.ID == item.ID);

                if (existe)
                {

                    var pregunta = Lista_Preguntas.Where(x => x.ID == item.ID).FirstOrDefault();

                    item.Descripcion = pregunta.Descripcion;
                    db.SaveChanges();
                }
                else
                {
                    db.Preguntas.Remove(item);
                    db.SaveChanges();
                }


            }



            foreach (var item in Lista_Preguntas)
            {

                var pregunta = db.Preguntas.Find(item.ID);

                if (pregunta != null)
                {

                    pregunta.Descripcion = item.Descripcion;
                    db.SaveChanges();
                }
                else
                {
                    var model = new Pregunta
                    {

                        Descripcion = item.Descripcion,
                        Encuesta_ID = Encuesta_ID,
                    };

                    db.Preguntas.Add(model);
                    db.SaveChanges();
                }
            }

            return Json(new { title = "Encuesta", text = "Encuesta editada exitósamente", icon = "success" });
        }
        public JsonResult Obtener_Encuesta()
        {


            var Lista_Encuestas = db.Encuestas.ToList();

            return Json(new { Lista_Encuestas });
        }

        public JsonResult Obtener_Encuestas_Respondidas()
        {


            var Lista_Encuestas = db.Encuestas.Include(x => x.Respuestas)
                .Select(x => new Encuesta
                {
                    ID = x.ID,
                    Fecha_Creacion = x.Fecha_Creacion,
                    Titulo = x.Titulo,
                    Respuestas = (List<Respuesta_Por_Usuario>)x.Respuestas.Select(x => new Respuesta_Por_Usuario { 
                        ID = x.ID,
                        Nombre_Encuestado = x.Nombre_Encuestado,
                        Fecha_Completada = x.Fecha_Completada,
                        Get_Respuestas = (List<Respuesta>)x.Get_Respuestas.Select(x => new Respuesta { Pregunta = x.Pregunta, Respuesta_Text = x.Respuesta_Text})
                        
                    })

                }).ToList();

            return Json(new { Lista_Encuestas });
        }

        public JsonResult Preguntas_Encuesta(int Id_Encuestas)
        {

            var preguntas = db.Preguntas.Where(x => x.Encuesta_ID == Id_Encuestas).ToList();




            return Json(new { preguntas });
        }
        public JsonResult Elimnar_Encuesta (int Id_Encuestas)
        {
            var encuesta = db.Encuestas.Find(Id_Encuestas);

            if(encuesta != null)
            {
                db.Encuestas.Remove(encuesta);
                db.SaveChanges();
                return Json(new { title = "Encuesta", text = "Encuesta eliminada exitósamente", icon = "success" });

            }
            else
            {
                return Json(new { title = "Encuesta", text = "No se encuentra la encuesta seleccionada", icon = "info" });

            }

        }
        public JsonResult Enviar_Respuestas(string Nombre_Usuario, int Encuesta_ID, List<Respuesta> Respuestas)
        {

            var Respuestas_Por_Usuario = new Respuesta_Por_Usuario
            {
                Fecha_Completada = DateTime.Now,
                Nombre_Encuestado = Nombre_Usuario,
                Encuesta_ID = Encuesta_ID
            };

            db.Respuestas_Por_Usuario.Add(Respuestas_Por_Usuario);
            db.SaveChanges();

            foreach (var item in Respuestas)
            {
                var model = new Respuesta
                {
                    Respuestas_ID = Respuestas_Por_Usuario.ID,
                    Pregunta = item.Pregunta,
                    Respuesta_Text = item.Respuesta_Text
                };

                db.Respuestas.Add(model);
                db.SaveChanges();
            }



            return Json(new { title = "Encuestas", text = "Sus respuestas fueron enviadas exitósamente", icon = "success" });
        }

        


    }
}
