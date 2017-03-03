using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HoveyTech.Data.EfCore.Tests.Migrations
{
    public partial class TestGuidObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TestGuidObjectId",
                table: "TestObject",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TestGuidObject",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestGuidObject", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TestObject_TestGuidObjectId",
                table: "TestObject",
                column: "TestGuidObjectId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_TestObject_TestGuidObject_TestGuidObjectId",
            //    table: "TestObject",
            //    column: "TestGuidObjectId",
            //    principalTable: "TestGuidObject",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestObject_TestGuidObject_TestGuidObjectId",
                table: "TestObject");

            migrationBuilder.DropTable(
                name: "TestGuidObject");

            migrationBuilder.DropIndex(
                name: "IX_TestObject_TestGuidObjectId",
                table: "TestObject");

            migrationBuilder.DropColumn(
                name: "TestGuidObjectId",
                table: "TestObject");
        }
    }
}
