using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Front.Migrations
{
    /// <inheritdoc />
    public partial class Initial_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ScrappedElements",
                table: "ScrappedElements");

            migrationBuilder.RenameTable(
                name: "ScrappedElements",
                newName: "scrappedElements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_scrappedElements",
                table: "scrappedElements",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_scrappedElements",
                table: "scrappedElements");

            migrationBuilder.RenameTable(
                name: "scrappedElements",
                newName: "ScrappedElements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScrappedElements",
                table: "ScrappedElements",
                column: "Id");
        }
    }
}
