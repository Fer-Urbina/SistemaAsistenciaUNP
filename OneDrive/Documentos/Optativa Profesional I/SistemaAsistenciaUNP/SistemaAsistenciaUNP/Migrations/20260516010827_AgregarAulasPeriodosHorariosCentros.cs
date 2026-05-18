using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAsistenciaUNP.Migrations
{
    /// <inheritdoc />
    public partial class AgregarAulasPeriodosHorariosCentros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CentrosUniversitarios",
                columns: table => new
                {
                    CentroUniversitarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentrosUniversitarios", x => x.CentroUniversitarioId);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    HorarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaSemana = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeSpan>(type: "time", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.HorarioId);
                });

            migrationBuilder.CreateTable(
                name: "PeriodosAcademicos",
                columns: table => new
                {
                    PeriodoAcademicoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodosAcademicos", x => x.PeriodoAcademicoId);
                });

            migrationBuilder.CreateTable(
                name: "Aulas",
                columns: table => new
                {
                    AulaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CentroUniversitarioId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aulas", x => x.AulaId);
                    table.ForeignKey(
                        name: "FK_Aulas_CentrosUniversitarios_CentroUniversitarioId",
                        column: x => x.CentroUniversitarioId,
                        principalTable: "CentrosUniversitarios",
                        principalColumn: "CentroUniversitarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aulas_CentroUniversitarioId",
                table: "Aulas",
                column: "CentroUniversitarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aulas");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "PeriodosAcademicos");

            migrationBuilder.DropTable(
                name: "CentrosUniversitarios");
        }
    }
}
