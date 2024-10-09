using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FollowLikeMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAlreadyLiked",
                table: "Likes",
                newName: "IsLiked");

            migrationBuilder.RenameColumn(
                name: "IsAlreadyFollowed",
                table: "Follows",
                newName: "IsFollowed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsLiked",
                table: "Likes",
                newName: "IsAlreadyLiked");

            migrationBuilder.RenameColumn(
                name: "IsFollowed",
                table: "Follows",
                newName: "IsAlreadyFollowed");
        }
    }
}
