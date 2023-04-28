using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class addLikeInComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Like",
                table: "Comments",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Like",
                table: "CommentDeposits",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Like",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Like",
                table: "CommentDeposits");
        }
    }
}
