using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentManagement.Migrations
{
    public partial class GroupAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyProgram_Faculties_FacultyId",
                table: "StudyProgram");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyProgram",
                table: "StudyProgram");

            migrationBuilder.RenameTable(
                name: "StudyProgram",
                newName: "StudyPrograms");

            migrationBuilder.RenameIndex(
                name: "IX_StudyProgram_FacultyId",
                table: "StudyPrograms",
                newName: "IX_StudyPrograms_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyPrograms",
                table: "StudyPrograms",
                column: "StudyProgramId");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                }); ;

            migrationBuilder.AddForeignKey(
                name: "FK_StudyPrograms_Faculties_FacultyId",
                table: "StudyPrograms",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyPrograms_Faculties_FacultyId",
                table: "StudyPrograms");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudyPrograms",
                table: "StudyPrograms");

            migrationBuilder.RenameTable(
                name: "StudyPrograms",
                newName: "StudyProgram");

            migrationBuilder.RenameIndex(
                name: "IX_StudyPrograms_FacultyId",
                table: "StudyProgram",
                newName: "IX_StudyProgram_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudyProgram",
                table: "StudyProgram",
                column: "StudyProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyProgram_Faculties_FacultyId",
                table: "StudyProgram",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "FacultyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
