using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReviews.Data.Migrations
{
    public partial class AdditionalReviewCommentRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewId",
                table: "Review",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReviewId",
                table: "Review",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Review_ReviewId",
                table: "Review",
                column: "ReviewId",
                principalTable: "Review",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_Review_ReviewId",
                table: "Review");

            migrationBuilder.DropIndex(
                name: "IX_Review_ReviewId",
                table: "Review");

            migrationBuilder.DropColumn(
                name: "ReviewId",
                table: "Review");
        }
    }
}
