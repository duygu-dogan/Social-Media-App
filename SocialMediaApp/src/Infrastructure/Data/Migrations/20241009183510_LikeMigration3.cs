using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class LikeMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_DomainUsers_CreatedById",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_CreatedById",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Likes");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_DomainUsers_LikerId",
                table: "Likes",
                column: "LikerId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_DomainUsers_LikerId",
                table: "Likes");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Likes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_CreatedById",
                table: "Likes",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_DomainUsers_CreatedById",
                table: "Likes",
                column: "CreatedById",
                principalTable: "DomainUsers",
                principalColumn: "Id");
        }
    }
}
