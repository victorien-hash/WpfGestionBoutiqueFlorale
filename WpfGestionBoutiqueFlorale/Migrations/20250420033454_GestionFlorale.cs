using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfGestionBoutiqueFlorale.Migrations
{
    /// <inheritdoc />
    public partial class GestionFlorale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    IdUtilisateur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.IdUtilisateur);
                });

            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    IdCommande = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUtilisateur = table.Column<int>(type: "int", nullable: true),
                    UtilisateurIdUtilisateur = table.Column<int>(type: "int", nullable: true),
                    MontantTotal = table.Column<double>(type: "float", nullable: false),
                    EstValidee = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.IdCommande);
                    table.ForeignKey(
                        name: "FK_Commandes_Utilisateurs_UtilisateurIdUtilisateur",
                        column: x => x.UtilisateurIdUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "IdUtilisateur");
                });

            migrationBuilder.CreateTable(
                name: "Bouquets",
                columns: table => new
                {
                    IdBouquet = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomBouquet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartePersonnalise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCommande = table.Column<int>(type: "int", nullable: true),
                    EstSelectionnee = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bouquets", x => x.IdBouquet);
                    table.ForeignKey(
                        name: "FK_Bouquets_Commandes_IdCommande",
                        column: x => x.IdCommande,
                        principalTable: "Commandes",
                        principalColumn: "IdCommande",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    IdFacture = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModePaiement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCommande = table.Column<int>(type: "int", nullable: true),
                    CommandeIdCommande = table.Column<int>(type: "int", nullable: true),
                    IdUtilisateur = table.Column<int>(type: "int", nullable: true),
                    UtilisateurIdUtilisateur = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.IdFacture);
                    table.ForeignKey(
                        name: "FK_Factures_Commandes_CommandeIdCommande",
                        column: x => x.CommandeIdCommande,
                        principalTable: "Commandes",
                        principalColumn: "IdCommande");
                    table.ForeignKey(
                        name: "FK_Factures_Utilisateurs_UtilisateurIdUtilisateur",
                        column: x => x.UtilisateurIdUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "IdUtilisateur");
                });

            migrationBuilder.CreateTable(
                name: "Fleurs",
                columns: table => new
                {
                    IdFleur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrixUnitaire = table.Column<double>(type: "float", nullable: false),
                    CouleurDominante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdBouquet = table.Column<int>(type: "int", nullable: true),
                    BouquetIdBouquet = table.Column<int>(type: "int", nullable: true),
                    IdCommande = table.Column<int>(type: "int", nullable: true),
                    EstSelectionnee = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fleurs", x => x.IdFleur);
                    table.ForeignKey(
                        name: "FK_Fleurs_Bouquets_BouquetIdBouquet",
                        column: x => x.BouquetIdBouquet,
                        principalTable: "Bouquets",
                        principalColumn: "IdBouquet");
                    table.ForeignKey(
                        name: "FK_Fleurs_Commandes_IdCommande",
                        column: x => x.IdCommande,
                        principalTable: "Commandes",
                        principalColumn: "IdCommande");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bouquets_IdCommande",
                table: "Bouquets",
                column: "IdCommande");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_UtilisateurIdUtilisateur",
                table: "Commandes",
                column: "UtilisateurIdUtilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_CommandeIdCommande",
                table: "Factures",
                column: "CommandeIdCommande");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_UtilisateurIdUtilisateur",
                table: "Factures",
                column: "UtilisateurIdUtilisateur");

            migrationBuilder.CreateIndex(
                name: "IX_Fleurs_BouquetIdBouquet",
                table: "Fleurs",
                column: "BouquetIdBouquet");

            migrationBuilder.CreateIndex(
                name: "IX_Fleurs_IdCommande",
                table: "Fleurs",
                column: "IdCommande");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Fleurs");

            migrationBuilder.DropTable(
                name: "Bouquets");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
