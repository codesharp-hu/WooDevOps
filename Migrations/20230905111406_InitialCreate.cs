using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BashScriptRunner.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HostingEnvironments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApiEndpoint = table.Column<string>(type: "text", nullable: true),
                    SshEndpoint = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostingEnvironments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ProductionId = table.Column<int>(type: "integer", nullable: false),
                    StagingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PipelineDescriptors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipelineDescriptors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipelineDescriptors_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobDescriptors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PipelineDescriptorId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDescriptors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobDescriptors_PipelineDescriptors_PipelineDescriptorId",
                        column: x => x.PipelineDescriptorId,
                        principalTable: "PipelineDescriptors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PipelineStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PipelineDescriptorId = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipelineStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PipelineStates_PipelineDescriptors_PipelineDescriptorId",
                        column: x => x.PipelineDescriptorId,
                        principalTable: "PipelineDescriptors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobParameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobDescriptorId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobParameters_JobDescriptors_JobDescriptorId",
                        column: x => x.JobDescriptorId,
                        principalTable: "JobDescriptors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PipelineStateId = table.Column<int>(type: "integer", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Messages = table.Column<List<string>>(type: "text[]", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobStates_PipelineStates_PipelineStateId",
                        column: x => x.PipelineStateId,
                        principalTable: "PipelineStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "HostingEnvironments",
                columns: new[] { "Id", "ApiEndpoint", "SshEndpoint" },
                values: new object[,]
                {
                    { 1, "https://api.example.com/", "ssh.example.com" },
                    { 2, "https://api.example.com/", "ssh.example.com" },
                    { 3, "https://api.example.com/", "ssh.example.com" },
                    { 4, "https://api.example.com/", "ssh.example.com" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Name", "ProductionId", "StagingId" },
                values: new object[,]
                {
                    { 1, "Sample Project 1", 1, 2 },
                    { 2, "Sample Project 2", 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "PipelineDescriptors",
                columns: new[] { "Id", "Name", "ProjectId" },
                values: new object[,]
                {
                    { 1, "Sample Pipeline 1", 1 },
                    { 2, "Sample Pipeline 2", 2 }
                });

            migrationBuilder.InsertData(
                table: "JobDescriptors",
                columns: new[] { "Id", "Name", "PipelineDescriptorId" },
                values: new object[,]
                {
                    { 1, "Sample Job 1", 1 },
                    { 2, "Sample Job 2", 2 },
                    { 3, "Sample Job 3", 1 }
                });

            migrationBuilder.InsertData(
                table: "PipelineStates",
                columns: new[] { "Id", "Date", "PipelineDescriptorId", "State" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 5, 11, 14, 6, 371, DateTimeKind.Utc).AddTicks(7750), 1, 3 },
                    { 2, new DateTime(2023, 9, 5, 11, 14, 6, 371, DateTimeKind.Utc).AddTicks(7750), 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "JobParameters",
                columns: new[] { "Id", "JobDescriptorId", "Name", "Value" },
                values: new object[,]
                {
                    { 1, 1, "JobParameter", "JobParameterValue" },
                    { 2, 1, "JobParameter2", "JobParameterValue2" },
                    { 3, 2, "JobParameter", "JobParameterValue" },
                    { 4, 2, "JobParameter2", "JobParameterValue2" }
                });

            migrationBuilder.InsertData(
                table: "JobStates",
                columns: new[] { "Id", "Date", "Messages", "PipelineStateId", "State" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 9, 5, 11, 14, 6, 371, DateTimeKind.Utc).AddTicks(7760), new List<string> { "Message 1", "Message 2" }, 1, 3 },
                    { 2, new DateTime(2023, 9, 5, 11, 14, 6, 371, DateTimeKind.Utc).AddTicks(7770), new List<string> { "Message 1", "Message 2" }, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobDescriptors_PipelineDescriptorId",
                table: "JobDescriptors",
                column: "PipelineDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_JobParameters_JobDescriptorId",
                table: "JobParameters",
                column: "JobDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_JobStates_PipelineStateId",
                table: "JobStates",
                column: "PipelineStateId");

            migrationBuilder.CreateIndex(
                name: "IX_PipelineDescriptors_ProjectId",
                table: "PipelineDescriptors",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PipelineStates_PipelineDescriptorId",
                table: "PipelineStates",
                column: "PipelineDescriptorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HostingEnvironments");

            migrationBuilder.DropTable(
                name: "JobParameters");

            migrationBuilder.DropTable(
                name: "JobStates");

            migrationBuilder.DropTable(
                name: "JobDescriptors");

            migrationBuilder.DropTable(
                name: "PipelineStates");

            migrationBuilder.DropTable(
                name: "PipelineDescriptors");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
