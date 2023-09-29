using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Penlog.Data.Migrations
{
    /// <inheritdoc />
    public partial class correctImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Photos_PreviewPhotoId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PreviewPhotoId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "PreviewPhotoId",
                table: "Posts",
                newName: "PreviewImageId");

            migrationBuilder.AddColumn<int>(
                name: "ProfileImageId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PreviewImageId",
                table: "Posts",
                column: "PreviewImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_ProfileImageId",
                table: "AspNetUsers",
                column: "ProfileImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Images_PreviewImageId",
                table: "Posts",
                column: "PreviewImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Images_PreviewImageId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PreviewImageId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImageId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PreviewImageId",
                table: "Posts",
                newName: "PreviewPhotoId");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: true),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PreviewPhotoId",
                table: "Posts",
                column: "PreviewPhotoId",
                unique: true,
                filter: "[PreviewPhotoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Photos_PreviewPhotoId",
                table: "Posts",
                column: "PreviewPhotoId",
                principalTable: "Photos",
                principalColumn: "Id");
        }
    }
}
