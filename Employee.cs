////////////////////////////////////////////////////////////////////////////
//
//* (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               *
//* Pearson Education, Inc. All Rights Reserved.                           *
// Fig. 12.4: Employee.cs
// Employee abstract base class.
////////////////////////////////////////////////////////////////////////////
// TINFO 200 A, Winter 2026
// UWTacoma SET, Jason Chang
// 2026-03-13
///////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-13  Chang       Employee abstract base class with EmailAddress
using System;

namespace EmpDB
{
    public abstract class Employee
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string SocialSecurityNumber { get; }
		public string EmailAddress { get; set; } // Primary Key

		// three-parameter constructor
		public Employee(string firstName, string lastName,
           string socialSecurityNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
        }

		// return string representation of Employee object, using properties
		public override string ToString()
		{
			string str = "\n**** Employee Record ****************\n";
			str += $"First: {FirstName}\n";
			str += $" Last: {LastName}\n";
			str += $"Email: {EmailAddress}\n";
			str += $"  SSN: {SocialSecurityNumber}\n";
			return str;
		}

		public virtual string ToStringForOutputFile()
		{
			string str = string.Empty;
			str += $"{FirstName}\n";
			str += $"{LastName}\n";
			str += $"{EmailAddress}\n";
			str += $"{SocialSecurityNumber}";
			return str;
		}

		// abstract method overridden by derived classes
		public abstract decimal Earnings();
	}
}



