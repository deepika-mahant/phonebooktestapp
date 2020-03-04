using PhoneBookTestApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
namespace PhoneBookTestApp.Repositories
{
    public class PhoneBook : IPhoneBook
    {
        public void AddPerson(Person person)
        {
            var con = DatabaseUtil.GetConnection();
            var cmd = new SQLiteCommand(con);
            cmd.CommandText = "INSERT INTO PHONEBOOK(Name, PhoneNumber, Address) VALUES(@Name, @PhoneNumber,@Address)";

            cmd.Parameters.AddWithValue("@Name", person.Name);
            cmd.Parameters.AddWithValue("@PhoneNumber", person.PhoneNumber);
            cmd.Parameters.AddWithValue("@Address", person.Address);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public Person FindPerson(string firstName, string lastName)
        {
            var con = DatabaseUtil.GetConnection();
            string stm = "SELECT * FROM PHONEBOOK Where Name = '" + firstName + " " + lastName + "'";

            var cmd = new SQLiteCommand(stm, con);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            var person = new Person();
            while (rdr.Read())
            {
                person.Name = rdr.GetString(0);
                person.PhoneNumber = rdr.GetString(1);
                person.Address = rdr.GetString(2);
            }
            return person;
        }

        public IList<Person> GetAllPersons()
        {
            var personList = new List<Person>();
            string query = "SELECT * FROM PHONEBOOK";
            DataTable data = GetDataTable(query);

            foreach (DataRow row in data.Rows)
            {
                Person person = new Person();
                person.Name = Convert.ToString(row.ItemArray[0]);
                person.PhoneNumber = Convert.ToString(row.ItemArray[1]);
                person.Address = Convert.ToString(row.ItemArray[2]);
                personList.Add(person);
            }
                      
            return personList;
        }

        public DataTable GetDataTable(string query)
        {
            try
            {
                DataTable dt = new DataTable();
                var con = DatabaseUtil.GetConnection();
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        dt.Load(rdr);
                        return dt;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}