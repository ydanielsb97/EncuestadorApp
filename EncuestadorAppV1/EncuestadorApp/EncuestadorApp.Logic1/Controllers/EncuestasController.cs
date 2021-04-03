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


    }
}
