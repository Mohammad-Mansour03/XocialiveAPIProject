using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XocialiveProject.Migrations
{
    /// <inheritdoc />
    public partial class AddIndividualAndCoporate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "Particpants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Particpants",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsIntern",
                table: "Particpants",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobTitle",
                table: "Particpants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "Particpants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "YearOfGraduation",
                table: "Particpants",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Company",
                table: "Particpants");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Particpants");

            migrationBuilder.DropColumn(
                name: "IsIntern",
                table: "Particpants");

            migrationBuilder.DropColumn(
                name: "JobTitle",
                table: "Particpants");

            migrationBuilder.DropColumn(
                name: "University",
                table: "Particpants");

            migrationBuilder.DropColumn(
                name: "YearOfGraduation",
                table: "Particpants");
        }
    }
}
