using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class AddUniqeIndexesToLookupTabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_email_lists_email",
                table: "email_lists",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_email_lists_email",
                table: "email_lists");
        }
    }
}
