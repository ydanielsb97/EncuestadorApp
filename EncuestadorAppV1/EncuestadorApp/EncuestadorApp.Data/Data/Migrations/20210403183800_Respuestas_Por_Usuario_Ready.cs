using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EncuestadorApp.Logic1.Data.Migrations
{
    public partial class Respuestas_Por_Usuario_Ready : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_Preguntas_Pregunta_ID",
                table: "Respuestas");

            migrationBuilder.RenameColumn(
                name: "Pregunta_ID",
                table: "Respuestas",
                newName: "Respuestas_ID");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Respuestas",
                newName: "Respuesta_Text");

            migrationBuilder.RenameIndex(
                name: "IX_Respuestas_Pregunta_ID",
                table: "Respuestas",
                newName: "IX_Respuestas_Respuestas_ID");

            migrationBuilder.AddColumn<string>(
                name: "Pregunta",
                table: "Respuestas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Respuestas_Por_Usuario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Encuestado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha_Completada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Encuesta_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas_Por_Usuario", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Respuestas_Por_Usuario_Encuestas_Encuesta_ID",
                        column: x => x.Encuesta_ID,
                        principalTable: "Encuestas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Respuestas_Por_Usuario_Encuesta_ID",
                table: "Respuestas_Por_Usuario",
                column: "Encuesta_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_Respuestas_Por_Usuario_Respuestas_ID",
                table: "Respuestas",
                column: "Respuestas_ID",
                principalTable: "Respuestas_Por_Usuario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Respuestas_Respuestas_Por_Usuario_Respuestas_ID",
                table: "Respuestas");

            migrationBuilder.DropTable(
                name: "Respuestas_Por_Usuario");

            migrationBuilder.DropColumn(
                name: "Pregunta",
                table: "Respuestas");

            migrationBuilder.RenameColumn(
                name: "Respuestas_ID",
                table: "Respuestas",
                newName: "Pregunta_ID");

            migrationBuilder.RenameColumn(
                name: "Respuesta_Text",
                table: "Respuestas",
                newName: "Descripcion");

            migrationBuilder.RenameIndex(
                name: "IX_Respuestas_Respuestas_ID",
                table: "Respuestas",
                newName: "IX_Respuestas_Pregunta_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Respuestas_Preguntas_Pregunta_ID",
                table: "Respuestas",
                column: "Pregunta_ID",
                principalTable: "Preguntas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
