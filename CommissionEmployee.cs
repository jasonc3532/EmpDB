////////////////////////////////////////////////////////////////////////////
//
//* (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               *
//* Pearson Education, Inc. All Rights Reserved.                           *
// Fig. 12.7: CommissionEmployee.cs
// CommissionEmployee class that extends Employee.
using System;

namespace EmpDB
{
    public class CommissionEmployee : Employee
    {
        private decimal grossSales; // gross weekly sales
        private decimal commissionRate; // commission percentage

        // five-parameter constructor
        public CommissionEmployee(string firstName, string lastName,
           string socialSecurityNumber, string email, decimal grossSales,
           decimal commissionRate)
           : base(firstName, lastName, socialSecurityNumber)
        {
            GrossSales = grossSales; // validates gross sales
            CommissionRate = commissionRate; // validates commission rate
        }

        // property that gets and sets commission employee's gross sales
        public decimal GrossSales
        {
            get
            {
                return grossSales;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(GrossSales)} must be >= 0");
                }

                grossSales = value;
            }
        }

        // property that gets and sets commission employee's commission rate
        public decimal CommissionRate
        {
            get
            {
                return commissionRate;
            }
            set
            {
                if (value <= 0 || value >= 1) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                       value, $"{nameof(CommissionRate)} must be > 0 and < 1");
                }

                commissionRate = value;
            }
        }

        // calculate earnings; override abstract method Earnings in Employee
        public override decimal Earnings() => CommissionRate * GrossSales;

		// return string representation of CommissionEmployee object
		public override string ToString()
		{
			string str = base.ToString();
			str += $"  Type: Commission Employee\n";
			str += $"Gross Sales: {GrossSales:C}\n";
			str += $"Commission Rate: {CommissionRate:F2}\n";
			return str;
		}

		public override string ToStringForOutputFile()
		{
			string str = this.GetType().Name + "\n";
			str += base.ToStringForOutputFile() + "\n";
			str += $"{GrossSales:F2}\n";
			str += $"{CommissionRate:F2}";
			return str;
		}
	}
}


