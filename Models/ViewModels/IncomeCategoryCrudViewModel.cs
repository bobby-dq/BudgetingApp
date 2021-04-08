using BudgetingApp.Models.BudgetingModels;

namespace BudgetingApp.Models.ViewModels
{
    public class IncomeCategoryCrudViewModel
    {
        public Budget Budget {get; set;}
        public IncomeCategory IncomeCategory {get; set;}
        public string Action {get; set;}
        public bool ReadOnly {get; set;}
        public string ObjectTheme {get; set;}
        public bool ShowAction {get; set;}
        public string ActionTheme {get;set;}
        public string ButtonTheme {get;set;}

    }
}