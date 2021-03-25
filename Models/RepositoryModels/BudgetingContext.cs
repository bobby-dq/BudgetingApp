using Microsoft.EntityFrameworkCore;
using BudgetingApp.Models.BudgetingModels;

namespace BudgetingApp.Models.RepositoryModels
{
    public class BudgetingContext: DbContext 
    {
        public BudgetingContext(DbContextOptions<BudgetingContext> opts): base (opts) {}

        public DbSet<Budget> Budgets {get; set;}
        public DbSet<ExpenseCategory> ExpenseCategories {get; set;}
        public DbSet<ExpenseItem> ExpenseItems {get; set;}
        public DbSet<IncomeCategory> IncomeCategories {get; set;}
        public DbSet<IncomeItem> IncomeItems {get; set;}
    }
}