using Microsoft.EntityFrameworkCore.Migrations;

namespace PretzelFrontToBack.Migrations
{
    public partial class addDeliciousCardMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deliciousCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardImage = table.Column<string>(nullable: true),
                    CardTitle = table.Column<string>(nullable: true),
                    CardDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliciousCards", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deliciousCards");
        }
    }
}
