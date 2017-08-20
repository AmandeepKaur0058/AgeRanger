using System.Collections.Generic;
using System.Linq;
using AgeRanger.Data;
using AgeRanger.Models;

namespace AgeRanger.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IApplicationDbContext _context;

        public PersonRepository(IApplicationDbContext context)
        {
            _context=context;
        }

        public List<Person> GetPersons()
        {           
            var personData = (from p in _context.People
                from a in _context.AgeGroups
                where p.Age >= a.MinAge && p.Age < a.MaxAge
                select new {p , a})
                .ToList();

            var persons = personData.Select(m=> new Person
            {
                Id = m.p.Id,
                Age = m.p.Age,
                FirstName = m.p.FirstName,
                LastName = m.p.LastName,
                AgeGroup = m.a.Description
            }).ToList();

            return persons;
        }
        public  List<Person> GetPersonBySearch(string query, List<Person> person)
        {
            int age = 0;
            int.TryParse(query, out age);

            return person.Where(p => p.FirstName.Contains(query) || p.LastName.Contains(query) ||p.Age==age||
                                     p.AgeGroup.Contains(query)).ToList();
        }

        public Person GetPersonById(int id)
        {
            return _context.People.SingleOrDefault(x => x.Id == id);
        }

        public void Add(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public void Remove(Person person)
        {
            _context.People.Remove(person);
            _context.SaveChanges();
        }

        public void Update(AgeGroupViewModel viewModel, int id)
        {
            var people = GetPersonById(id);
            people.FirstName = viewModel.FirstName;
            people.LastName = viewModel.LastName;
            people.Age = viewModel.Age;

            _context.SaveChanges();
        }
    }
}