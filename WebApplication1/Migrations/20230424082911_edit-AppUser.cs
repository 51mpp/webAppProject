using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class editAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentDeposits_Deposits_DepositId",
                table: "CommentDeposits");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentDeposits_MainPoses_MainPoseId",
                table: "CommentDeposits");

            migrationBuilder.DropIndex(
                name: "IX_CommentDeposits_MainPoseId",
                table: "CommentDeposits");

            migrationBuilder.DropColumn(
                name: "MainPoseId",
                table: "CommentDeposits");

            migrationBuilder.AlterColumn<int>(
                name: "DepositId",
                table: "CommentDeposits",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepositId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDeposits_Deposits_DepositId",
                table: "CommentDeposits",
                column: "DepositId",
                principalTable: "Deposits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentDeposits_Deposits_DepositId",
                table: "CommentDeposits");

            migrationBuilder.DropColumn(
                name: "DepositId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "DepositId",
                table: "CommentDeposits",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MainPoseId",
                table: "CommentDeposits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CommentDeposits_MainPoseId",
                table: "CommentDeposits",
                column: "MainPoseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDeposits_Deposits_DepositId",
                table: "CommentDeposits",
                column: "DepositId",
                principalTable: "Deposits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentDeposits_MainPoses_MainPoseId",
                table: "CommentDeposits",
                column: "MainPoseId",
                principalTable: "MainPoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
