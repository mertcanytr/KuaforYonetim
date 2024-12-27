using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuaforYonetim1.Migrations
{
    public partial class AddServicesTable5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Salons_SalonId",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "SalonId",
                table: "Services",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Salons_SalonId",
                table: "Services",
                column: "SalonId",
                principalTable: "Salons",
                principalColumn: "SalonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Salons_SalonId",
                table: "Services");

            migrationBuilder.AlterColumn<int>(
                name: "SalonId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Salons_SalonId",
                table: "Services",
                column: "SalonId",
                principalTable: "Salons",
                principalColumn: "SalonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
