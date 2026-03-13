////////////////////////////////////////////////////////////////////////////
//
//* (C) Copyright 1992-2017 by Deitel & Associates, Inc. and               *
//* Pearson Education, Inc. All Rights Reserved.                           *
// Fig. 12.4: Employee.cs
// Employee abstract base class.
namespace EmpDB
{
    public abstract class Employee
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string SocialSecurityNumber { get; }

        // three-parameter constructor
        public Employee(string firstName, string lastName,
           string socialSecurityNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
        }

        // return string representation of Employee object, using properties
        public override string ToString() => $"{FirstName} {LastName}\n" +
           $"social security number: {SocialSecurityNumber}";

        // abstract method overridden by derived classes
        public abstract decimal Earnings(); // no implementation here
    }
}


