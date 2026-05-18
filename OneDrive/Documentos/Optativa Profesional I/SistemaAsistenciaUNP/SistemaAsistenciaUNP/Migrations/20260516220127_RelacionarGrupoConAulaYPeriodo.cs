using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAsistenciaUNP.Migrations
{
    /// <inheritdoc />
    public partial class RelacionarGrupoConAulaYPeriodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aula",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "PeriodoAcademico",
                table: "Grupos");

            migrationBuilder.AddColumn<int>(
                name: "AulaId",
                table: "Grupos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodoAcademicoId",
                table: "Grupos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_AulaId",
                table: "Grupos",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_PeriodoAcademicoId",
                table: "Grupos",
                column: "PeriodoAcademicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_Aulas_AulaId",
                table: "Grupos",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "AulaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_PeriodosAcademicos_PeriodoAcademicoId",
                table: "Grupos",
                column: "PeriodoAcademicoId",
                principalTable: "PeriodosAcademicos",
                principalColumn: "PeriodoAcademicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Aulas_AulaId",
                table: "Grupos");

            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_PeriodosAcademicos_PeriodoAcademicoId",
                table: "Grupos");

            migrationBuilder.DropIndex(
                name: "IX_Grupos_AulaId",
                table: "Grupos");

            migrationBuilder.DropIndex(
                name: "IX_Grupos_PeriodoAcademicoId",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "AulaId",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "PeriodoAcademicoId",
                table: "Grupos");

            migrationBuilder.AddColumn<string>(
                name: "Aula",
                table: "Grupos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PeriodoAcademico",
                table: "Grupos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
