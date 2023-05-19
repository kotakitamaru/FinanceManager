using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_Manager.Data
{
    public enum IncomeType
    {
        Salary,
        Pension,
        DepositPercent,
        Other
    }

    public enum SpendType
    {
        Food,
        EatingOut,
        Clothes,
        Technique,
        PublicTransport,
        Fuel,
        Insurance,
        Entertainment,
        Books,
        Rent,
        Bills,
        Education,
        Health,
        Sport,
        Pets,
        Traveling,
        Taxes,
        Alcohol,
        Cigarette,
        Beauty,
        Credit,
        Other
    }

    public abstract class FinanceOperation
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }
        public FinanceOperation(decimal amount = 0m, DateTime? date = null, string description = "")
        {
            Amount = amount;
            Date = date == null ? DateTime.Today : date;
            Description = description;
        }
    }
    public class Income : FinanceOperation
    {
        public IncomeType Type { get; set; }
        public Income(decimal amount = 0m, IncomeType type = IncomeType.Other, DateTime? date = null, string description = "")
            : base(amount, date, description)
        {
            Type = type;
        }
    }
    public class Spends : FinanceOperation
    {
        public SpendType Type { get; set; }
        public Spends(decimal amount = 0m, SpendType type = SpendType.Other, DateTime? date = null, string description = "")
            : base(amount, date, description)
        {
            Type = type;
        }
    }
}
