using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class LikeMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_DomainUsers_CreatedById",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Likes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Likes",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "LikerId",
                table: "Likes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "LikerId", "PostId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_DomainUsers_CreatedById",
                table: "Likes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_CreatedById",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "LikerId",
                table: "Likes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedById",
                table: "Likes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "Likes",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                columns: new[] { "CreatedById", "PostId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_DomainUsers_CreatedById",
                table: "Likes",
                column: "CreatedById",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
