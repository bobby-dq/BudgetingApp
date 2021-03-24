using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetingApp.Models.BudgetingModels
{
    public class Budget
    {
        public long BudgetId {get; set;}


        public string UserId {get;set;}


        [Required(ErrorMessage="Please enter a description for your budget.")]
        [StringLength(64, ErrorMessage="The field {0} cannot exceed more than 64 characters.")]
        public string Description{get;set;}


        [Required(ErrorMessage="Please enter a start date.")]
        [DataType(DataType.Date)]
        [Display(Name="Start Date")]
        public DateTime StartDate {get; set;}


        [Required(ErrorMessage="Please enter an end date.")]
        [DataType(DataType.Date)]
        [Display(Name="End Date")]
        public DateTime EndDate {get;set;}



        public readonly DateTime DateCreated = DateTime.Now;

        // Foreign relationships
        public IEnumerable<ExpenseCategory> ExpenseCategories {get; set;}
        public IEnumerable<IncomeCategory> IncomeCategories {get; set;}
    }
}