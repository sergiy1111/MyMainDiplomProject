using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMainDiplomProject.Migrations
{
    /// <inheritdoc />
    public partial class slip1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostHashTags");

            migrationBuilder.CreateTable(
                name: "HashTagsPost",
                columns: table => new
                {
                    PostHashTagsId = table.Column<int>(type: "int", nullable: false),
                    PostHashTagsId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashTagsPost", x => new { x.PostHashTagsId, x.PostHashTagsId1 });
                    table.ForeignKey(
                        name: "FK_HashTagsPost_HashTags_PostHashTagsId",
                        column: x => x.PostHashTagsId,
                        principalTable: "HashTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HashTagsPost_Posts_PostHashTagsId1",
                        column: x => x.PostHashTagsId1,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HashTagsPost_PostHashTagsId1",
                table: "HashTagsPost",
                column: "PostHashTagsId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HashTagsPost");

            migrationBuilder.CreateTable(
                name: "PostHashTags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashTagsId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    HashTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostHashTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostHashTags_HashTags_HashTagsId",
                        column: x => x.HashTagsId,
                        principalTable: "HashTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostHashTags_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostHashTags_HashTagsId",
                table: "PostHashTags",
                column: "HashTagsId");

            migrationBuilder.CreateIndex(
                name: "IX_PostHashTags_PostId",
                table: "PostHashTags",
                column: "PostId");
        }
    }
}
