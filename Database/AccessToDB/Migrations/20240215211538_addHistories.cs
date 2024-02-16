using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessToDB.Migrations
{
    /// <inheritdoc />
    public partial class addHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Readers_ReaderId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_Readers_Documents_DocumentTypeId",
                table: "Readers");

            migrationBuilder.DropForeignKey(
                name: "FK_Readers_Librarians_LibrarianId",
                table: "Readers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Readers",
                table: "Readers");

            migrationBuilder.RenameTable(
                name: "Readers",
                newName: "Reader");

            migrationBuilder.RenameIndex(
                name: "IX_Readers_LibrarianId",
                table: "Reader",
                newName: "IX_Reader_LibrarianId");

            migrationBuilder.RenameIndex(
                name: "IX_Readers_DocumentTypeId",
                table: "Reader",
                newName: "IX_Reader_DocumentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reader",
                table: "Reader",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Reader_ReaderId",
                table: "History",
                column: "ReaderId",
                principalTable: "Reader",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reader_Documents_DocumentTypeId",
                table: "Reader",
                column: "DocumentTypeId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reader_Librarians_LibrarianId",
                table: "Reader",
                column: "LibrarianId",
                principalTable: "Librarians",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Reader_ReaderId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_Reader_Documents_DocumentTypeId",
                table: "Reader");

            migrationBuilder.DropForeignKey(
                name: "FK_Reader_Librarians_LibrarianId",
                table: "Reader");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reader",
                table: "Reader");

            migrationBuilder.RenameTable(
                name: "Reader",
                newName: "Readers");

            migrationBuilder.RenameIndex(
                name: "IX_Reader_LibrarianId",
                table: "Readers",
                newName: "IX_Readers_LibrarianId");

            migrationBuilder.RenameIndex(
                name: "IX_Reader_DocumentTypeId",
                table: "Readers",
                newName: "IX_Readers_DocumentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Readers",
                table: "Readers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Readers_ReaderId",
                table: "History",
                column: "ReaderId",
                principalTable: "Readers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Readers_Documents_DocumentTypeId",
                table: "Readers",
                column: "DocumentTypeId",
                principalTable: "Documents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Readers_Librarians_LibrarianId",
                table: "Readers",
                column: "LibrarianId",
                principalTable: "Librarians",
                principalColumn: "Id");
        }
    }
}
