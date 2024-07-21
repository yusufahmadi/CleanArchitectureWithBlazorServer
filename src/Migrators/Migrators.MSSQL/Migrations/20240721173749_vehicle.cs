using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Blazor.Migrators.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class vehicle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    JenisKendaraan = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Merk = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Tipe = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    NoChasis = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    NoRangka = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Pabrikasi = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    TahunPembuatan = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    TahunOperasi = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ServiceA = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ServiceB = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ServiceC = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LastPerbaikan = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
