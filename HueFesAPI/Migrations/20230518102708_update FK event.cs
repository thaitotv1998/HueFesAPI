﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HueFesAPI.Migrations
{
    public partial class updateFKevent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventTypeId",
                table: "Event",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Event_EventTypeId",
                table: "Event",
                column: "EventTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_EventType_EventTypeId",
                table: "Event",
                column: "EventTypeId",
                principalTable: "EventType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_EventType_EventTypeId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_EventTypeId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "EventTypeId",
                table: "Event");
        }
    }
}
