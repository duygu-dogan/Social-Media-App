using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FollowLikeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_DomainUsers_ForUserId",
                table: "Notifications");

            migrationBuilder.AlterColumn<Guid>(
                name: "ForUserId",
                table: "Notifications",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<bool>(
                name: "IsAlreadyLiked",
                table: "Likes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAlreadyFollowed",
                table: "Follows",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_DomainUsers_ForUserId",
                table: "Notifications",
                column: "ForUserId",
                principalTable: "DomainUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_DomainUsers_ForUserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsAlreadyLiked",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "IsAlreadyFollowed",
                table: "Follows");

            migrationBuilder.AlterColumn<Guid>(
                name: "ForUserId",
                table: "Notifications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_DomainUsers_ForUserId",
                table: "Notifications",
                column: "ForUserId",
                principalTable: "DomainUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
