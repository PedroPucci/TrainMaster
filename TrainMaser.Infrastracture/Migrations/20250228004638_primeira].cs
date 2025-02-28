using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainMaser.Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class primeira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressType",
                table: "AddressEntity");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AddressEntity");

            migrationBuilder.DropColumn(
                name: "Complement",
                table: "AddressEntity");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "AddressEntity");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AddressEntity");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "AddressEntity");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "AddressEntity",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                table: "AddressEntity",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Localidade",
                table: "AddressEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "AddressEntity",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Uf",
                table: "AddressEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "AddressEntity");

            migrationBuilder.DropColumn(
                name: "Cep",
                table: "AddressEntity");

            migrationBuilder.DropColumn(
                name: "Localidade",
                table: "AddressEntity");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "AddressEntity");

            migrationBuilder.DropColumn(
                name: "Uf",
                table: "AddressEntity");

            migrationBuilder.AddColumn<string>(
                name: "AddressType",
                table: "AddressEntity",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AddressEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complement",
                table: "AddressEntity",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "AddressEntity",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AddressEntity",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "AddressEntity",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
