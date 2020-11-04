using Microsoft.EntityFrameworkCore.Migrations;

namespace Fashionplex.Data.Migrations
{
    public partial class productSizeEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "Products",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
