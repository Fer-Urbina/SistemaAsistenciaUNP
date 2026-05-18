using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAsistenciaUNP.Migrations
{
    /// <inheritdoc />
    public partial class RelacionCarreraCentro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_CentrosUniversitarios_CentroUniversitarioId",
                table: "Aulas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CentrosUniversitarios",
                table: "CentrosUniversitarios");

            migrationBuilder.RenameTable(
                name: "CentrosUniversitarios",
                newName: "CentroUniversitario");

            migrationBuilder.AddColumn<int>(
                name: "CentroUniversitarioId",
                table: "Carreras",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CentroUniversitario",
                table: "CentroUniversitario",
                column: "CentroUniversitarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Carreras_CentroUniversitarioId",
                table: "Carreras",
                column: "CentroUniversitarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_CentroUniversitario_CentroUniversitarioId",
                table: "Aulas",
                column: "CentroUniversitarioId",
                principalTable: "CentroUniversitario",
                principalColumn: "CentroUniversitarioId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carreras_CentroUniversitario_CentroUniversitarioId",
                table: "Carreras",
                column: "CentroUniversitarioId",
                principalTable: "CentroUniversitario",
                principalColumn: "CentroUniversitarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aulas_CentroUniversitario_CentroUniversitarioId",
                table: "Aulas");

            migrationBuilder.DropForeignKey(
                name: "FK_Carreras_CentroUniversitario_CentroUniversitarioId",
                table: "Carreras");

            migrationBuilder.DropIndex(
                name: "IX_Carreras_CentroUniversitarioId",
                table: "Carreras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CentroUniversitario",
                table: "CentroUniversitario");

            migrationBuilder.DropColumn(
                name: "CentroUniversitarioId",
                table: "Carreras");

            migrationBuilder.RenameTable(
                name: "CentroUniversitario",
                newName: "CentrosUniversitarios");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CentrosUniversitarios",
                table: "CentrosUniversitarios",
                column: "CentroUniversitarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aulas_CentrosUniversitarios_CentroUniversitarioId",
                table: "Aulas",
                column: "CentroUniversitarioId",
                principalTable: "CentrosUniversitarios",
                principalColumn: "CentroUniversitarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
