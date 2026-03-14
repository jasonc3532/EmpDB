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
					Employee salaried = new SalariedEmployee(first, last, ssn, email, weeklySalary);
					employees.Add(salaried);
				}
				else if (employeeType == "HourlyEmployee")
				{
					decimal wage = decimal.Parse(inFile.ReadLine());
					decimal hours = decimal.Parse(inFile.ReadLine());
					Employee hourly = new HourlyEmployee(first, last, ssn, email, wage, hours);
					employees.Add(hourly);
				}
				else if (employeeType == "CommissionEmployee")
				{
					decimal grossSales = decimal.Parse(inFile.ReadLine());
					decimal commissionRate = decimal.Parse(inFile.ReadLine());
					Employee commission = new CommissionEmployee(first, last, ssn, email, grossSales, commissionRate);
					employees.Add(commission);
				}
				else if (employeeType == "BasePlusCommissionEmployee")
				{
					decimal grossSales = decimal.Parse(inFile.ReadLine());
					decimal commissionRate = decimal.Parse(inFile.ReadLine());
					decimal baseSalary = decimal.Parse(inFile.ReadLine());
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
			if (emp != null)
			{
				Console.WriteLine("Cannot Delete Employee -- Not found");
				return;
			}

			employees.Remove(emp);

			Console.WriteLine($"Employee with email {email} deleted");
		}
    }
}
