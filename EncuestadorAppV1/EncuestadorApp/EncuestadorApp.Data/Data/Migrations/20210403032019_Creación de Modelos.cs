using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EncuestadorApp.Logic1.Data.Migrations
{
    public partial class CreacióndeModelos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Encuestas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creador_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creador_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuestas", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Preguntas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Encuesta_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preguntas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Preguntas_Encuestas_Encuesta_ID",
                        column: x => x.Encuesta_ID,
                        principalTable: "Encuestas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pregunta_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Respuestas_Preguntas_Pregunta_ID",
                        column: x => x.Pregunta_ID,
                        principalTable: "Preguntas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Preguntas_Encuesta_ID",
                table: "Preguntas",
                column: "Encuesta_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_Pregunta_ID",
                table: "Respuestas",
                column: "Pregunta_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Respuestas");

            migrationBuilder.DropTable(
                name: "Preguntas");

            migrationBuilder.DropTable(
                name: "Encuestas");
        }
    }
}
