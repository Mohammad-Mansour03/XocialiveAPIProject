using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XocialiveProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTheParticipantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Coporates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Company = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    JobTitle = table.Column<string>(type: "VARCHAR(90)", maxLength: 90, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coporates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coporates_Particpants_Id",
                        column: x => x.Id,
                        principalTable: "Particpants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    University = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    YearOfGraduation = table.Column<int>(type: "int", nullable: false),
                    IsIntern = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Individuals_Particpants_Id",
                        column: x => x.Id,
                        principalTable: "Particpants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coporates");

            migrationBuilder.DropTable(
                name: "Individuals");

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
    }
}
