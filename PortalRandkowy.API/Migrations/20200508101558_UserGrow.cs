using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PortalRandkowy.API.Migrations
{
    public partial class UserGrow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Children",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorEye",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorSkin",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FreeTime",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendsWouldDescribeMe",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Growth",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ILike",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "INotLike",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interest",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItFeelsBestIn",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Langueches",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActive",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LookingFor",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MakesMeLaugh",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MartialStatus",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Motto",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Movies",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Music",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Personality",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sport",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ZodiacSign",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    MainPhoto = table.Column<bool>(nullable: false),
                    Userid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "Userid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_Userid",
                table: "Photos",
                column: "Userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ColorEye",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ColorSkin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FreeTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FriendsWouldDescribeMe",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Growth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ILike",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "INotLike",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Interest",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ItFeelsBestIn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Langueches",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastActive",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LookingFor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MakesMeLaugh",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MartialStatus",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Motto",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Movies",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Music",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Personality",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Sport",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ZodiacSign",
                table: "Users");
        }
    }
}
