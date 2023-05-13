using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMainDiplomProject.Migrations
{
    /// <inheritdoc />
    public partial class slip2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HashTagsPost_Posts_PostHashTagsId1",
                table: "HashTagsPost");

            migrationBuilder.RenameColumn(
                name: "PostHashTagsId1",
                table: "HashTagsPost",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_HashTagsPost_PostHashTagsId1",
                table: "HashTagsPost",
                newName: "IX_HashTagsPost_PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_HashTagsPost_Posts_PostId",
                table: "HashTagsPost",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HashTagsPost_Posts_PostId",
                table: "HashTagsPost");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "HashTagsPost",
                newName: "PostHashTagsId1");

            migrationBuilder.RenameIndex(
                name: "IX_HashTagsPost_PostId",
                table: "HashTagsPost",
                newName: "IX_HashTagsPost_PostHashTagsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_HashTagsPost_Posts_PostHashTagsId1",
                table: "HashTagsPost",
                column: "PostHashTagsId1",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
