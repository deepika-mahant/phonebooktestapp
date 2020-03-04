using PhoneBookTestApp.Models;
using PhoneBookTestApp.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DatabaseUtil.initializeDatabase();
                
                // Create person objects and put them in the PhoneBook and database
                AddSampleData();

                // Find Cynthia Smith and print out just her entry
                FindPerson("Cynthia", "Smith");
                               
                // Print the phone book out to System.out
                GetAndDisplayAll();

                // TODO: insert the new person objects into the database
                AddNewPerson();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                // Logg exception error 
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                DatabaseUtil.CleanUp();
            }
        }

        #region dml ddl operations
        public static void AddSampleData()
        {
            PhoneBook phonebook = new PhoneBook();
            Person objPerson1 = new Person { Name = "John Smith", PhoneNumber = "(248) 123-4567", Address = "1234 Sand Hill Dr, Royal Oak, MI" };
            Person objPerson2 = new Person { Name = "Cynthia Smith", PhoneNumber = "(824) 128-8758", Address = "875 Main St, Ann Arbor, MI" };
            phonebook.AddPerson(objPerson1);
            phonebook.AddPerson(objPerson2);
        }
        public static void GetAndDisplayAll()
        {
            PhoneBook phoneBook = new PhoneBook();
            var allRecords = phoneBook.GetAllPersons();
            if (allRecords.Count() > 0)
            {
                Console.WriteLine("----------------------All phone book records ----------------------");
                foreach (var person in allRecords)
                {
                    Console.WriteLine("Name: {0}, Phone: {1}, Address: {2}", person.Name, person.PhoneNumber, person.Address);
                }
                Console.WriteLine("-------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("No record found");
            }
        }
        public static void FindPerson(string FirstName, string LastName)
        {
            PhoneBook phoneBook = new PhoneBook();
            Person findPerson = phoneBook.FindPerson(FirstName, LastName);
            if (findPerson != null)
            {
                Console.WriteLine("Here are the details of " + FirstName);
                Console.WriteLine("Name: " + findPerson.Name);
                Console.WriteLine("Phone: " + findPerson.PhoneNumber);
                Console.WriteLine("Address: " + findPerson.Address);
            }
            else
            {
                Console.WriteLine("Person not found");
            }
        }
        public static void AddNewPerson()
        {
            PhoneBook phoneBook = new PhoneBook();
            Console.WriteLine("Enter FirstName:");
            var firstName = Console.ReadLine();

            Console.WriteLine("Enter LastName:");
            var lastName = Console.ReadLine();

            Console.WriteLine("Phone Number:");
            var phone = Console.ReadLine();

            Console.WriteLine("Enter Address:");
            var address = Console.ReadLine();
            Person objPerson = new Person
            {
                Name = firstName + " " + lastName,
                PhoneNumber = phone,
                Address = address
            };
            phoneBook.AddPerson(objPerson);

            GetAndDisplayAll();
        }
        #endregion
    }
}
