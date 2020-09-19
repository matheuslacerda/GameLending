using Microsoft.EntityFrameworkCore.Migrations;
using System;
using Volo.Abp;

namespace GameLending.Migrations
{
    public partial class AddAmigoJogo : Migration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Check.NotNull")]
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            Check.NotNull(migrationBuilder, nameof(migrationBuilder));

            migrationBuilder.CreateTable(
                name: "GlAmigo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlAmigo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GlJogo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Nome = table.Column<string>(maxLength: 200, nullable: false),
                    AmigoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlJogo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlJogo_GlAmigo_AmigoId",
                        column: x => x.AmigoId,
                        principalTable: "GlAmigo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlJogo_AmigoId",
                table: "GlJogo",
                column: "AmigoId");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Check.NotNull")]
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            Check.NotNull(migrationBuilder, nameof(migrationBuilder));

            migrationBuilder.DropTable(
                name: "GlJogo");

            migrationBuilder.DropTable(
                name: "GlAmigo");
        }
    }
}
