////////////////////////////////////////////////////////////////////////////
//
//* (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               *
//* Pearson Education, Inc. All Rights Reserved.                           *
// Fig. 12.8: BasePlusCommissionEmployee.cs
// BasePlusCommissionEmployee class that extends CommissionEmployee.
////////////////////////////////////////////////////////////////////////////
// TINFO 200 A, Winter 2026
// UWTacoma SET, Jason Chang
// 2026-03-13
///////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-13  Chang       
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
           : base(firstName, lastName, socialSecurityNumber, email,
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
            //editing out below line, was messing output file, not deleting just in case it breaks someone else's stuff
			//string str = this.GetType().Name + "\n";
			string str = base.ToStringForOutputFile();
			str += $"{baseSalary:F2}\n";
			return str;
		}
	}
}

