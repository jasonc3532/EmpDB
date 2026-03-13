////////////////////////////////////////////////////////////////////////////
//
//* (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               *
//* Pearson Education, Inc. All Rights Reserved.                           *
// Fig. 12.6: HourlyEmployee.cs
// HourlyEmployee class that extends Employee.
using System;

namespace EmpDB
{
    public class HourlyEmployee : Employee
    {
        private decimal wage; // wage per hour
        private decimal hours; // hours worked for the week

        // five-parameter constructor
        public HourlyEmployee(string firstName, string lastName,
           string socialSecurityNumber, string email, decimal hourlyWage,
           decimal hoursWorked)
           : base(firstName, lastName, socialSecurityNumber)
        {
            Wage = hourlyWage; // validate hourly wage 
            Hours = hoursWorked; // validate hours worked 
        }

        // property that gets and sets hourly employee's wage
        public decimal Wage
        {
            get
            {
                return wage;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(Wage)} must be >= 0");
                }

                wage = value;
            }
        }

        // property that gets and sets hourly employee's hours
        public decimal Hours
        {
            get
            {
                return hours;
            }
            set
            {
                if (value < 0 || value > 168) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(Hours)} must be >= 0 and <= 168");
                }

                hours = value;
            }
        }

        // calculate earnings; override Employee’s abstract method Earnings
        public override decimal Earnings()
        {
            if (Hours <= 40) // no overtime                          
            {
                return Wage * Hours;
            }
            else
            {
                return (40 * Wage) + ((Hours - 40) * Wage * 1.5M);
            }
        }

		// return string representation of HourlyEmployee object
		public override string ToString()
		{
			string str = base.ToString();
			str += $"  Type: Hourly Employee\n";
			str += $"Hourly Wage: {Wage:C}\n";
			str += $" Hours Worked: {Hours:F2}\n";
			str += $"   Pay Amount: {Earnings():C}\n";
			return str;
		}

		public override string ToStringForOutputFile()
		{
			string str = this.GetType().Name + "\n";
			str += base.ToStringForOutputFile() + "\n";
			str += $"{Wage:F2}\n";
			str += $"{Hours:F2}";
			return str;
		}
	}
}


