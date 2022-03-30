using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReviews.Data.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Comment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Comment",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
