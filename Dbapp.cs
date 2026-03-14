////////////////////////////////////////////////////////////////////////////
// TINFO 200 A, Winter 2026
// UWTacoma SET, Jason Chang
// 2026-03-13
///////////////////////////////////////////////////////////////////////////////
// Change History
// Date ------- Developer -- Description
// 2026-03-13  Chang       Initial creation from StudentDB template
// 2026-03-13  Chang       CRUD operations and payroll processing

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics.Contracts;

namespace EmpDB
{
    internal class Dbapp
    {
		// Runtime storage of employee objects
		private List<Employee> employees = new List<Employee>();

		// File names for data persistene
		private const string EMPLOYEE_INPUT_FILE = "employees_input.txt";
        private const string EMPLOYEE_OUTPUT_FILE = "employees_output.txt";
        public Dbapp()
        {
            ReadEmployeeDataFromInputFile(); 
        }

		private void ReadEmployeeDataFromInputFile()
		{
            StreamReader inFile = new StreamReader(EMPLOYEE_INPUT_FILE);
            string employeeType = string.Empty;
			while ((employeeType = inFile.ReadLine()) != null)
			{
				string first = inFile.ReadLine();
				string last = inFile.ReadLine();
				string email = inFile.ReadLine();
				string ssn = inFile.ReadLine();

				if (employeeType == "SalariedEmployee")
				{
					decimal weeklySalary = decimal.Parse(inFile.ReadLine());
					//may not be necessary for payable
					int workedWeeks = int.Parse(inFile.ReadLine());
					//may  not be necessary for Ipayable but adding anyway for now
					decimal amountOwed= decimal.Parse(inFile.ReadLine());
					Employee salaried = new SalariedEmployee(first, last, ssn, email, weeklySalary);
					employees.Add(salaried);
				}
				else if (employeeType == "HourlyEmployee")
				{
					//pay per hour
					decimal wage = decimal.Parse(inFile.ReadLine());
					//hours total worked before pay period
					decimal hours = decimal.Parse(inFile.ReadLine());
					//not sure if needed yet
                    decimal amountOwed = decimal.Parse(inFile.ReadLine());
                    Employee hourly = new HourlyEmployee(first, last, ssn, email, wage, hours);
					employees.Add(hourly);
				}
				else if (employeeType == "CommissionEmployee")
				{
					decimal grossSales = decimal.Parse(inFile.ReadLine());
					decimal commissionRate = decimal.Parse(inFile.ReadLine());
					//not sure if needed yet
                    decimal amountOwed = decimal.Parse(inFile.ReadLine());
                    Employee commission = new CommissionEmployee(first, last, ssn, email, grossSales, commissionRate);
					employees.Add(commission);
				}
				else if (employeeType == "BasePlusCommissionEmployee")
				{
					decimal grossSales = decimal.Parse(inFile.ReadLine());
					decimal commissionRate = decimal.Parse(inFile.ReadLine());
					decimal baseSalary = decimal.Parse(inFile.ReadLine());
					//not sure if needed yet
                    decimal amountOwed = decimal.Parse(inFile.ReadLine());
                    Employee bpCommission = new BasePlusCommissionEmployee(first, last, ssn, email,
						grossSales, commissionRate, baseSalary);
					employees.Add(bpCommission);
				}
				else
				{
					Console.WriteLine($"ERROR: {employeeType} is not a valid employee type.");
				}
			}
			inFile.Close();

			Console.WriteLine($"Loaded {employees.Count} employees from file.");
		}

		public void GoDataBase()
        {
			while (true)
			{
				DisplayMainMenu();
				char selection = GetUserSelection();

				switch (selection)
				{
					case 'C':
					case 'c':
						CreateNewEmployeeRecord();
						break;
					case 'F':
					case 'f':
						string email = string.Empty;
						FindEmployeeRecord(out email);
						break;
					case 'P':
					case 'p':
						PrintAllRecords();
						break;
					case 'R':
					case 'r':
						ProcessPayroll();
						break;
					case 'U':
					case 'u':
						UpdateEmployeeRecord();
						break;
					case 'D':
					case 'd':
						DeleteEmployeeRecord();
						break;
					case 'E':
					case 'e':
						SaveEmployeeDataToOutputFile();
						Environment.Exit(0);
						break;
					case 'Q':
					case 'q':
						Environment.Exit(0);
						break;
					case 'S':
					case 's':
						SaveEmployeeDataToOutputFile();
						break;
					default:
						Console.Write($"\n\nERROR: {selection} is not a valid choice. Select again: ");
						break;
				}
			}
		}

        private void SaveEmployeeDataToOutputFile()
        {
            throw new NotImplementedException();
        }

        private void UpdateEmployeeRecord()
        {
            //use the util method find to determine that the employee to add
            //is not already in the database- if they are print an error mesage in return
            string email = string.Empty;
            Employee emp = FindEmployeeRecord(out email);
            //if email matched and found  in list
            if (emp != null)
            {
                //delete by default since both operation types require it
                Console.WriteLine($"Found email {email}");
				//maybe invoke ouor method for cleaner output
                employees.Remove(emp);
                Console.WriteLine($"Updating {email} account! ");
                //run update procedure
                //run new  instance of database
                CreateNewEmployeeRecord();
                Console.WriteLine($"Sucessfully updated {email}! ");
            }//do nothing if no email match  in list
        }

