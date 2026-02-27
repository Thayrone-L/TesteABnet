using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndTesteABnet.Migrations
{
    /// <inheritdoc />
    public partial class removeOverdueStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Overdue",
                table: "Assignments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Overdue",
                table: "Assignments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
