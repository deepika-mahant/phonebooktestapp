using PhoneBookTestApp.Models;
using System.Collections.Generic;

namespace PhoneBookTestApp.Repositories
{
    public interface IPhoneBook
    {
        void AddPerson(Person newPerson);
        Person FindPerson(string firstName, string lastName);
        IList<Person> GetAllPersons();
    }
}