using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BudgetingApp.Models.BudgetingModels
{
    public class ExpenseCategory
    {
        public long ExpenseCategoryId {get; set;}


        public string UserId {get; set;}


        [Required(ErrorMessage="Please enter a description for your expense category.")]
        [StringLength(64, ErrorMessage="The field {0} cannot exceed more than 64 characters.")]
        public string Description {get; set;}
        
        
        [Column(TypeName="decimal(12,2)")]
        [Display(Name="Budgeted Amount", Prompt="Enter 0 if not known.")]
        [Required(ErrorMessage="Please enter a budgeted amount. Enter 0 if not known.")]
        public decimal BudgetedAmount {get; set;}


        [Display(Name="Expected Date", Prompt="Leave blank if not known.")]
        public DateTime? ExpectedDate {get; set;}


        public readonly DateTime DateCreated = DateTime.Now;


        // Foreign Relationships
        public long BudgetId {get; set;}
        public Budget Budget {get; set;}
        public IEnumerable<ExpenseItem> ExpenseItems {get;set;}
    }
}