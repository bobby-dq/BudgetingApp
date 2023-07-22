using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BudgetingApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    BudgetId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.BudgetId);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseCategories",
                columns: table => new
                {
                    ExpenseCategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    BudgetedAmount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    ExpectedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BudgetId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategories", x => x.ExpenseCategoryId);
                    table.ForeignKey(
                        name: "FK_ExpenseCategories_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomeCategories",
                columns: table => new
                {
                    IncomeCategoryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    BudgetedAmount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    ExpectedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    BudgetId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeCategories", x => x.IncomeCategoryId);
                    table.ForeignKey(
                        name: "FK_IncomeCategories_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseItems",
                columns: table => new
                {
                    ExpenseItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpenseCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    BudgetId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseItems", x => x.ExpenseItemId);
                    table.ForeignKey(
                        name: "FK_ExpenseItems_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId");
                    table.ForeignKey(
                        name: "FK_ExpenseItems_ExpenseCategories_ExpenseCategoryId",
                        column: x => x.ExpenseCategoryId,
                        principalTable: "ExpenseCategories",
                        principalColumn: "ExpenseCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomeItems",
                columns: table => new
                {
                    IncomeItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(12,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IncomeCategoryId = table.Column<long>(type: "bigint", nullable: false),
                    BudgetId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeItems", x => x.IncomeItemId);
                    table.ForeignKey(
                        name: "FK_IncomeItems_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Budgets",
                        principalColumn: "BudgetId");
                    table.ForeignKey(
                        name: "FK_IncomeItems_IncomeCategories_IncomeCategoryId",
                        column: x => x.IncomeCategoryId,
                        principalTable: "IncomeCategories",
                        principalColumn: "IncomeCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseCategories_BudgetId",
                table: "ExpenseCategories",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseItems_BudgetId",
                table: "ExpenseItems",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseItems_ExpenseCategoryId",
                table: "ExpenseItems",
                column: "ExpenseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeCategories_BudgetId",
                table: "IncomeCategories",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeItems_BudgetId",
                table: "IncomeItems",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeItems_IncomeCategoryId",
                table: "IncomeItems",
                column: "IncomeCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseItems");

            migrationBuilder.DropTable(
                name: "IncomeItems");

            migrationBuilder.DropTable(
                name: "ExpenseCategories");

            migrationBuilder.DropTable(
                name: "IncomeCategories");

            migrationBuilder.DropTable(
                name: "Budgets");
        }
    }
}
