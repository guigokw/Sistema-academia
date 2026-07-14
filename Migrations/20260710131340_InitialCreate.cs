using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sistema_academia.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    CpfAluno = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NomeAluno = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TelefoneAluno = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailAluno = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataNascimento = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Ativo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.CpfAluno);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Exercicios",
                columns: table => new
                {
                    IdExercicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomeExercicio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Series = table.Column<int>(type: "int", nullable: false),
                    Repeticoes = table.Column<int>(type: "int", nullable: false),
                    Descanso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercicios", x => x.IdExercicio);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Planos",
                columns: table => new
                {
                    IdPlano = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomePlano = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoPlano = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ValorPlano = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.IdPlano);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Treinos",
                columns: table => new
                {
                    idTreino = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Objetivo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    duracaoTreino = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treinos", x => x.idTreino);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    idMatricula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CpfAluno = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdPlano = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    SituacaoPagamento = table.Column<string>(type: "varchar(30)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataVencimento = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.idMatricula);
                    table.ForeignKey(
                        name: "FK_Matriculas_Alunos_CpfAluno",
                        column: x => x.CpfAluno,
                        principalTable: "Alunos",
                        principalColumn: "CpfAluno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_Planos_IdPlano",
                        column: x => x.IdPlano,
                        principalTable: "Planos",
                        principalColumn: "IdPlano",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TreinoAluno",
                columns: table => new
                {
                    IdTreino = table.Column<int>(type: "int", nullable: false),
                    CpfAluno = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreinoAluno", x => new { x.CpfAluno, x.IdTreino });
                    table.ForeignKey(
                        name: "FK_TreinoAluno_Alunos_CpfAluno",
                        column: x => x.CpfAluno,
                        principalTable: "Alunos",
                        principalColumn: "CpfAluno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreinoAluno_Treinos_IdTreino",
                        column: x => x.IdTreino,
                        principalTable: "Treinos",
                        principalColumn: "idTreino",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TreinoExercicio",
                columns: table => new
                {
                    IdTreino = table.Column<int>(type: "int", nullable: false),
                    IdExercicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreinoExercicio", x => new { x.IdTreino, x.IdExercicio });
                    table.ForeignKey(
                        name: "FK_TreinoExercicio_Exercicios_IdExercicio",
                        column: x => x.IdExercicio,
                        principalTable: "Exercicios",
                        principalColumn: "IdExercicio",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TreinoExercicio_Treinos_IdTreino",
                        column: x => x.IdTreino,
                        principalTable: "Treinos",
                        principalColumn: "idTreino",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    IdPagamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ValorPagamento = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataPagamento = table.Column<DateOnly>(type: "date", nullable: true),
                    MesReferencia = table.Column<int>(type: "int", nullable: false),
                    AnoReferencia = table.Column<int>(type: "int", nullable: false),
                    IdMatricula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.IdPagamento);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Matriculas_IdMatricula",
                        column: x => x.IdMatricula,
                        principalTable: "Matriculas",
                        principalColumn: "idMatricula",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_EmailAluno",
                table: "Alunos",
                column: "EmailAluno",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_CpfAluno",
                table: "Matriculas",
                column: "CpfAluno",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_IdPlano",
                table: "Matriculas",
                column: "IdPlano");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_IdMatricula",
                table: "Pagamentos",
                column: "IdMatricula");

            migrationBuilder.CreateIndex(
                name: "IX_TreinoAluno_IdTreino",
                table: "TreinoAluno",
                column: "IdTreino");

            migrationBuilder.CreateIndex(
                name: "IX_TreinoExercicio_IdExercicio",
                table: "TreinoExercicio",
                column: "IdExercicio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "TreinoAluno");

            migrationBuilder.DropTable(
                name: "TreinoExercicio");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "Exercicios");

            migrationBuilder.DropTable(
                name: "Treinos");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Planos");
        }
    }
}
