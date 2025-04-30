using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beMahou.Migrations
{
    /// <inheritdoc />
    public partial class FixImagenFileMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Publicaciones");

            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Publicaciones");

            migrationBuilder.AlterColumn<int>(
                name: "EstrellasAcumuladas",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                comment: "Estrellas totales acumulados por el usuario",
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AvatarPath",
                table: "Usuarios",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Usuarios",
                type: "TEXT",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "Estrellas",
                table: "Publicaciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                comment: "Puntos acumulables (antes llamado estrellas)",
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Evento",
                table: "Publicaciones",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FotoPath",
                table: "Publicaciones",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EsUtil",
                table: "Comentarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Comentarios",
                type: "TEXT",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UsuarioId",
                table: "Usuarios",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_Estrellas",
                table: "Publicaciones",
                column: "Estrellas");

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_Evento",
                table: "Publicaciones",
                column: "Evento");

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_Fecha",
                table: "Publicaciones",
                column: "Fecha");

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_UsuarioId",
                table: "Publicaciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_UsuarioId",
                table: "Comentarios",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_AspNetUsers_UsuarioId",
                table: "Usuarios",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_AspNetUsers_UsuarioId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_UsuarioId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Publicaciones_Estrellas",
                table: "Publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Publicaciones_Evento",
                table: "Publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Publicaciones_Fecha",
                table: "Publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Publicaciones_UsuarioId",
                table: "Publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_UsuarioId",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "AvatarPath",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Evento",
                table: "Publicaciones");

            migrationBuilder.DropColumn(
                name: "FotoPath",
                table: "Publicaciones");

            migrationBuilder.DropColumn(
                name: "EsUtil",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Comentarios");

            migrationBuilder.AlterColumn<int>(
                name: "EstrellasAcumuladas",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0,
                oldComment: "Estrellas totales acumulados por el usuario");

            migrationBuilder.AlterColumn<int>(
                name: "Estrellas",
                table: "Publicaciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0,
                oldComment: "Estrellas acumulables (antes llamado estrellas)");

            migrationBuilder.AddColumn<int>(
                name: "Categoria",
                table: "Publicaciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Publicaciones",
                type: "TEXT",
                nullable: true);
        }
    }
}
