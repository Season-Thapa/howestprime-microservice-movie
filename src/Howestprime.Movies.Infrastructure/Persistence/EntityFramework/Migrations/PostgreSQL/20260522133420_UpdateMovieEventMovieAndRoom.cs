using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Migrations.PostgreSQL
{
    /// <inheritdoc />
    public partial class UpdateMovieEventMovieAndRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MovieEvents_MovieId",
                table: "MovieEvents",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieEvents_RoomId",
                table: "MovieEvents",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEvents_Movies_MovieId",
                table: "MovieEvents",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEvents_Rooms_RoomId",
                table: "MovieEvents",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieEvents_Movies_MovieId",
                table: "MovieEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieEvents_Rooms_RoomId",
                table: "MovieEvents");

            migrationBuilder.DropIndex(
                name: "IX_MovieEvents_MovieId",
                table: "MovieEvents");

            migrationBuilder.DropIndex(
                name: "IX_MovieEvents_RoomId",
                table: "MovieEvents");
        }
    }
}
