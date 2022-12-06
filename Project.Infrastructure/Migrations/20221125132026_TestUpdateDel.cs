using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    public partial class TestUpdateDel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.CreateTable(
                name: "AuthorResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommitCount = table.Column<int>(type: "int", nullable: false),
                    CommitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepositoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorResults", x => x.Id);
                });*/

            migrationBuilder.CreateTable(
                name: "ForkResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForkName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RepositoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForkResults", x => x.Id);
                });

           /* migrationBuilder.CreateTable(
                name: "FrequencyResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommitCount = table.Column<int>(type: "int", nullable: false),
                    CommitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RepositoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrequencyResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepositoryName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LatestCommit = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorResults_Id",
                table: "AuthorResults",
                column: "Id",
                unique: true); */

            migrationBuilder.CreateIndex(
                name: "IX_ForkResults_Id",
                table: "ForkResults",
                column: "Id",
                unique: true);

            /*migrationBuilder.CreateIndex(
                name: "IX_FrequencyResults_Id",
                table: "FrequencyResults",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_Id",
                table: "Repositories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_RepositoryName",
                table: "Repositories",
                column: "RepositoryName",
                unique: true);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropTable(
                name: "AuthorResults");*/

            migrationBuilder.DropTable(
                name: "ForkResults");

            /*migrationBuilder.DropTable(
                name: "FrequencyResults");

            migrationBuilder.DropTable(
                name: "Repositories");*/
        }
    }
}
