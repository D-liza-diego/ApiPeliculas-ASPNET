using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoAPi.Migrations
{
    public partial class PeliculasActores_PeliculasGeneros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeliculaActores",
                columns: table => new
                {
                    actorid = table.Column<int>(type: "int", nullable: false),
                    peliculaid = table.Column<int>(type: "int", nullable: false),
                    personaje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaActores", x => new { x.actorid, x.peliculaid });
                    table.ForeignKey(
                        name: "FK_PeliculaActores_Actores_actorid",
                        column: x => x.actorid,
                        principalTable: "Actores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaActores_Peliculas_peliculaid",
                        column: x => x.peliculaid,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeliculaGeneros",
                columns: table => new
                {
                    generoid = table.Column<int>(type: "int", nullable: false),
                    peliculaid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaGeneros", x => new { x.peliculaid, x.generoid });
                    table.ForeignKey(
                        name: "FK_PeliculaGeneros_Generos_generoid",
                        column: x => x.generoid,
                        principalTable: "Generos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaGeneros_Peliculas_peliculaid",
                        column: x => x.peliculaid,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaActores_peliculaid",
                table: "PeliculaActores",
                column: "peliculaid");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaGeneros_generoid",
                table: "PeliculaGeneros",
                column: "generoid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculaActores");

            migrationBuilder.DropTable(
                name: "PeliculaGeneros");
        }
    }
}
