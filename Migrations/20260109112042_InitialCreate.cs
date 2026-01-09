using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace examen_csharp_sur_table.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnneesScolaires",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Libelle = table.Column<string>(type: "TEXT", nullable: false),
                    Statut = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnneesScolaires", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Libelle = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Etudiants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Matricule = table.Column<string>(type: "TEXT", nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Montant = table.Column<decimal>(type: "TEXT", nullable: false),
                    EtudiantId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClasseId = table.Column<int>(type: "INTEGER", nullable: false),
                    AnneeScolaireId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscriptions_AnneesScolaires_AnneeScolaireId",
                        column: x => x.AnneeScolaireId,
                        principalTable: "AnneesScolaires",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscriptions_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscriptions_Etudiants_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "Etudiants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AnneesScolaires",
                columns: new[] { "Id", "Code", "Libelle", "Statut" },
                values: new object[,]
                {
                    { 1, "2023-2024", "Année Scolaire 2023-2024", 1 },
                    { 2, "2024-2025", "Année Scolaire 2024-2025", 0 }
                });

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Code", "Libelle" },
                values: new object[,]
                {
                    { 1, "L1-INFO", "Licence 1 Informatique" },
                    { 2, "L2-INFO", "Licence 2 Informatique" },
                    { 3, "M1-MIAGE", "Master 1 MIAGE" }
                });

            migrationBuilder.InsertData(
                table: "Etudiants",
                columns: new[] { "Id", "Matricule", "Nom", "Prenom" },
                values: new object[,]
                {
                    { 1, "STD001", "Diallo", "Moussa" },
                    { 2, "STD002", "Ndiaye", "Fatou" },
                    { 3, "STD003", "Sow", "Amadou" },
                    { 4, "STD004", "Ba", "Aïssatou" },
                    { 5, "STD005", "Traore", "Ibrahima" }
                });

            migrationBuilder.InsertData(
                table: "Inscriptions",
                columns: new[] { "Id", "AnneeScolaireId", "ClasseId", "Date", "EtudiantId", "Montant" },
                values: new object[,]
                {
                    { 1, 2, 1, new DateTime(2024, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 50000.00m },
                    { 2, 2, 1, new DateTime(2024, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 50000.00m },
                    { 3, 2, 2, new DateTime(2024, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 55000.00m },
                    { 4, 2, 3, new DateTime(2024, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 60000.00m },
                    { 5, 1, 1, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 50000.00m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_AnneeScolaireId",
                table: "Inscriptions",
                column: "AnneeScolaireId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_ClasseId",
                table: "Inscriptions",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscriptions_EtudiantId",
                table: "Inscriptions",
                column: "EtudiantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscriptions");

            migrationBuilder.DropTable(
                name: "AnneesScolaires");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Etudiants");
        }
    }
}
