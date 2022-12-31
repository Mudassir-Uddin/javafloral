using Microsoft.EntityFrameworkCore.Migrations;

namespace Java_Floral.Migrations
{
    public partial class update_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_ID",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User_ID",
                table: "Orders",
                type: "int",
                nullable: true);
        }
    }
}
