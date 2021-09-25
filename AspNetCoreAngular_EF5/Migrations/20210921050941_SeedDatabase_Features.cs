﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreAngular_EF5.Migrations
{
    public partial class SeedDatabase_Features : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature1')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature2')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature3')");
            migrationBuilder.Sql("INSERT INTO Features (Name) VALUES ('Feature4')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Features");
        }
    }
}
