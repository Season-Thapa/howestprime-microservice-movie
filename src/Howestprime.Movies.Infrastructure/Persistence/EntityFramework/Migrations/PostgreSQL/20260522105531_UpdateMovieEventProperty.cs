using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Howestprime.Movies.Infrastructure.Persistence.EntityFramework.Migrations.PostgreSQL
{
    /// <inheritdoc />
    public partial class UpdateMovieEventProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShowTime",
                table: "MovieEvents",
                newName: "Showtime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Showtime",
                table: "MovieEvents",
                newName: "ShowTime");
        }
    }
}
