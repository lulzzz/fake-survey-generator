using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeSurveyGenerator.API.Migrations
{
    public partial class AddAudit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "AuditSeq",
                schema: "Survey",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Audit",
                schema: "Survey",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    WhatHappened = table.Column<string>(maxLength: 250, nullable: false),
                    When = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit",
                schema: "Survey");

            migrationBuilder.DropSequence(
                name: "AuditSeq",
                schema: "Survey");
        }
    }
}
