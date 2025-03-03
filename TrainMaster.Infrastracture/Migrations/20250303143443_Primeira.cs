using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TrainMaster.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class Primeira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cpf = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PessoalProfileEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    Marital = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoalProfileEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoalProfileEntity_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalProfileEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobTitle = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    YearsOfExperience = table.Column<int>(type: "integer", nullable: true),
                    Skills = table.Column<string>(type: "text", nullable: true),
                    Certifications = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalProfileEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfessionalProfileEntity_UserEntity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PostalCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Street = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Neighborhood = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Uf = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PessoalProfileId = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressEntity_PessoalProfileEntity_PessoalProfileId",
                        column: x => x.PessoalProfileId,
                        principalTable: "PessoalProfileEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationLevelEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Institution = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndeedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProfessionalProfileId = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevelEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationLevelEntity_ProfessionalProfileEntity_Professional~",
                        column: x => x.ProfessionalProfileId,
                        principalTable: "ProfessionalProfileEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressEntity_PessoalProfileId",
                table: "AddressEntity",
                column: "PessoalProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationLevelEntity_ProfessionalProfileId",
                table: "EducationLevelEntity",
                column: "ProfessionalProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PessoalProfileEntity_UserId",
                table: "PessoalProfileEntity",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalProfileEntity_UserId",
                table: "ProfessionalProfileEntity",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressEntity");

            migrationBuilder.DropTable(
                name: "EducationLevelEntity");

            migrationBuilder.DropTable(
                name: "PessoalProfileEntity");

            migrationBuilder.DropTable(
                name: "ProfessionalProfileEntity");

            migrationBuilder.DropTable(
                name: "UserEntity");
        }
    }
}
