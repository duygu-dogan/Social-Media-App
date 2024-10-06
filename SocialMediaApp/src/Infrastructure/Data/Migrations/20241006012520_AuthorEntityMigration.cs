using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AuthorEntityMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_DomainUsers_UserId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_DomainUsers_CreatedById",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_DomainUsers_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_RePosts_DomainUsers_UserId",
                table: "RePosts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "RePosts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Posts",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Likes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RePosts_CreatedById",
                table: "RePosts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts",
                column: "CreatedById");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_DomainUsers_CreatedById",
                table: "Notifications",
                column: "CreatedById",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_DomainUsers_CreatedById",
                table: "Posts",
                column: "CreatedById",
                principalTable: "DomainUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RePosts_DomainUsers_CreatedById",
                table: "RePosts",
                column: "CreatedById",
                principalTable: "DomainUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_DomainUsers_CreatedById",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_DomainUsers_CreatedById",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_DomainUsers_CreatedById",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_RePosts_DomainUsers_CreatedById",
                table: "RePosts");

            migrationBuilder.DropIndex(
                name: "IX_RePosts_CreatedById",
                table: "RePosts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CreatedById",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Likes_CreatedById",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "RePosts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Likes");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_DomainUsers_UserId",
                table: "Likes",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_DomainUsers_CreatedById",
                table: "Notifications",
                column: "CreatedById",
                principalTable: "DomainUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_DomainUsers_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RePosts_DomainUsers_UserId",
                table: "RePosts",
                column: "UserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
