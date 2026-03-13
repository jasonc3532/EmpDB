////////////////////////////////////////////////////////////////////////////
//
//* (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               *
//* Pearson Education, Inc. All Rights Reserved.                           *
// Fig. 12.8: BasePlusCommissionEmployee.cs
// BasePlusCommissionEmployee class that extends CommissionEmployee.
using System;

namespace EmpDB
{
    public class BasePlusCommissionEmployee : CommissionEmployee
    {
        private decimal baseSalary; // base salary per week

        // six-parameter constructor
        public BasePlusCommissionEmployee(string firstName, string lastName,
           string socialSecurityNumber, string email, decimal grossSales,
           decimal commissionRate, decimal baseSalary)
           : base(firstName, lastName, socialSecurityNumber,
                grossSales, commissionRate)
        {
            BaseSalary = baseSalary; // validates base salary
        }

        // property that gets and sets 
        // BasePlusCommissionEmployee's base salary
        public decimal BaseSalary
        {
            get
            {
                return baseSalary;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(BaseSalary)} must be >= 0");
                }

                baseSalary = value;
            }
        }

        // calculate earnings
        public override decimal Earnings() => BaseSalary + base.Earnings();

		// return string representation of BasePlusCommissionEmployee
		public override string ToString()
		{
			string str = base.ToString();
			str += $"Base Salary: {BaseSalary:C}\n";
			return str;
		}

		public override string ToStringForOutputFile()
		{
			string str = this.GetType().Name + "\n";
			str += base.ToStringForOutputFile() + "\n";
			str += $"{baseSalary:F2}";
			return str;
		}
	}
}

