using System.Collections.Generic;
using AgeRanger.Controllers;
using AgeRanger.Models;

namespace AgeRanger.Repositories
{
    public interface IPersonRepository
    {
        List<Person> GetPersons();
        List<Person> GetPersonBySearch(string query, List<Person> person);
        Person GetPersonById(int id);

        void Add(Person person);
        void Remove(Person person);

        void Update(AgeGroupViewModel viewModel, int id);
    }
}