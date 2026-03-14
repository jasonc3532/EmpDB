////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////
//
//* (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               *
//* Pearson Education, Inc. All Rights Reserved.                           *
// Fig. 12.5: SalariedEmployee.cs
// SalariedEmployee class that extends Employee.
////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////
// TINFO 200 A, Winter 2026
// UWTacoma SET, Jason Chang
// 2026-03-13
///////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-13  Axec	       added toouptufiletostring to match input file format now that we track labor and money owed

using System;

namespace EmpDB
{
    public class SalariedEmployee : Employee
    {
        private decimal weeklySalary;
        private int WorkedWeeks { get; set; }
        private decimal AmountOwed { get; set; }

        // four-parameter constructor
        public SalariedEmployee(string firstName, string lastName,
           string socialSecurityNumber, string email, decimal weeklySalary, int workedWeeks, decimal amountOwed)
           : base(firstName, lastName, socialSecurityNumber, email)
        {
            WeeklySalary = weeklySalary; // validate salary via property
            WorkedWeeks = workedWeeks;  
            AmountOwed = amountOwed;
        }

        // property that gets and sets salaried employee's salary
        public decimal WeeklySalary
        {
            get
            {
                return weeklySalary;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(WeeklySalary)} must be >= 0");
                }

                weeklySalary = value;
            }
        }

        // calculate earnings; override abstract method Earnings in Employee
        public override decimal Earnings() => WeeklySalary;

		// return string representation of SalariedEmployee object
		public override string ToString()
		{
			string str = base.ToString();
			str += $"  Type: Salaried Employee\n";
			str += $"Weekly Salary: {WeeklySalary:C}\n";
			str += $"   Pay Amount: {Earnings():C}\n";
			return str;
		}

		public override string ToStringForOutputFile()
		{
			string str = this.GetType().Name + "\n";
			str += base.ToStringForOutputFile() + "\n";
			str += $"{WeeklySalary:F2} \n";
            //for payable  stuff, still not sure if needed
            str += $"{WorkedWeeks:F2} \n";
            str += $"{AmountOwed:F2} \n";
            return str;
		}
	}
}

