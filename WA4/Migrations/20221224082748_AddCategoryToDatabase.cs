using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WA4.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryToDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComponentNumber = table.Column<double>(type: "float", nullable: false),
                    ComponentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    ChangeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories1", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories1");
        }
    }
}
