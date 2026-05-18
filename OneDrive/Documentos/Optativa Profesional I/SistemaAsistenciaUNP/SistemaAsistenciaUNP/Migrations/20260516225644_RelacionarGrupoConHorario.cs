using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAsistenciaUNP.Migrations
{
    /// <inheritdoc />
    public partial class RelacionarGrupoConHorario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HorarioId",
                table: "Grupos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_HorarioId",
                table: "Grupos",
                column: "HorarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grupos_Horarios_HorarioId",
                table: "Grupos",
                column: "HorarioId",
                principalTable: "Horarios",
                principalColumn: "HorarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grupos_Horarios_HorarioId",
                table: "Grupos");

            migrationBuilder.DropIndex(
                name: "IX_Grupos_HorarioId",
                table: "Grupos");

            migrationBuilder.DropColumn(
                name: "HorarioId",
                table: "Grupos");
        }
    }
}
