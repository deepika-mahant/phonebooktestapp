using NUnit.Framework;
using PhoneBookTestApp.Models;
using PhoneBookTestApp.Repositories;

namespace PhoneBookTestAppTests
{
    // ReSharper disable InconsistentNaming

    [TestFixture]
    public class PhoneBookTest
    {
        [Test]
        public void AddPerson()
        {
            var phoneBook = new PhoneBook();
            Person person = new Person { Name = "Deepika Thakur", PhoneNumber = "(110) 123-4567", Address = "Pownell Garden" };
            phoneBook.AddPerson(person);
            Assert.Pass();
        }

        [Test]
        public void FindPerson()
        {
            var phoneBook = new PhoneBook();
            var person = phoneBook.FindPerson("Jermy", "Mathew");
            Assert.IsNull(person);
        }

        [Test]
        public void GetAllPersons()
        {
            var phoneBook = new PhoneBook();
            var person = phoneBook.GetAllPersons();
            Assert.IsNotNull(person);
        }
    }

    // ReSharper restore InconsistentNaming 
}