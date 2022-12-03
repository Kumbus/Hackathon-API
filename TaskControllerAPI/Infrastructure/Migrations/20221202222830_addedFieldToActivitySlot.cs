using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class addedFieldToActivitySlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity Slots_AspNetUsers_UserId",
                table: "Activity Slots");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Activity Slots",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Activity Slots_AspNetUsers_UserId",
                table: "Activity Slots",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity Slots_AspNetUsers_UserId",
                table: "Activity Slots");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Activity Slots",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity Slots_AspNetUsers_UserId",
                table: "Activity Slots",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
