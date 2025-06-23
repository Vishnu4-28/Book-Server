using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_commerce.Server.Migrations
{
    /// <inheritdoc />
    public partial class setFkinimgTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookImgs_Book_id",
                table: "BookImgs",
                column: "Book_id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookImgs_Books_Book_id",
                table: "BookImgs",
                column: "Book_id",
                principalTable: "Books",
                principalColumn: "Book_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookImgs_Books_Book_id",
                table: "BookImgs");

            migrationBuilder.DropIndex(
                name: "IX_BookImgs_Book_id",
                table: "BookImgs");
        }
    }
}
