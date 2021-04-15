using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using BudgetingApp.Models.BudgetingModels;

namespace BudgetingApp.Models.RepositoryModels
{
    public static class BudgetingSeedData
    {
        public static void SeedBudgetingDatabase(BudgetingContext context, IServiceProvider serviceProvider)
        {
            serviceProvider = serviceProvider.CreateScope().ServiceProvider;
            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            SeedBudgetingDatabaseAsync(context, userManager).Wait();
        }
        public static async Task SeedBudgetingDatabaseAsync (BudgetingContext context, UserManager<IdentityUser> userManager) 
        {
            IdentityUser user = await userManager.FindByNameAsync("BobbyDq");
            string userId = user.Id;

            context.Database.Migrate();
            if (context.Budgets.Count() == 0 && context.ExpenseCategories.Count() == 0 &&
                context.ExpenseItems.Count() == 0 && context.IncomeCategories.Count() == 0 &&
                context.IncomeItems.Count() == 0)
            {
                //-------------------------------------------------------
                // Create Budgets
                Budget budgetA = new Budget 
                {
                    UserId = userId,
                    Description = "Budget A",
                    StartDate = new DateTime(2021, 01, 01),
                    EndDate = new DateTime(2021, 01, 31)
                };
                Budget budgetB = new Budget 
                {
                    UserId = userId,
                    Description = "Budget B",
                    StartDate = new DateTime(2021, 03, 01),
                    EndDate = new DateTime(2021, 03, 31)
                };
                //------------------------------------------------------
                // Create Expense Categories
                ExpenseCategory expenseCategoryAA = new ExpenseCategory 
                {
                    UserId = userId,
                    Description = "Expense Category AA",
                    BudgetedAmount = 100, 
                    ExpectedDate = new DateTime(2021, 01, 30),
                    Budget = budgetA
                };
                ExpenseCategory expenseCategoryAB = new ExpenseCategory 
                {
                    UserId = userId,
                    Description = "Expense Category AB",
                    BudgetedAmount = 200, 
                    ExpectedDate = new DateTime(2021, 04, 30),
                    Budget = budgetA
                };
                ExpenseCategory expenseCategoryBA = new ExpenseCategory 
                {
                    UserId = userId,
                    Description = "Expense Category BA",
                    BudgetedAmount = 222, 
                    ExpectedDate = new DateTime(2021, 01, 30),
                    Budget = budgetB
                };
                ExpenseCategory expenseCategoryBB = new ExpenseCategory 
                {
                    UserId = userId,
                    Description = "Expense Category BB",
                    BudgetedAmount = 333, 
                    ExpectedDate = new DateTime(2021, 04, 30),
                    Budget = budgetB
                };
                //-------------------------------------------------------
                // Create Income Categories
                IncomeCategory incomeCategoryAA = new IncomeCategory
                {
                    UserId = userId,
                    Description = "Income Category AA",
                    BudgetedAmount = 1000, 
                    ExpectedDate = new DateTime(2021, 01, 30),
                    Budget = budgetA
                };
                IncomeCategory incomeCategoryAB = new IncomeCategory
                {
                    UserId = userId,
                    Description = "Income Category AB",
                    BudgetedAmount = 2000, 
                    ExpectedDate = new DateTime(2021, 04, 30),
                    Budget = budgetA
                };
                IncomeCategory incomeCategoryBA = new IncomeCategory
                {
                    UserId = userId,
                    Description = "Income Category BA",
                    BudgetedAmount = 2222, 
                    ExpectedDate = new DateTime(2021, 01, 30),
                    Budget = budgetB
                };
                IncomeCategory incomeCategoryBB = new IncomeCategory
                {
                    UserId = userId,
                    Description = "Income Category BB",
                    BudgetedAmount = 3333, 
                    ExpectedDate = new DateTime(2021, 04, 30),
                    Budget = budgetB
                };
                //------------------------------------------------------------
                // Create Expense Items
                context.ExpenseItems.AddRange(
                    new ExpenseItem {
                        UserId = userId,
                        Description = "Expense Item AAA",
                        Amount = 12.89m,
                        TransactionDate = new DateTime(2021, 05, 09),
                        ExpenseCategory = expenseCategoryAA,
                        Budget = budgetA
                    },
                    new ExpenseItem {
                        UserId = userId,
                        Description = "Expense Item AAB",
                        Amount = 16.75m,
                        TransactionDate = new DateTime(2021, 05, 10),
                        ExpenseCategory = expenseCategoryAA,
                        Budget = budgetA
                    },
                    new ExpenseItem {
                        UserId = userId,
                        Description = "Expense Item ABA",
                        Amount = 500.76m,
                        TransactionDate = new DateTime(2021, 05, 11),
                        ExpenseCategory = expenseCategoryAB,
                        Budget = budgetA
                    },
                    new ExpenseItem {
                        UserId = userId,
                        Description = "Expense Item ABB",
                        Amount = 122.32m,
                        TransactionDate = new DateTime(202, 05, 12),
                        ExpenseCategory = expenseCategoryAB,
                        Budget = budgetA
                    },
                    new ExpenseItem {
                        UserId = userId,
                        Description = "Expense Item BAA",
                        Amount = 12.89m,
                        TransactionDate = new DateTime(2021, 05, 09),
                        ExpenseCategory = expenseCategoryBA,
                        Budget = budgetB
                    },
                    new ExpenseItem {
                        UserId = userId,
                        Description = "Expense Item BAB",
                        Amount = 26.75m,
                        TransactionDate = new DateTime(2021, 05, 16),
                        ExpenseCategory = expenseCategoryBA,
                        Budget = budgetB
                    },
                    new ExpenseItem {
                        UserId = userId,
                        Description = "Expense Item BBA",
                        Amount = 300.76m,
                        TransactionDate = new DateTime(2021, 05, 17),
                        ExpenseCategory = expenseCategoryBB,
                        Budget = budgetB
                    },
                    new ExpenseItem {
                        UserId = userId,
                        Description = "Expense Item BBB",
                        Amount = 422.32m,
                        TransactionDate = new DateTime(2021, 05, 18),
                        ExpenseCategory = expenseCategoryBB,
                        Budget = budgetB
                    }
                );
                //--------------------------------------------------------------
                // Create Income Items
                context.IncomeItems.AddRange(
                    new IncomeItem {
                        UserId = userId,
                        Description = "Income Item AAA",
                        Amount = 140.89m,
                        TransactionDate = new DateTime(2021, 05, 09),
                        IncomeCategory = incomeCategoryAA,
                        Budget = budgetA
                    },
                    new IncomeItem {
                        UserId = userId,
                        Description = "Income Item AAB",
                        Amount = 160.75m,
                        TransactionDate = new DateTime(2021, 06, 10),
                        IncomeCategory = incomeCategoryAA,
                        Budget = budgetA
                    },
                    new IncomeItem {
                        UserId = userId,
                        Description = "Income Item ABA",
                        Amount = 5001.76m,
                        TransactionDate = new DateTime(2021, 07, 11),
                        IncomeCategory = incomeCategoryAB,
                        Budget = budgetA
                    },
                    new IncomeItem {
                        UserId = userId,
                        Description = "Income Item ABB",
                        Amount = 1224.32m,
                        TransactionDate = new DateTime(202, 08, 12),
                        IncomeCategory = incomeCategoryAB,
                        Budget = budgetA
                    },
                    new IncomeItem {
                        UserId = userId,
                        Description = "Income Item BAA",
                        Amount = 129.89m,
                        TransactionDate = new DateTime(2021, 09, 09),
                        IncomeCategory = incomeCategoryBA,
                        Budget = budgetB
                    },
                    new IncomeItem {
                        Description = "Income Item BAB",
                        Amount = 6.75m,
                        TransactionDate = new DateTime(2021, 10, 16),
                        IncomeCategory = incomeCategoryBA,
                        Budget = budgetB
                    },
                    new IncomeItem {
                        UserId = userId,
                        Description = "Income Item BBA",
                        Amount = 00.76m,
                        TransactionDate = new DateTime(2021, 11, 17),
                        IncomeCategory = incomeCategoryBB,
                        Budget = budgetB
                    },
                    new IncomeItem {
                        UserId = userId,
                        Description = "Income Item BBB",
                        Amount = 3224.32m,
                        TransactionDate = new DateTime(2021, 12, 18),
                        IncomeCategory = incomeCategoryBB,
                        Budget = budgetB
                    }
                );
                
                context.SaveChanges();
            }
        }
    }
}