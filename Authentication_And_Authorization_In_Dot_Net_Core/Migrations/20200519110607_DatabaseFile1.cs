using Microsoft.EntityFrameworkCore.Migrations;

namespace Authentication_And_Authorization_In_Dot_Net_Core.Migrations
{
    public partial class DatabaseFile1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "boss",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(type: "varchar(30)", nullable: true),
                    Address = table.Column<string>(type: "varchar(30)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(30)", nullable: true),
                    Password = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boss", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(type: "varchar(30)", nullable: true),
                    Address = table.Column<string>(type: "varchar(30)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(30)", nullable: true),
                    Password = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "boss");

            migrationBuilder.DropTable(
                name: "employee");
        }
    }
}
