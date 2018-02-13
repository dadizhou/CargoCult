using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CargoCult.Migrations
{
    public partial class LocationUpate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Location",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Location",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Location");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Location",
                newName: "Name");
        }
    }
}
