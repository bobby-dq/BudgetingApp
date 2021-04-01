using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetingApp.Models.BudgetingModels
{
    public class ExpenseItem
    {
        public long ExpenseItemId {get; set;}


        public string UserId {get; set;}


        [Required(ErrorMessage="Please enter a description for your expense item.")]
        [StringLength(64, ErrorMessage = "The field {0} cannot exceed more than 64 characters.")]
        public string Description {get; set;}


        [Column(TypeName="decimal(12,2)")]
        [Required(ErrorMessage="Please enter the expense item amount")]
        public decimal Amount {get; set;}


        [Required(ErrorMessage="Please enter a transaction date.")]
        [DataType(DataType.Date)]
        [Display(Name="Transaction Date")]
        public DateTime TransactionDate {get; set;}


        public readonly DateTime DateCreated = DateTime.Now;


        // Foreign Relationships
        public long ExpenseCategoryId {get; set;}
        public ExpenseCategory ExpenseCategory {get; set;}
        public long BudgetId {get; set;}
        public Budget Budget {get; set;}
    }
}