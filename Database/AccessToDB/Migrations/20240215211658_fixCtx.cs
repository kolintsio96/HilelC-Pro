using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessToDB.Migrations
{
    /// <inheritdoc />
    public partial class fixCtx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Books_BookId",
                table: "History");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_History",
                table: "History");

            migrationBuilder.RenameTable(
                name: "Reader",
                newName: "Readers");

            migrationBuilder.RenameTable(
                name: "History",
                newName: "Histories");

            migrationBuilder.RenameIndex(
                name: "IX_Reader_LibrarianId",
                table: "Readers",
                newName: "IX_Readers_LibrarianId");

            migrationBuilder.RenameIndex(
                name: "IX_Reader_DocumentTypeId",
                table: "Readers",
                newName: "IX_Readers_DocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_History_ReaderId",
                table: "Histories",
                newName: "IX_Histories_ReaderId");

            migrationBuilder.RenameIndex(
                name: "IX_History_BookId",
                table: "Histories",
                newName: "IX_Histories_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Readers",
                table: "Readers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Histories",
                table: "Histories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Books_BookId",
                table: "Histories",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Histories_Readers_ReaderId",
                table: "Histories",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Books_BookId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Histories_Readers_ReaderId",
                table: "Histories");

            migrationBuilder.DropForeignKey(
                name: "FK_Readers_Documents_DocumentTypeId",
                table: "Readers");

            migrationBuilder.DropForeignKey(
                name: "FK_Readers_Librarians_LibrarianId",
                table: "Readers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Readers",
                table: "Readers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Histories",
                table: "Histories");

            migrationBuilder.RenameTable(
                name: "Readers",
                newName: "Reader");

            migrationBuilder.RenameTable(
                name: "Histories",
                newName: "History");

            migrationBuilder.RenameIndex(
                name: "IX_Readers_LibrarianId",
                table: "Reader",
                newName: "IX_Reader_LibrarianId");

            migrationBuilder.RenameIndex(
                name: "IX_Readers_DocumentTypeId",
                table: "Reader",
                newName: "IX_Reader_DocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Histories_ReaderId",
                table: "History",
                newName: "IX_History_ReaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Histories_BookId",
                table: "History",
                newName: "IX_History_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reader",
                table: "Reader",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_History",
                table: "History",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Books_BookId",
                table: "History",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

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
    }
}
