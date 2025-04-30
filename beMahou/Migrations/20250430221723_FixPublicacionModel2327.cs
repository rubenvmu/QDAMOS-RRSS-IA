using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beMahou.Migrations
{
    /// <inheritdoc />
    public partial class FixPublicacionModel2327 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Publicaciones_PublicacionId1",
                table: "Comentarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_Usuarios_UsuarioMahouId",
                table: "Publicaciones");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Publicaciones_UsuarioMahouId",
                table: "Publicaciones");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_PublicacionId1",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "UsuarioMahouId",
                table: "Publicaciones");

            migrationBuilder.DropColumn(
                name: "PublicacionId1",
                table: "Comentarios");

            migrationBuilder.AlterColumn<int>(
                name: "Estrellas",
                table: "Publicaciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 0,
                oldComment: "Puntos acumulables (antes llamado estrellas)");

            migrationBuilder.AddColumn<string>(
                name: "AvatarPath",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstrellasAcumuladas",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_AspNetUsers_UsuarioId",
                table: "Publicaciones",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publicaciones_AspNetUsers_UsuarioId",
                table: "Publicaciones");

            migrationBuilder.DropColumn(
                name: "AvatarPath",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EstrellasAcumuladas",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");

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
                name: "UsuarioMahouId",
                table: "Publicaciones",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicacionId1",
                table: "Comentarios",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    AvatarPath = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    EstrellasAcumuladas = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0, comment: "Puntos totales acumulados por el usuario"),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UsuarioId = table.Column<string>(type: "TEXT", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_UsuarioMahouId",
                table: "Publicaciones",
                column: "UsuarioMahouId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_PublicacionId1",
                table: "Comentarios",
                column: "PublicacionId1");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UsuarioId",
                table: "Usuarios",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Publicaciones_PublicacionId1",
                table: "Comentarios",
                column: "PublicacionId1",
                principalTable: "Publicaciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Publicaciones_Usuarios_UsuarioMahouId",
                table: "Publicaciones",
                column: "UsuarioMahouId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