        private void ProcessPayroll()
        {
            throw new NotImplementedException();
        }
        private void PrintAllRecords()
        {
            throw new NotImplementedException();
        }

        private char GetUserSelection()
        {
			ConsoleKeyInfo key = Console.ReadKey();
			return key.KeyChar;
        }

        private void DisplayMainMenu()
        {
			Console.Write(@"
    ************************************
    *****   Employee Payroll DB    *****
    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    [C]reate a new employee record
    [F]ind an employee record
    [P]rint all employee records
    [R]un payroll (process payments)
    [U]pdate an existing employee record
    [D]elete an existing employee record
    [E]xit the app - Saving all changes
    [Q]uit the app - Discard changes
    [S]ave all changes and continue
    ************************************
    User selection: ");
        }

        //can only be done if student is aready in db
        private void CreateNewEmployeeRecord()
        {
            //throw new NotImplementedException();

            //use the util method find to determine that the employee to add
            //is not already in the database- if they are print an error mesage in return
            string email = string.Empty;
            Employee emp = FindEmployeeRecord(out email);

            //if no record found in inputfile
            if (emp == null)
            {
                //employee is NOT in the database - we can add them
                Console.WriteLine($"Createing new employee record for email: {email}");
                Console.WriteLine("enter First name: ");
                string firstName = Console.ReadLine();
                Console.WriteLine("enter Last name: ");
                string lastName = Console.ReadLine();
                Console.Write("enter social security number: ");
                string socialSecurityNumber = Console.ReadLine();

                Console.Write("enter pay type letter: [S]alary, [H]ourly, [C]omission, or comission [P]lus base pay ");
                char employeeType = GetUserSelection();

                switch (employeeType)
                {
					case 'S':
					case 's':
						Console.WriteLine();
						Console.WriteLine("Enter weekly salary amount:");
						int weeklySalary = int.Parse(Console.ReadLine());
						//create the new employee object and add it to the list
						emp = new SalariedEmployee(firstName, lastName, socialSecurityNumber, email, weeklySalary);
						employees.Add(emp);
						break;
					case 'H':
					case 'h':
                        Console.WriteLine();
                        Console.WriteLine("Enter hourly Wage amount:");
                        decimal hourlyWage = decimal.Parse(Console.ReadLine());
						Console.WriteLine("Enter hours worked. 0  if none.");
						decimal hoursWorked = decimal.Parse(Console.ReadLine());
                        //create the new employee object and add it to the list
                        emp = new HourlyEmployee(firstName, lastName, socialSecurityNumber, email, hourlyWage, hoursWorked);
                        employees.Add(emp);
                        break;
                    case 'C':
					case 'c':
                        Console.WriteLine();
                        Console.WriteLine("Enter comission percentage:");
                        decimal commissionRate = decimal.Parse(Console.ReadLine());
						Console.WriteLine("Enter total gross sales if any. 0  if none.");
						decimal grossSales = decimal.Parse(Console.ReadLine());	
                        //create the new employee object and add it to the list
                        emp = new CommissionEmployee(firstName, lastName, socialSecurityNumber, email, grossSales, commissionRate);
                        employees.Add(emp);
                        break;
					case 'P':
					case 'p':
                        Console.WriteLine();
                        Console.WriteLine("Enter comission percentage:");
                        decimal commissionRated = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Enter total gross sales if any. 0  if none.");
                        decimal grossSale = decimal.Parse(Console.ReadLine());
						Console.WriteLine("enter base weekly salary");
						decimal baseSalary = decimal.Parse(Console.ReadLine());
                        //create the new employee object and add it to the list
                        emp = new BasePlusCommissionEmployee(firstName, lastName,socialSecurityNumber,email, grossSale,commissionRated, baseSalary);
                        employees.Add(emp);
                        break;
                    default:
						Console.WriteLine($"Error: Student with email {email} already exists");
						break;
                }
            }
            else
            {
                Console.WriteLine($"error: student with email {email} alread exists cannot create duplicate record.");
            }


        }


        // Find operation will search the current list for the resence of a given email
        // address and return the Employee record if found, other wise return null.
        private Employee FindEmployeeRecord(out string email)
		{
			Console.WriteLine("\nENTER the email address to serach for: ");
			email = Console.ReadLine();

			// iterate through the list looking for the search
			foreach (Employee emp in employees)
			{ 
				if(email==emp.EmailAddress)
				{
					// serach email WAS FOUND - report back and return the object
					Console.WriteLine($"Found email address: {emp.EmailAddress}\n");
					return emp;
				}
			}
			Console.WriteLine($"{email} NOT FOUND");
			return null;
		}

		// Deletes an existing employee record from DB
		// Uses email as primary key.
		private void DeleteEmployeeRecord()
		{
			string email = string.Empty;
			Employee emp = FindEmployeeRecord(out email);
			if (emp == null)
			{
				Console.WriteLine("Cannot Delete Employee -- Not found");
				return;
			}

			employees.Remove(emp);

			Console.WriteLine($"Employee with email {email} deleted");
		}
    }
}
