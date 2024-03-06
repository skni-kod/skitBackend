using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace skit.Infrastructure.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddImageInCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Companies",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ImageId",
                table: "Companies",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Files_ImageId",
                table: "Companies",
                column: "ImageId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Files_ImageId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ImageId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Companies");
        }
    }
}
