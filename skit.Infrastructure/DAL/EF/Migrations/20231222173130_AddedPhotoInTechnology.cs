using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skit.Infrastructure.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddedPhotoInTechnology : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "Technologies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_PhotoId",
                table: "Technologies",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Technologies_Files_PhotoId",
                table: "Technologies",
                column: "PhotoId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Technologies_Files_PhotoId",
                table: "Technologies");

            migrationBuilder.DropIndex(
                name: "IX_Technologies_PhotoId",
                table: "Technologies");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Technologies");
        }
    }
}
